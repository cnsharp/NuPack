using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CnSharp.VisualStudio.Extensions.Util;
using CnSharp.VisualStudio.NuPack.Models;

namespace CnSharp.VisualStudio.NuPack.Config
{
    public class NuPackConfig
    {
        public PackArgs PackArgs { get; set; } 

        public List<string> SymbolServers { get; set; } = new List<string>();
    }

    public class NuPackConfigHelper
    {
        private const string ConfigFileRelativePath = ".nupack\\config.xml";
        private string _configFilePath;
       
        public NuPackConfigHelper(string projectDir)
        {
            _configFilePath = Path.Combine(projectDir , ConfigFileRelativePath);
        }

        public NuPackConfig Read()
        {
            if (!File.Exists(_configFilePath))
            {
                return null;
            }

           return XmlSerializerHelper.LoadObjectFromXml<NuPackConfig>(_configFilePath);
        }

        public void Save(NuPackConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            var fi = new FileInfo(_configFilePath);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
                fi.Directory.Attributes |= FileAttributes.Hidden;
            }
            XmlSerializerHelper.SaveObjectToXml(config, _configFilePath);
        }
    }
}