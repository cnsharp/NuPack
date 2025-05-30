using System.IO;
using CnSharp.VisualStudio.NuPack.Models;
using EnvDTE;

namespace CnSharp.VisualStudio.NuPack.Util
{
    public static class DirectoryBuildUtil
    {
        public static DirectoryBuildProps GetDirectoryBuildProps(this Solution solution)
        {
            var file = solution.GetDirectoryBuildPropsPath();
            return !File.Exists(file) ? null : DirectoryBuildProps.FromFile(file);
        }

        public static string GetDirectoryBuildPropsPath(this Solution solution)
        {
            return Path.Combine(Path.GetDirectoryName(solution.FullName), DirectoryBuildProps.FileName);
        }
    }
}