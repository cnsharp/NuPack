using System;
using System.Linq;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Versioning;

namespace CnSharp.VisualStudio.NuPack.Models
{
    public class ManifestMetadataViewModel : ManifestMetadata
    {
        private readonly ManifestMetadata _metadata;
        private string _licenseUrlString;
        private string _projectUrlString;

        public ManifestMetadataViewModel()
        {
        }

        public ManifestMetadataViewModel(ManifestMetadata metadata)
        {
            _metadata = metadata;
            Authors = metadata.Authors;
            ContentFiles = metadata.ContentFiles;
            Copyright = metadata.Copyright;
            DependencyGroups = metadata.DependencyGroups;
            Description = metadata.Description;
            DevelopmentDependency = metadata.DevelopmentDependency;
            Id = metadata.Id;
            Icon = metadata.Icon;
            Language = metadata.Language;
            Owners = metadata.Owners;
            Readme = metadata.Readme;
            ReleaseNotes = metadata.ReleaseNotes;
            Repository = metadata.Repository;
            RequireLicenseAcceptance = metadata.RequireLicenseAcceptance;
            Serviceable = metadata.Serviceable;
            Summary = metadata.Summary;
            Tags = metadata.Tags;
            Title = metadata.Title;
            LicenseMetadata = metadata.LicenseMetadata;

            // licenseUrl is deprecated, keep it just for old projects when LicenseMetadata is null
            if (metadata.LicenseMetadata == null && metadata.LicenseUrl != null)
            {
                LicenseUrlString = metadata.LicenseUrl?.OriginalString;
            }

            _projectUrlString = metadata.ProjectUrl?.OriginalString ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(_projectUrlString))
                SetProjectUrl(_projectUrlString);
            Version = metadata.Version;
        }


        public string AuthorsString
        {
            get => Authors != null ? string.Join(",", Authors) : string.Empty;
            set => Authors = value.Split(',').ToList();
        }

        [Obsolete]
        public string OwnersString
        {
            get => Owners != null ? string.Join(",", Owners) : string.Empty;
            set => Owners = value.Split(',').ToList();
        }


        public string PackageLicenseExpression
        {
            get => LicenseMetadata?.License ?? string.Empty;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    LicenseMetadata = null;
                    return;
                }

                LicenseMetadata = new LicenseMetadata(LicenseType.Expression, value, null, null,
                    LicenseMetadata.CurrentVersion);
            }
        }

        public string LicenseUrlString
        {
            get => _licenseUrlString;
            set
            {
                _licenseUrlString = value;
                if (!string.IsNullOrWhiteSpace(value))
                    SetLicenseUrl(value);
            }
        }

        public string IconUrlString
        {
            get => Icon ?? (IconUrl?.OriginalString ?? string.Empty);
            set
            {
                Icon = value;
                if (!string.IsNullOrWhiteSpace(value) && (value.StartsWith("http://") || value.StartsWith("https://")))
                { 
                    SetIconUrl(value);
                    Icon = null;
                }
            }
        }

        public bool IconChanged => Icon != _metadata.Icon;


        public string ProjectUrlString
        {
            get => _projectUrlString;
            set
            {
                _projectUrlString = value;
                if (!string.IsNullOrWhiteSpace(value))
                    SetProjectUrl(value);
            }
        }

        public bool ReadmeChanged => Readme != _metadata.Readme;

        public string RepositoryType
        {
            get => Repository?.Type ?? string.Empty;
            set
            {
                if (Repository != null)
                {
                    Repository.Type = value;
                    return;
                }

                Repository = new RepositoryMetadata(value, null, null, null);
            }
        }

        public string RepositoryUrl
        {
            get => Repository?.Url ?? string.Empty;
            set
            {
                if (Repository != null)
                {
                    Repository.Url = value;
                    return;
                }

                Repository = new RepositoryMetadata(null, value, null, null);
            }
        }

        public string VersionString
        {
            get => Version.OriginalVersion;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    Version = null;
                }

                try
                {
                    Version = new NuGetVersion(value);
                }
                catch
                {
                    //ignored
                }
            }
        }
    }
}