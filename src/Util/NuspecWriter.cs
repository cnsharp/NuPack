using NuGet.Packaging.Core;
using NuGet.Packaging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CnSharp.VisualStudio.NuPack.Util
{
    public class NuspecWriter
    {
        /// <summary>
        /// Updates all supported metadata fields in a .nuspec file
        /// </summary>
        /// <param name="nuspecPath">The path to the .nuspec file.</param>
        /// <param name="newMetadata">The new metadata values to apply.</param>
        /// <param name="overwritePlaceholder">Whether overwrite placeholder or not.</param>
        /// <exception cref="FileNotFoundException">Thrown when the specified .nuspec file doesn't exist.</exception>
        /// <exception cref="InvalidDataException">Thrown when the .nuspec file has invalid format.</exception>
        public static void UpdateNuspec(string nuspecPath, ManifestMetadata newMetadata, bool overwritePlaceholder = false)
        {
            if (!File.Exists(nuspecPath))
                throw new FileNotFoundException($"Nuspec file not found: {nuspecPath}");

            // Load and parse the nuspec file
            XDocument nuspecDoc;
            using (var stream = File.OpenRead(nuspecPath))
            {
                nuspecDoc = XDocument.Load(stream);
            }

            // XNamespace ns = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd";
            var metadataNode = nuspecDoc.Descendants("metadata").FirstOrDefault()
                ?? throw new InvalidDataException("Invalid nuspec format: missing metadata node");

            // Update all standard metadata fields
            UpdateFieldIfNotPlaceholder(metadataNode, "id", newMetadata.Id, overwritePlaceholder);
            UpdateFieldIfNotPlaceholder(metadataNode, "version", newMetadata.Version?.ToString(), overwritePlaceholder);
            UpdateFieldIfNotPlaceholder(metadataNode, "title", newMetadata.Title, overwritePlaceholder);
            UpdateFieldIfNotPlaceholder(metadataNode, "authors",
                newMetadata.Authors != null ? string.Join(",", newMetadata.Authors) : null,
                overwritePlaceholder);
            UpdateFieldIfNotPlaceholder(metadataNode, "owners",
                newMetadata.Owners != null ? string.Join(",", newMetadata.Owners) : null,
                overwritePlaceholder);
            UpdateFieldIfNotPlaceholder(metadataNode, "description", newMetadata.Description, overwritePlaceholder);
            UpdateFieldIfNotPlaceholder(metadataNode, "readme", newMetadata.Readme, overwritePlaceholder);
            UpdateFieldIfNotPlaceholder(metadataNode, "releaseNotes", newMetadata.ReleaseNotes, overwritePlaceholder);
            UpdateFieldIfNotPlaceholder(metadataNode, "summary", newMetadata.Summary, overwritePlaceholder);
            UpdateFieldIfNotPlaceholder(metadataNode, "icon", newMetadata.Icon, overwritePlaceholder);
            UpdateFieldIfNotPlaceholder(metadataNode, "iconUrl", newMetadata.IconUrl?.ToString(), overwritePlaceholder);
            UpdateFieldIfNotPlaceholder(metadataNode, "licenseUrl", newMetadata.LicenseUrl?.ToString(), overwritePlaceholder);
            UpdateFieldIfNotPlaceholder(metadataNode, "projectUrl", newMetadata.ProjectUrl?.ToString(), overwritePlaceholder);
            UpdateFieldIfNotPlaceholder(metadataNode, "copyright", newMetadata.Copyright, overwritePlaceholder);
            UpdateFieldIfNotPlaceholder(metadataNode, "tags", newMetadata.Tags, overwritePlaceholder);
            UpdateFieldIfNotPlaceholder(metadataNode, "language", newMetadata.Language, overwritePlaceholder);
            UpdateFieldIfNotPlaceholder(metadataNode, "requireLicenseAcceptance",
                newMetadata.RequireLicenseAcceptance.ToString().ToLower(), overwritePlaceholder);

            UpdateLicenseMetadata(metadataNode, newMetadata.LicenseMetadata, overwritePlaceholder);
            UpdateRepositoryMetadata(metadataNode, newMetadata.Repository, overwritePlaceholder);
            UpdateDependencies(metadataNode, newMetadata.DependencyGroups);

            nuspecDoc.Save(nuspecPath);
        }

        /// <summary>
        /// Updates a specific metadata field if it's not a placeholder value.
        /// </summary>
        private static void UpdateFieldIfNotPlaceholder(
            XElement metadataNode,
            string fieldName,
            string newValue,
            bool overwritePlaceholder)
        {
            if (string.IsNullOrEmpty(newValue))
                return;

            var fieldNode = metadataNode.Element(fieldName);
            if (fieldNode == null)
            {
                metadataNode.Add(new XElement(fieldName, newValue));
                return;
            }
            if (!overwritePlaceholder && IsPlaceholder(fieldNode.Value))
            {
                return;
            }

            fieldNode.Value = newValue;
        }

        /// <summary>
        /// Updates repository metadata in the nuspec file.
        /// </summary>
        private static void UpdateRepositoryMetadata(
            XElement metadataNode,
            RepositoryMetadata repository,
            bool overwritePlaceholder)
        {
            if (repository == null)
                return;

            var repoNode = metadataNode.Element("repository");
            if (repoNode == null)
            {
                repoNode = new XElement("repository");
                metadataNode.Add(repoNode);
            }

            UpdateAttributeIfNotPlaceholder(repoNode, "url", repository.Url, overwritePlaceholder);
            UpdateAttributeIfNotPlaceholder(repoNode, "type", repository.Type, overwritePlaceholder);
            UpdateAttributeIfNotPlaceholder(repoNode, "branch", repository.Branch, overwritePlaceholder);
            UpdateAttributeIfNotPlaceholder(repoNode, "commit", repository.Commit, overwritePlaceholder);
        }

        /// <summary>
        /// Updates an XML attribute if it's not a placeholder value.
        /// </summary>
        private static void UpdateAttributeIfNotPlaceholder(
            XElement element,
            string attributeName,
            string newValue,
            bool overwritePlaceholder)
        {
            if (string.IsNullOrEmpty(newValue))
                return;

            var attribute = element.Attribute(attributeName);
            if (attribute == null || (!overwritePlaceholder && IsPlaceholder(attribute.Value)))
            {
                return;
            }

            attribute.Value = newValue;
        }

        /// <summary>
        /// Updates package dependencies in the nuspec file.
        /// </summary>
        private static void UpdateDependencies(
            XElement metadataNode,
            IEnumerable<PackageDependencyGroup> dependencyGroups)
        {
            if (dependencyGroups == null || !dependencyGroups.Any())
                return;

            var dependenciesNode = metadataNode.Element("dependencies");
            if (dependenciesNode == null)
            {
                dependenciesNode = new XElement("dependencies");
                metadataNode.Add(dependenciesNode);
            }
            else
            {
                dependenciesNode.RemoveAll();
            }

            foreach (var group in dependencyGroups)
            {
                var groupNode = new XElement("group");
                if (group.TargetFramework != null)
                {
                    groupNode.Add(new XAttribute("targetFramework", group.TargetFramework));
                }

                foreach (var dependency in group.Packages)
                {
                    var dependencyNode = new XElement("dependency");
                    dependencyNode.Add(new XAttribute("id", dependency.Id));
                    dependencyNode.Add(new XAttribute("version", dependency.VersionRange));
                    groupNode.Add(dependencyNode);
                }

                dependenciesNode.Add(groupNode);
            }
        }

        /// <summary>
        /// Determines whether a value is a placeholder (e.g., $version$).
        /// </summary>
        private static bool IsPlaceholder(string value)
        {
            return !string.IsNullOrEmpty(value) &&
                   value.StartsWith("$") &&
                   value.EndsWith("$");
        }

        public static void ChangeFiles(string nuspecFile, IEnumerable<string> removingFiles, IEnumerable<string> newFiles, string target = "")
        {
            if (!File.Exists(nuspecFile))
                throw new FileNotFoundException($"Nuspec file not found: {nuspecFile}");

            XDocument nuspecDoc;
            using (var stream = File.OpenRead(nuspecFile))
            {
                nuspecDoc = XDocument.Load(stream);
            }

            var packageNode = nuspecDoc.Element("package");
            if (packageNode == null)
                throw new InvalidDataException("Invalid nuspec format: missing package node");

            var filesNode = packageNode.Element("files");
            if (filesNode == null)
            {
                filesNode = new XElement("files");
                packageNode.Add(filesNode);
            }
            else
            {
                foreach (var file in removingFiles)
                {
                    filesNode.Elements("file")
                        .Where(e => string.Equals(e.Attribute("src")?.Value, file, System.StringComparison.OrdinalIgnoreCase))
                        .Remove();
                }
            }

            foreach (var file in newFiles)
            {
                // Avoid duplicate file entries
                bool exists = filesNode.Elements("file")
                    .Any(e => string.Equals(e.Attribute("src")?.Value, file, System.StringComparison.OrdinalIgnoreCase));
                if (!exists)
                {
                    var fileElement = new XElement("file");
                    fileElement.SetAttributeValue("src", file);
                    fileElement.SetAttributeValue("target", target);
                    filesNode.Add(fileElement);
                }
            }

            nuspecDoc.Save(nuspecFile);
        }

        /// <summary>
        /// Updates the license metadata in the nuspec file.
        /// </summary>
        private static void UpdateLicenseMetadata(
            XElement metadataNode,
            LicenseMetadata licenseMetadata,
            bool overwritePlaceholder)
        {
            if (licenseMetadata == null)
                return;

            // Remove old <license> and <licenseUrl> if present
            var licenseNode = metadataNode.Element("license");
            var licenseUrlNode = metadataNode.Element("licenseUrl");

            // Add or update <license> element
            if (!string.IsNullOrEmpty(licenseMetadata.License))
            {
                if (licenseNode == null)
                {
                    licenseNode = new XElement("license", licenseMetadata.License);
                    licenseNode.SetAttributeValue("type", licenseMetadata.Type.ToString());
                    metadataNode.Add(licenseNode);
                }
                else
                {
                    if (overwritePlaceholder || !IsPlaceholder(licenseNode.Value))
                        licenseNode.Value = licenseMetadata.License;
                    licenseNode.SetAttributeValue("type", licenseMetadata.Type.ToString());
                }
                // Remove <licenseUrl> if <license> is present
                licenseUrlNode?.Remove();
            }
            else if (licenseMetadata.LicenseUrl != null)
            {
                // Remove <license> if <licenseUrl> is present
                licenseNode?.Remove();
                if (licenseUrlNode == null)
                {
                    licenseUrlNode = new XElement("licenseUrl", licenseMetadata.LicenseUrl);
                    metadataNode.Add(licenseUrlNode);
                }
                else
                {
                    if (overwritePlaceholder || !IsPlaceholder(licenseUrlNode.Value))
                        licenseUrlNode.Value = licenseMetadata.LicenseUrl.OriginalString;
                }
            }
        }
    }
}
