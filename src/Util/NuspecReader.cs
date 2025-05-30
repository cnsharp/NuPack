using NuGet.Packaging.Core;
using NuGet.Packaging;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CnSharp.VisualStudio.NuPack.Util
{
    public static class NuspecReader
    {
        public static ManifestMetadata ReadMetadata(string nuspecPath, bool validateSchema = true)
        {
            if (!File.Exists(nuspecPath))
                throw new FileNotFoundException($"Nuspec file not found: {nuspecPath}");

            try
            {
                using (var stream = File.OpenRead(nuspecPath))
                {
                    var manifest = Manifest.ReadFrom(stream, validateSchema);
                    return manifest.Metadata;
                }
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is PackagingException)
            {
                throw new InvalidDataException($"Invalid nuspec format: {ex.Message}", ex);
            }
        }

        public static ManifestMetadata ReadRawMetadata(string nuspecPath)
        {
            var metadata = new ManifestMetadata();
            var doc = XDocument.Load(nuspecPath);
            var elements = doc.Element("package").Element("metadata").Elements().ToList();
            var props = typeof(ManifestMetadata).GetProperties()
                .Where(p => p.PropertyType.IsValueType ||
                            p.PropertyType == typeof(string))
                .ToList();
            foreach (var prop in props)
            {
                var v = elements.FirstOrDefault(m => m.Name.LocalName.Equals(prop.Name, StringComparison.InvariantCultureIgnoreCase))?.Value;
                if (prop.PropertyType == typeof(string))
                {
                    if (v != null)
                    {
                        prop.SetValue(metadata, v, null);
                    }
                }
                else if (prop.PropertyType == typeof(bool))
                {
                    if (v != null)
                    {
                        prop.SetValue(metadata, v.Equals("true", StringComparison.InvariantCultureIgnoreCase), null);
                    }
                }
                else if (prop.PropertyType == typeof(IEnumerable<string>))
                {
                    if (!string.IsNullOrWhiteSpace(v))
                    {
                        prop.SetValue(metadata, v.Split(new [] {','}, StringSplitOptions.RemoveEmptyEntries), null);
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
    }
}