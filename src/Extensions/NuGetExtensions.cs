using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.Extensions.Projects;
using CnSharp.VisualStudio.NuPack.Util;
using EnvDTE;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Versioning;

namespace CnSharp.VisualStudio.NuPack.Extensions
{
    public static class NuGetExtensions
    {
        public static string GetNuspecFilePath(this Project project)
        {
            return Path.Combine(project.GetDirectory(), project.Name+".nuspec");
        }
        public static ManifestMetadata LoadFromNuspecFile(string file)
        {
            return Util.NuspecReader.ReadMetadata(file);
        }


        public static void UpdateNuspec(this Project project, ManifestMetadata metadata)
        {
            var nuspecFile = project.GetNuspecFilePath();
            if (!File.Exists(nuspecFile)) return;

            metadata.SaveToNuSpec(nuspecFile);
        }

        public static void SaveToNuSpec(this ManifestMetadata metadata, string nuspecFile)
        {
            NuspecWriter.UpdateNuspec(nuspecFile, metadata, true);
        }

        public static bool IsEmptyOrPlaceHolder(this string value)
        {
            return string.IsNullOrWhiteSpace(value) || (value.StartsWith("$") && value.EndsWith("$"));
        }

        public static bool IsNotEmptyOrPlaceHolder(this string value)
        {
            return !string.IsNullOrWhiteSpace(value) && !(value.StartsWith("$") && value.EndsWith("$"));
        }

        public static ManifestMetadata ToManifestMetadata(this ProjectAssemblyInfo pai)
        {
            var metadata = new ManifestMetadata
            {
                Id = pai.Title,
                Owners = new[] { pai.Company },
                Title = pai.Title,
                Description = pai.Description,
                Authors = new[] { pai.Company },
                Copyright = pai.Copyright
            };
            return metadata;
        }

        public static ManifestMetadata CopyFromManifestMetadata(this ManifestMetadata metadata, ManifestMetadata commonMetadata)
        {
            if (metadata.Id.IsEmptyOrPlaceHolder() && commonMetadata.Id.IsNotEmptyOrPlaceHolder())
                metadata.Id = commonMetadata.Id;
            if (metadata.Title.IsEmptyOrPlaceHolder() && commonMetadata.Title.IsNotEmptyOrPlaceHolder())
                metadata.Title = commonMetadata.Title;
            if (metadata.Owners?.Any() != true && commonMetadata.Owners?.Any() == true)
                metadata.Owners = commonMetadata.Owners;
            if (metadata.Description.IsEmptyOrPlaceHolder() && commonMetadata.Description.IsNotEmptyOrPlaceHolder())
                metadata.Description = commonMetadata.Description;
            if (metadata.Authors?.Any() != true && commonMetadata.Authors?.Any() == true)
                metadata.Authors = commonMetadata.Authors;
            if (metadata.Copyright.IsEmptyOrPlaceHolder() && commonMetadata.Copyright.IsNotEmptyOrPlaceHolder())
                metadata.Copyright = commonMetadata.Copyright;
            if (metadata.Icon.IsEmptyOrPlaceHolder() && commonMetadata.Icon.IsNotEmptyOrPlaceHolder())
                metadata.Icon = commonMetadata.Icon;
            try
            {
                if (metadata.IconUrl == null && commonMetadata.IconUrl != null)
                    metadata.SetIconUrl(commonMetadata.IconUrl.AbsoluteUri);
            }
            catch (UriFormatException ignored)
            {

            }
            try
            {
                if (metadata.LicenseUrl == null && commonMetadata.LicenseUrl != null)
                    metadata.SetLicenseUrl(commonMetadata.LicenseUrl.AbsoluteUri);
            }
            catch (UriFormatException ignored)
            {

            }

            if (metadata.LicenseMetadata == null && commonMetadata.LicenseMetadata != null)
                metadata.LicenseMetadata = commonMetadata.LicenseMetadata;
            try
            {
                if (metadata.ProjectUrl == null && commonMetadata.ProjectUrl != null)
                    metadata.SetProjectUrl(commonMetadata.ProjectUrl.AbsoluteUri);
            }
            catch (UriFormatException ignored)
            {

            }

            if (metadata.Readme.IsEmptyOrPlaceHolder() && commonMetadata.Readme.IsNotEmptyOrPlaceHolder())
                metadata.Readme = commonMetadata.Readme;
            if (metadata.ReleaseNotes.IsEmptyOrPlaceHolder() && commonMetadata.ReleaseNotes.IsNotEmptyOrPlaceHolder())
                metadata.ReleaseNotes = commonMetadata.ReleaseNotes;
            if (metadata.RequireLicenseAcceptance == false && commonMetadata.RequireLicenseAcceptance)
                metadata.RequireLicenseAcceptance = true;
            if (metadata.Tags.IsEmptyOrPlaceHolder() && commonMetadata.Tags.IsNotEmptyOrPlaceHolder())
                metadata.Tags = commonMetadata.Tags;
            if (metadata.Version == null && commonMetadata.Version != null)
                metadata.Version = commonMetadata.Version;
            if (metadata.Language.IsEmptyOrPlaceHolder() && commonMetadata.Language.IsNotEmptyOrPlaceHolder())
                metadata.Language = commonMetadata.Language;
            if (metadata.Repository == null && commonMetadata.Repository != null)
                metadata.Repository = commonMetadata.Repository;
            return metadata;
        }

        public static ManifestMetadata CopyFromAssemblyInfo(this ManifestMetadata metadata, ProjectAssemblyInfo pai)
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
                Authors = ppp.Authors?.Split(','),
                Copyright = ppp.Copyright,
                Owners = ppp.Company?.Split(','),
                Description = ppp.Description,
                Icon = ppp.PackageIcon,
                Language = ppp.NeutralLanguage,
                Readme = ppp.PackageReadmeFile,
                ReleaseNotes = ppp.PackageReleaseNotes,
                RequireLicenseAcceptance = ppp.PackageRequireLicenseAcceptance,
                Tags = ppp.PackageTags,
                Version = !string.IsNullOrEmpty(ppp.Version) ? new NuGetVersion(ppp.Version) : new NuGetVersion("1.0.0")
            };

            if (!string.IsNullOrWhiteSpace(ppp.RepositoryUrl))
                meta.Repository = new RepositoryMetadata
                {
                    Type = ppp.RepositoryType ?? "git",
                    Url = ppp.RepositoryUrl
                };

            if (!string.IsNullOrWhiteSpace(ppp.PackageLicenseExpression))
                meta.LicenseMetadata = new LicenseMetadata(LicenseType.Expression, ppp.PackageLicenseExpression, null,
                    null, LicenseMetadata.CurrentVersion);
            else
                meta.SetLicenseUrl(ppp.PackageLicenseUrl);
            if (meta.Icon == null && !string.IsNullOrWhiteSpace(ppp.PackageIconUrl))
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

            if (!string.IsNullOrWhiteSpace(ppp.PackageIcon)) ppp.PackageIconUrl = null;
        }


        public static List<string> UpdateDependencyInSolution(string packageId, NuGetVersion newVersion)
        {
            var nuspecFiles = new List<string>();
            var projects = Host.Instance.DTE.GetSolutionProjects().ToList();
            projects.ForEach(p =>
            {
                var nuspecFile = p.GetNuspecFilePath();
                if (File.Exists(nuspecFile))
                {
                    var metadata = LoadFromNuspecFile(nuspecFile);

                    if (metadata.DependencyGroups != null)
                    {
                        var found = false;
                        foreach (var g in metadata.DependencyGroups)
                        {
                            var packages = g.Packages.ToList();
                            var i = 0;
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

}