using System.Management;

namespace CnSharp.VisualStudio.NuPack
{
    public class Common
    {
        public const string ProductName = "NuPack";

        public static readonly string[] SupportedProjectTypes = { ".csproj", ".vbproj",".fsproj" };

        public static string GetOrganization()
        {
            var c = new ManagementClass("Win32_OperatingSystem");
            foreach (var o in c.GetInstances())
            {
                //Console.WriteLine("Registered User: {0}, Organization: {1}", o["RegisteredUser"], o["Organization"]);
                if (!string.IsNullOrWhiteSpace(o["Organization"]?.ToString()))
                    return o["Organization"].ToString();
            }
            return null;
            //return (string)Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion", "RegisteredOrganization", "");
        }
    }
}