using System.Collections.Concurrent;
using System.Collections.Generic;
using EnvDTE;

namespace CnSharp.VisualStudio.NuPack.Extensions
{
    public class SolutionDataCache : ConcurrentDictionary<string,SolutionProperties>
    {
        private static SolutionDataCache instance;
        protected SolutionDataCache()
        {
           
        }

        public static SolutionDataCache Instance => instance ?? (instance = new SolutionDataCache());

        public SolutionProperties GetSolutionProperties(string solutionFile)
        {
            SolutionProperties sp;
            while (!TryGetValue(solutionFile,out sp))
            {
                System.Threading.Thread.Sleep(500);
            }
            return sp;
        }
    }

    public class SolutionProperties
    {
        public List<Project> Projects { get; set; } = new List<Project>();

        public void AddProject(Project project)
        {
            Projects.Add(project);
        }

        public void RemoveProject(Project project)
        {
            Projects.Remove(project);
        }
    }
}
