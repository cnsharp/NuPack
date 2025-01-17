using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.Extensions.Projects;
using CnSharp.VisualStudio.Extensions.Util;
using EnvDTE;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Versioning;

namespace CnSharp.VisualStudio.NuPack.Extensions
{
    public static class NuGetExtensions
    {
        public static string GetNuSpecFilePath(this Project project)
        {
            return Path.Combine(project.GetDirectory(), NuGetDomain.NuSpecFileName);
        }

        public static ManifestMetadata LoadFromNuspecFile(string file)
        {
            var metadata = new ManifestMetadata();
            var doc = XDocument.Load(file);
            var elements = doc.Element("package").Element("metadata").Elements().ToList();
            var props = typeof(ManifestMetadata).GetProperties()
                .Where(p => p.PropertyType.IsValueType || 
                                 p.PropertyType == typeof(string))
                .ToList();
            foreach (var prop in props)
            {
                if (prop.PropertyType == typeof(string))
                {
                    var v = elements.FirstOrDefault(m => m.Name.LocalName.Equals(prop.Name,StringComparison.InvariantCultureIgnoreCase))?.Value;
                    if (v != null)
                    {
                        prop.SetValue(metadata, v, null);
                    }
                }
                else if (prop.PropertyType == typeof(bool))
                {
                    var v = elements.FirstOrDefault(m => m.Name.LocalName.Equals(prop.Name, StringComparison.InvariantCultureIgnoreCase))?.Value;
                    if (v != null)
                    {
                        prop.SetValue(metadata, v.Equals("true",StringComparison.InvariantCultureIgnoreCase), null);
                    }
                }
            } 
            SetUrl(elements, "licenseUrl", v => metadata.SetLicenseUrl(v));
            SetUrl(elements, "iconUrl", v => metadata.SetIconUrl(v));
            SetUrl(elements, "projectUrl", v => metadata.SetProjectUrl(v));
            return metadata;
        }

        private static void SetUrl(List<XElement> elements, string node, Action<string> action)
        {
            var v = elements.FirstOrDefault(m => m.Name.LocalName.Equals(node, StringComparison.InvariantCultureIgnoreCase))?.Value;
            if (!string.IsNullOrWhiteSpace(v))
            {
                action.Invoke(v);
            }
        }

        public static void UpdateNuspec(this Project project, ManifestMetadata metadata)
        {
            var nuspecFile = project.GetNuSpecFilePath();
            if (!File.Exists(nuspecFile))
            {
                return;
            }

            metadata.SaveToNuSpec(nuspecFile);
        }

        public static void SaveToNuSpec(this ManifestMetadata metadata,string nuspecFile)
        {
            var doc = new XmlDocument();
            doc.Load(nuspecFile);
            metadata.SyncToXmlDocument(doc);
            doc.Save(nuspecFile);
        }

        public static void SyncToXmlDocument(this  ManifestMetadata metadata, XmlDocument doc)
        {
            var metadataNode = doc.SelectSingleNode("package/metadata");
            if (metadataNode == null)
                return;
            var props = typeof(ManifestMetadata).GetProperties().Where(p => p.PropertyType.IsValueType || p.PropertyType == typeof(string)).ToList();
            props.ForEach(p =>
            {
                var val = p.GetValue(metadata, null);
                if (val == null) return;
                var text = p.PropertyType == typeof(bool)
                    ? val.ToString().ToLower()
                    : val.ToString();
                metadataNode.SetXmlNode(p.Name.Substring(0,1).ToLower()+p.Name.Substring(1),text);
            });
            UpdateDependencies( metadata,doc);
        }

        private static void SetXmlNode(this XmlNode metadataNode, string key, string value)
        {
            var idNode = metadataNode.SelectSingleNode(key);
            if (idNode != null)
                idNode.InnerText = value == null ? string.Empty : value;
        }

        public static void UpdateDependencies(this ManifestMetadata metadata, XmlDocument doc)
        {
            var metadataNode = doc.SelectSingleNode("package/metadata");
            if (metadataNode == null)
                return;

            if (metadata.DependencyGroups?.Any() == true)
            {
                var depNode = metadataNode.SelectSingleNode("dependencyGroups");
                if (depNode == null)
                {
                    var node = doc.CreateElement("dependencyGroups");
                    metadataNode.AppendChild(node);
                    depNode = node;
                }

                depNode.RemoveAll();
                var tempNode = doc.CreateElement("temp");
                tempNode.InnerXml = XmlSerializerHelper.GetXmlStringFromObject(metadata.DependencyGroups);
                depNode.InnerXml = tempNode.ChildNodes[0].InnerXml;
            }
        }



        public static bool IsEmptyOrPlaceHolder(this string value)
        {
            return string.IsNullOrWhiteSpace(value) || value.StartsWith("$");
        }

        public static ManifestMetadata ToManifestMetadata(this ProjectAssemblyInfo pai)
        {
            var metadata = new ManifestMetadata
            {
                Id = pai.Title,
                Owners = new []{pai.Company},
                Title = pai.Title,
                Description = pai.Description,
                Authors = new[] { pai.Company },
                Copyright = pai.Copyright
            };
            return metadata;
        }

        public static ManifestMetadata CopyFromAssemblyInfo(this ManifestMetadata metadata,ProjectAssemblyInfo pai)
        {
            if (metadata.Id.IsEmptyOrPlaceHolder())
                metadata.Id = pai.Title;
            if (metadata.Title.IsEmptyOrPlaceHolder())
                metadata.Title = pai.Title;
            if (metadata.Owners == null || !metadata.Owners.Any())
                metadata.Owners = new[] { pai.Company };
            if (metadata.Description.IsEmptyOrPlaceHolder())
                metadata.Description = pai.Description;
            if (metadata.Authors == null || !metadata.Authors.Any())
                metadata.Authors = new[] { pai.Company };
            if (metadata.Copyright.IsEmptyOrPlaceHolder())
                metadata.Copyright = pai.Copyright;
            return metadata;
        }

        public static ManifestMetadata ToManifestMetadata(this PackageProjectProperties ppp)
        {
            var meta = new ManifestMetadata
            {
                Id = ppp.PackageId,
                Authors = ppp.Authors.Split(','),
                Copyright = ppp.Copyright,
                Owners = ppp.Company.Split(','),
                Description = ppp.Description,
                Icon = ppp.PackageIcon,
                Language = ppp.NeutralLanguage,
                Readme = ppp.PackageReadmeFile,
                ReleaseNotes = ppp.PackageReleaseNotes,
                RequireLicenseAcceptance = ppp.PackageRequireLicenseAcceptance,
                Tags = ppp.PackageTags,
                Version = new NuGetVersion(ppp.Version)
            };

            if (!string.IsNullOrWhiteSpace(ppp.RepositoryUrl))
            {
                meta.Repository = new RepositoryMetadata
                {
                    Type = ppp.RepositoryType ?? "git",
                    Url = ppp.RepositoryUrl
                };
            }

            if (!string.IsNullOrWhiteSpace(ppp.PackageLicenseExpression))
            {
                meta.LicenseMetadata = new LicenseMetadata(LicenseType.Expression, ppp.PackageLicenseExpression, null, null, LicenseMetadata.CurrentVersion);
            }
            else
            {
                meta.SetLicenseUrl(ppp.PackageLicenseUrl);
            }
            if(meta.Icon == null && !string.IsNullOrWhiteSpace(ppp.PackageIconUrl))
                meta.SetIconUrl(ppp.PackageIconUrl);
            meta.SetProjectUrl(ppp.PackageProjectUrl);
            return meta;
        }


        public static void SyncToPackageProjectProperties(this ManifestMetadata metadata, PackageProjectProperties ppp)
        {
            ppp.PackageId = metadata.Id;
            ppp.Authors = string.Join(",", metadata.Authors);
            ppp.Copyright = metadata.Copyright;
            ppp.Company = string.Join(",", metadata.Owners);
            ppp.Description = metadata.Description;
            ppp.PackageIcon = metadata.Icon;
            ppp.PackageIconUrl = metadata.IconUrl?.OriginalString;
            ppp.NeutralLanguage = metadata.Language;
            ppp.PackageLicenseUrl = metadata.LicenseUrl?.OriginalString;
            ppp.PackageReadmeFile = metadata.Readme;
            ppp.PackageReleaseNotes = metadata.ReleaseNotes;
            ppp.PackageRequireLicenseAcceptance = metadata.RequireLicenseAcceptance;
            ppp.PackageProjectUrl = metadata.ProjectUrl?.OriginalString;
            ppp.PackageTags = metadata.Tags;
            ppp.Version = metadata.Version?.ToString();
            if (metadata.Repository != null)
            {
                ppp.RepositoryType = metadata.Repository.Type;
                ppp.RepositoryUrl = metadata.Repository.Url;
            }

            if (metadata.LicenseMetadata != null)
            {
                ppp.PackageLicenseExpression = metadata.LicenseMetadata.License;
                // PackageLicenseUrl is deprecated, it cannot be conjunction with LicenseMetadata
                ppp.PackageLicenseUrl = null;
            }

            if (!string.IsNullOrWhiteSpace(ppp.PackageIcon))
            {
                ppp.PackageIconUrl = null;
            }
        }


        public static List<string> UpdateDependencyInSolution(string packageId, NuGetVersion newVersion)
        {
            var nuspecFiles = new List<string>();
            var projects = Host.Instance.DTE.GetSolutionProjects().ToList();
            projects.ForEach(p =>
            {
                var nuspecFile = p.GetNuSpecFilePath();
                if (File.Exists(nuspecFile))
                {
                    var metadata = LoadFromNuspecFile(nuspecFile);

                    if (metadata.DependencyGroups != null)
                    {
                        var found = false;
                        foreach (var g in metadata.DependencyGroups)
                        {
                            var packages = g.Packages.ToList();
                            int i = 0;
                            PackageDependency pd = null;
                            foreach (var d in packages)
                            {
                                if (d.Id == packageId)
                                {
                                    pd = d;
                                    found = true;
                                    break;
                                }

                                i++;
                            }

                            if (pd != null)
                            {
                                var npd = new PackageDependency(pd.Id, new VersionRange(newVersion));
                                packages.RemoveAt(i);
                                packages.Insert(i, npd);
                            }
                        }
                        if (found)
                        {
                            metadata.SaveToNuSpec(nuspecFile);
                            nuspecFiles.Add(nuspecFile);
                        }
                    }
                }
            });
            return nuspecFiles;
        }
    }

    public class NuGetDomain
    {
        public const string NuSpecFileName = "package.nuspec";
    }

}
