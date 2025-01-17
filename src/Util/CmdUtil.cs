using System;
using System.Diagnostics;
using System.Threading;

namespace CnSharp.VisualStudio.NuPack.Util
{
    public class CmdUtil
    {
        //cmd issues goes here:https://stackoverflow.com/questions/139593/processstartinfo-hanging-on-waitforexit-why
        public static bool RunCmd(string script, Action<string> outputMessageHandler = null, Action<string> errorMessageHandler = null)
        {
            using (var process = new Process())
            {
                process.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";
                //process.StartInfo.Arguments = arguments;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;

                using (var outputWaitHandle = new AutoResetEvent(false))
                using (var errorWaitHandle = new AutoResetEvent(false))
                {
                    process.OutputDataReceived += (sender, e) => {
                        if (e.Data == null)
                        {
                            outputWaitHandle.Set();
                        }
                        else
                        {
                            outputMessageHandler?.Invoke(e.Data);
                        }
                    };
                    process.ErrorDataReceived += (sender, e) =>
                    {
                        if (e.Data == null)
                        {
                            errorWaitHandle.Set();
                        }
                        else
                        {
                            errorMessageHandler?.Invoke(e.Data);
                        }
                    };

                    process.Start();
                    using (var writer = process.StandardInput)
                    {
                        if (writer.BaseStream.CanWrite)
                        {
                            writer.WriteLine(script);
                        }
                    }

                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    process.WaitForExit();
                    return process.ExitCode == 0;
                }
            }
        }

        public static bool RunDotnet(string arguments, Action<string> outputMessageHandler = null, Action<string> errorMessageHandler = null)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = new Process { StartInfo = startInfo })
            {
                using (var outputWaitHandle = new AutoResetEvent(false))
                using (var errorWaitHandle = new AutoResetEvent(false))
                {
                    process.OutputDataReceived += (sender, e) => {
                        if (e.Data == null)
                        {
                            outputWaitHandle.Set();
                        }
                        else
                        {
                            outputMessageHandler?.Invoke(e.Data);
                        }
                    };
                    process.ErrorDataReceived += (sender, e) =>
                    {
                        if (e.Data == null)
                        {
                            errorWaitHandle.Set();
                        }
                        else
                        {
                            errorMessageHandler?.Invoke(e.Data);
                        }
                    };

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    process.WaitForExit();
                    return process.ExitCode == 0;
                }
            }
        }
    }
}
