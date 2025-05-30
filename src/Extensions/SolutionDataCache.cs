using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using CnSharp.VisualStudio.Extensions;
using EnvDTE;

namespace CnSharp.VisualStudio.NuPack.Extensions
{
    public class SolutionDataCache : ConcurrentDictionary<string, SolutionProperties>
    {
        private static SolutionDataCache _instance;
        protected SolutionDataCache()
        {

        }

        public static SolutionDataCache Instance => _instance ?? (_instance = new SolutionDataCache());

        public SolutionProperties GetSolutionProperties(string solutionFile)
        {
            SolutionProperties sp;
            while (!TryGetValue(solutionFile, out sp))
            {
                System.Threading.Thread.Sleep(500);
            }
            return sp;
        }
    }

    public class SolutionProperties
    {
        private List<Project> _projects;

        public List<Project> Projects
        {
            get => _projects;
            set
            {
                _projects = value;
                if (_projects != null)
                {
                    ClassicProjects.AddRange(_projects.Where(p => p.IsNetFrameworkProject()));
                    SdkBasedProjects.AddRange(_projects.Where(p => p.IsSdkBased()));
                }
                else
                {
                    ClassicProjects.Clear();
                    SdkBasedProjects.Clear();
                }
            }
        }

        public List<Project> ClassicProjects { get; private set; } = new List<Project>();
        public List<Project> SdkBasedProjects { get; private set; } = new List<Project>();
        public bool HasClassicProjects => ClassicProjects?.Any() == true;
        public bool HasSdkBasedProjects => SdkBasedProjects?.Any() == true;

        public void AddProject(Project project)
        {
            if (_projects == null) _projects = new List<Project>();
            _projects.Add(project);
            if (project.IsNetFrameworkProject())
            {
                ClassicProjects.Add(project);
            }
            else if (project.IsSdkBased())
            {
                SdkBasedProjects.Add(project);
            }
        }

        public void RemoveProject(Project project)
        {
            _projects.Remove(project);
            if (project.IsNetFrameworkProject())
            {
                ClassicProjects.Remove(project);
            }
            else if (project.IsSdkBased())
            {
                SdkBasedProjects.Remove(project);
            }
        }
    }
}
