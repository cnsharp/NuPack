using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Versioning;

namespace CnSharp.VisualStudio.NuPack.Models
{
    [XmlRoot("PropertyGroup")]
    public class DirectoryBuildProps : ManifestMetadata
    {
        public const string FileName = "Directory.Build.props";

        private string _file;
        private XmlSerializer _xmlSerializer;

        public static DirectoryBuildProps FromFile(string file)
        {
            return new DirectoryBuildProps().DeserializeFromFile(file);
        }


        //Package MsBuild properties see https://learn.microsoft.com/en-us/nuget/reference/msbuild-targets#pack-target

        [XmlElement("Version")]
        public string VersionString
        {
            get => Version?.ToString();
            set => Version = string.IsNullOrWhiteSpace(value) ? null : NuGetVersion.Parse(value);
        }

        [XmlElement("Authors")]
        public string AuthorsString
        {
            get => Authors != null ? string.Join(",", Authors) : string.Empty;
            set => Authors = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(m => m.Trim()).ToList();
        }

        public string Company
        {
            get => Owners != null ? string.Join(",", Owners) : string.Empty;
            set => Owners = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(m => m.Trim()).ToList();
        }

        public string PackageLicenseExpression
        {
            get => LicenseMetadata?.License;
            set => LicenseMetadata = new LicenseMetadata(LicenseType.Expression, value, null, new List<string>(), LicenseMetadata.CurrentVersion);
        }

        public string PackageLicenseFile
        {
            get => LicenseMetadata?.License;
            set => LicenseMetadata = new LicenseMetadata(LicenseType.File, value, null, new List<string>(), LicenseMetadata.CurrentVersion);
        }

        public bool PackageRequireLicenseAcceptance
        {
            get => RequireLicenseAcceptance;
            set => RequireLicenseAcceptance = value;
        }


        [XmlElement("LicenseUrl")]
        public string LicenseUrlString
        {
            get => LicenseUrl?.OriginalString;
            set => SetLicenseUrl(value);
        }

        public string PackageIcon
        {
            get => Icon;
            set => Icon = value;
        }

        public string PackageIconUrl
        {
            get => IconUrl?.OriginalString ?? string.Empty;
            set => SetIconUrl(value);
            // {
            //     if (!string.IsNullOrWhiteSpace(value) && (value.StartsWith("http://") || value.StartsWith("https://")))
            //     {
            //         SetIconUrl(value);
            //     }
            // }
        }


        public string PackageProjectUrl
        {
            get => ProjectUrl?.OriginalString;
            set => SetProjectUrl(value);
        }

        public string RepositoryUrl
        {
            get => Repository?.Url;
            set
            {
                if (Repository == null)
                    Repository = new RepositoryMetadata();
                Repository.Url = value;
            }
        }

        public string PackageReadmeFile
        {
            get => Readme;
            set => Readme = value;
        }

        public string PackageReleaseNotes
        {
            get => ReleaseNotes;
            set => ReleaseNotes = value;
        }

        public string PackageTags
        {
            get => Tags;
            set => Tags = value;
        }


        public string RepositoryType
        {
            get => Repository?.Type;
            set
            {
                if (Repository == null)
                    Repository = new RepositoryMetadata();
                Repository.Type = value;
            }
        }

        public void Save()
        {
            if (_file == null)
                return;
            var doc = new XmlDocument();
            doc.Load(_file);
            var tempNode = doc.CreateElement("temp");
            tempNode.InnerXml = ToXml();
            doc.ChildNodes[0].ChildNodes[0].InnerXml = tempNode.ChildNodes[0].InnerXml;
            doc.Save(_file);
        }


        protected string ToXml()
        {
            using (var writer = new StringWriter())
            {
                XmlSerializer.Serialize(writer, this);
                return writer.ToString();
            }
        }

        protected DirectoryBuildProps DeserializeFromFile(string file)
        {
            _file = file;
            if (!File.Exists(file))
                return null;
            var doc = new XmlDocument();
            doc.Load(_file);
            using (var reader = new StringReader("<PropertyGroup>"+doc.ChildNodes[0].ChildNodes[0].InnerXml+ "</PropertyGroup>"))
            {
                return (DirectoryBuildProps)XmlSerializer.Deserialize(reader);
            }
        }

        protected XmlSerializer XmlSerializer
        {
            get
            {
                if(_xmlSerializer != null)  return _xmlSerializer;
                var overrides = new XmlAttributeOverrides();
                var attributes = new XmlAttributes
                {
                    XmlIgnore = true
                };
                overrides.Add(typeof(ManifestMetadata), "Authors", attributes);
                overrides.Add(typeof(ManifestMetadata), "ContentFiles", attributes);
                overrides.Add(typeof(ManifestMetadata), "DependencyGroups", attributes);
                overrides.Add(typeof(ManifestMetadata), "FrameworkReferences", attributes);
                overrides.Add(typeof(ManifestMetadata), "FrameworkReferenceGroups", attributes);
                overrides.Add(typeof(ManifestMetadata), "LicenseMetadata", attributes);
                overrides.Add(typeof(ManifestMetadata), "LicenseUrl", attributes);
                overrides.Add(typeof(ManifestMetadata), "MinClientVersion", attributes);
                overrides.Add(typeof(ManifestMetadata), "IconUrl", attributes);
                overrides.Add(typeof(ManifestMetadata), "Owners", attributes);
                overrides.Add(typeof(ManifestMetadata), "PackageAssemblyReferences", attributes);
                overrides.Add(typeof(ManifestMetadata), "PackageTypes", attributes);
                overrides.Add(typeof(ManifestMetadata), "ProjectUrl", attributes);
                overrides.Add(typeof(ManifestMetadata), "Repository", attributes);
                overrides.Add(typeof(ManifestMetadata), "Summary", attributes);
                overrides.Add(typeof(ManifestMetadata), "Version", attributes);

                _xmlSerializer = new XmlSerializer(GetType(), overrides);
                return _xmlSerializer;
            }
        }

        public IEnumerable<string> GetValuedProperties()
        {
            var props = GetType().GetProperties().ToList();
            foreach (var prop in props)
            {
                var v = prop.GetValue(this);
                if (!string.IsNullOrWhiteSpace(v?.ToString()))
                    yield return prop.Name;
            }
        }
    }
}