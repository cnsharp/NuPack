using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace CnSharp.VisualStudio.NuPack.Util
{
    public class NuGetConfigReader
    {
        public const string ConfigFileName = "NuGet.config";
        private const string NuGet = "NuGet";
        private const string VsOfflinePackages = "Microsoft Visual Studio Offline Packages";
        private static readonly string NuGetConfigFileSubDir = Path.Combine(NuGet, ConfigFileName);
        private static readonly string AppDataDir = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData),
           NuGetConfigFileSubDir);
        private readonly string _solutionDirectory;
        private readonly string _projectDirectory;

        public NuGetConfigReader(string solutionDirectory = null, string projectDirectory = null)
        {
            _solutionDirectory = solutionDirectory;
            _projectDirectory = projectDirectory;
        }


        public List<NuGetSource> GetNuGetSources()
        {
            var sources = new List<NuGetSource>();

            // User-level config
            if (File.Exists(AppDataDir))
            {
                var userLevelSources = ReadConfigFile(AppDataDir);
                FilterSources(userLevelSources, sources);
            }

            // Solution-level config
            if (!string.IsNullOrEmpty(_solutionDirectory))
            {
                var solutionConfigPath = Path.Combine(_solutionDirectory, ConfigFileName);
                if (File.Exists(solutionConfigPath))
                {
                    var solutionLevelSources = ReadConfigFile(solutionConfigPath);
                    FilterSources(solutionLevelSources, sources);
                }
            }

            // Project-level config
            if (!string.IsNullOrEmpty(_projectDirectory))
            {
                var projectConfigPath = Path.Combine(_projectDirectory, ConfigFileName);
                if (File.Exists(projectConfigPath))
                {
                    var projectLevelSources = ReadConfigFile(projectConfigPath);
                    FilterSources(projectLevelSources, sources);
                }
            }

            // Machine-level config
            // var machineConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), NuGet);
            // if (Directory.Exists(machineConfigPath))
            // {
            //   
            // }
            return sources;
        }

        private static void FilterSources(List<NuGetSource> subSources, List<NuGetSource> sources)
        {
            subSources.ForEach(s =>
            {
                if (s.Name != VsOfflinePackages && sources.All(x => x.Name != s.Name))
                {
                    sources.Add(s);
                }
            });
        }

        public static List<NuGetSource> ReadConfigFile(string configFileName)
        {
            var sources = new List<NuGetSource>();

            if (!File.Exists(configFileName))
            {
                throw new FileNotFoundException($"The config file {configFileName} does not exist.");
            }

            var doc = new XmlDocument();
            doc.Load(configFileName);

            var packageSourcesNode = doc.SelectSingleNode("//configuration/packageSources");
            if (packageSourcesNode != null)
            {
                foreach (XmlNode sourceNode in packageSourcesNode.ChildNodes)
                {
                    if (sourceNode.NodeType == XmlNodeType.Element)
                    {
                        var source = new NuGetSource
                        {
                            Url = sourceNode.Attributes["value"]?.InnerText,
                            Name = sourceNode.Attributes["key"]?.InnerText
                        };
                        sources.Add(source);
                    }
                }
            }

            return sources;
        }
    }

    public class NuGetSource
    {
        public string Url { get; set; }
        public string Name { get; set; }
    }

}
