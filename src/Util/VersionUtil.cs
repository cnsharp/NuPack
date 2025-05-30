using System;
using System.Text.RegularExpressions;

namespace CnSharp.VisualStudio.NuPack.Util
{
    public static class VersionUtil
    {
        public static string GetCurrentBuildVersionString(this Version baseVersion)
        {
            DateTime d = DateTime.Now;
            return new Version(baseVersion.Major, baseVersion.Minor,
                (DateTime.Today - new DateTime(2000, 1, 1)).Days,
                ((int)new TimeSpan(d.Hour, d.Minute, d.Second).TotalSeconds) / 2).ToString();
        }

        public static string GetWildCardVersionString(this Version version)
        {
            return $"{version.Major}.{version.Minor}.*";
        }

        public static bool IsAutoVersion(this string version)
        {
            return Regex.IsMatch(version, @"\d+\.\d+\.\d{4}\.\d{5}");
        }

        public static string ToSemanticVersion(this string fileVersion)
        {
            var version = Version.Parse(fileVersion);
            return $"{version.Major}.{version.Minor}.{version.Build}";
        }
    }
}
