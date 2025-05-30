using System.Collections.Generic;

namespace CnSharp.VisualStudio.NuPack.Models
{
    internal class NuspecMsbuildMapper
    {
        public static readonly Dictionary<string, string> NuspecToMsBuildMap = new Dictionary<string, string>
        {
            { "id", "PackageId" },
            { "version", "Version" },
            { "copyright", "Copyright" },
            { "authors", "Authors" },
            { "owners", "Company" },
            { "description", "Description" },
            { "license", "PackageLicenseExpression" },
            { "repositoryUrl", "RepositoryUrl" }, 
            { "repositoryType", "RepositoryType" },
            { "tags", "PackageTags" },
            { "icon", "PackageIcon" }
        };

    }
}
