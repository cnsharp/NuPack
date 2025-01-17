namespace CnSharp.VisualStudio.NuPack.Models
{
    /**
     * Arguments of 'dotnet pack' command
     * see:https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-pack
     */
    public class PackArgs
    {
        public string OutputDirectory { get; set; }
        public bool IncludeSymbols { get; set; }
        public bool IncludeSource { get; set; }

        public bool NoBuild { get; set; }
        public bool NoDependencies { get; set; }
    }
}
