using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace CnSharp.VisualStudio.NuPack.Util
{

    public class License
    {
        public string Reference { get; set; }
        public bool IsDeprecatedLicenseId { get; set; }
        public string DetailsUrl { get; set; }
        public int ReferenceNumber { get; set; }
        public string Name { get; set; }
        public string LicenseId { get; set; }
        public List<string> SeeAlso { get; set; }
        public bool IsOsiApproved { get; set; }
        public bool? IsFsfLibre { get; set; }
    }

    public class LicenseList
    {
        public string LicenseListVersion { get; set; }
        public List<License> Licenses { get; set; }
    }

    public class LicenseReader
    {
        public List<License> ReadLicenses(string filePath)
        {
            var jsonContent = File.ReadAllText(filePath);
            var licenseList = JsonConvert.DeserializeObject<LicenseList>(jsonContent);
            return licenseList.Licenses;
        }

        public static List<License> ReadLicenses()
        {
            var assembly = typeof(LicenseReader).Assembly;
            var resourceName = "CnSharp.VisualStudio.NuPack.Resources.licenses.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var jsonContent = reader.ReadToEnd();
                var licenseList = JsonConvert.DeserializeObject<LicenseList>(jsonContent);
                return licenseList.Licenses;
            }
        }
    }

}