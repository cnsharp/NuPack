using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using CnSharp.VisualStudio.Extensions.Projects;
using CnSharp.VisualStudio.Extensions.Util;
using EnvDTE;

namespace CnSharp.VisualStudio.NuPack.Util
{
    public static class DirectoryBuildUtil
    {
        public static DirectoryBuildProps GetDirectoryBuildProps(this Solution solution)
        {
            var file = solution.GetDirectoryBuildPropsPath();
            return !File.Exists(file) ? null : new DirectoryBuildProps(file);
        }

        public static string GetDirectoryBuildPropsPath(this Solution solution)
        {
            return Path.Combine(Path.GetDirectoryName(solution.FullName), DirectoryBuildProps.FileName);
        }
    }

    public class DirectoryBuildProps : IPackageCommonMetadata
    {
        private readonly string _file;

        public DirectoryBuildProps()
        {
            
        }

        public DirectoryBuildProps(string file)
        {
            _file = file;
            if(!File.Exists(file)) throw new FileNotFoundException("not found",file);
            var doc = new XmlDocument();
            doc.Load(_file);
            var parent = doc.ChildNodes[0].ChildNodes[0];
            var props = GetType().GetProperties().ToList();
            foreach (XmlNode node in parent.ChildNodes)
            {
                var prop = props.Find(m => m.Name == node.Name);
                if (prop == null) continue;
                prop.SetValue(this,node.InnerXml,null);
            }
        }

        public void Save()
        {
            if (_file == null)
                return;
            SaveTo(_file);
        }

        public void SaveTo(string file)
        {
            var doc = new XmlDocument();
            doc.LoadXml(XmlTemplate);
            var tempNode = doc.CreateElement("temp");
            tempNode.InnerXml = XmlSerializerHelper.GetXmlStringFromObject(this);
            doc.ChildNodes[0].ChildNodes[0].InnerXml = tempNode.ChildNodes[0].InnerXml;
            doc.Save(file);
        }

        public IEnumerable<string> GetValuedProperties()
        {
            var props = GetType().GetProperties().ToList();
            foreach (var prop in props)
            {
                var v = prop.GetValue(this);
                if (!string.IsNullOrWhiteSpace(v?.ToString()))
                    yield return prop.Name;
            }
        }

        public const string FileName = "Directory.Build.props";
        private const string XmlTemplate = @"<Project>
  <PropertyGroup>
  </PropertyGroup>
</Project>";

        #region Implementation of IPackageCommonMetadata

        public string Version { get; set; }
        public string Copyright { get; set; }
        public string Product { get; set; }
        public string Company { get; set; }
        public string Trademark { get; set; }
        public string Authors { get; set; }
        public string Owners { get; set; }

        #endregion
    }
}
