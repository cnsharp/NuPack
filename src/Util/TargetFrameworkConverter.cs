using System;
using System.Linq;

namespace CnSharp.VisualStudio.NuPack.Util
{
    public static class TargetFrameworkConverter
    {
        public static string ConvertToNuGetTargetFramework(string targetFrameworkMoniker)
        {
            if (string.IsNullOrWhiteSpace(targetFrameworkMoniker))
            {
                throw new ArgumentException("TargetFrameworkMoniker cannot be null or empty.");
            }

            var parts = targetFrameworkMoniker.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Trim())
                .ToArray();

            if (parts.Length != 2)
            {
                throw new ArgumentException($"Invalid TargetFrameworkMoniker format: '{targetFrameworkMoniker}'");
            }

            var framework = parts[0];
            var versionPart = parts[1];

            if (!versionPart.StartsWith("Version=", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException($"Invalid version format in TargetFrameworkMoniker: '{targetFrameworkMoniker}'");
            }

            var version = versionPart.Substring("Version=".Length).TrimStart('v');

            switch (framework)
            {
                // .NET Framework 4.7.2 -> net472
                case ".NETFramework":
                    return $"net{version.Replace(".", "")}";
                // .NETCoreApp 3.1 or previous -> netcoreapp3.1
                case ".NETCoreApp" when Version.Parse(version).Major < 5:
                    return $"netcoreapp{version}";
                // .NETCoreApp 5+ -> net5.0, net6.0, net8.0
                case ".NETCoreApp" when Version.Parse(version).Major >= 5:
                    return $"net{version}";
                // .NET 5+ -> net5.0, net6.0, net8.0
                case ".NET":
                    return $"net{version}";
                // .NET Standard -> netstandard2.0
                case ".NETStandard":
                    return $"netstandard{version}";
                default:
                    return targetFrameworkMoniker; // Return original if no match found
            }
        }
    }
}