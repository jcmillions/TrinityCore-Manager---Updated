using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;
using TrinityCore_Manager.Misc;

namespace TrinityCore_Manager.TC
{
    static class CMake
    {

        public static string GetCMakeBinLocation()
        {

            RegistryKey kitWare;

            if (Environment.Is64BitOperatingSystem)
                kitWare = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Kitware");
            else
                kitWare = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Kitware");

            if (kitWare == null)
                return null;

            string cMakeFolder = String.Empty;

            foreach (string subkey in kitWare.GetSubKeyNames())
            {

                cMakeFolder = subkey;
            
                break;
            
            }

            if (string.IsNullOrEmpty(cMakeFolder))
                return null;

            using (RegistryKey cMake = kitWare.OpenSubKey(cMakeFolder))
            {

                if (cMake == null)
                    return null;

                string path = cMake.GetValue(null).ToString();
                
                if (path == String.Empty)
                {
                    return null;
                }

                kitWare.Dispose();

                return Path.Combine(path, "bin");

            }

        }

        public static async Task<bool> Generate(string sourceDir, string destDir, bool x64, IProgress<string> progress, CancellationToken token)
        {

            return await Task.Run(() =>
            {

                string cMakeBinLoc = GetCMakeBinLocation();

                if (!Directory.Exists(cMakeBinLoc))
                    return false;

                string cmake = Path.Combine(cMakeBinLoc, "cmake.exe");

                if (!File.Exists(cmake))
                    return false;

                string args = String.Empty;

                if (x64)
                {
                    if (VisualStudio.Version == VSVersion.VisualStudio11)
                        args = String.Format("-G \"Visual Studio 11 Win64\" \"{0}\"", sourceDir);
                    else if (VisualStudio.Version == VSVersion.VisualStudio10)
                        args = String.Format("-G \"Visual Studio 10 Win64\" \"{0}\"", sourceDir);
                }
                else
                {
                    if (VisualStudio.Version == VSVersion.VisualStudio11)
                        args = String.Format("-G \"Visual Studio 11\" \"{0}\"", sourceDir);
                    else if (VisualStudio.Version == VSVersion.VisualStudio10)
                        args = String.Format("-G \"Visual Studio 10 \" \"{0}\"", sourceDir);
                }

                if (!String.IsNullOrEmpty(args))
                {

                    var proc = ProcessHelper.StartProcess(cmake, destDir, args);

                    if (proc == null)
                        return false;

                    int id = proc.Id;

                    proc.EnableRaisingEvents = true;

                    proc.BeginOutputReadLine();
                    proc.BeginErrorReadLine();

                    proc.OutputDataReceived += (sender, e) =>
                    {

                        progress.Report(e.Data);

                        if (token.IsCancellationRequested)
                        {

                            if (ProcessHelper.ProcessExists(id))
                            {
                                proc.Kill();
                                proc.Dispose();
                            }

                        }

                    };

                    proc.ErrorDataReceived += (sender, e) =>
                    {

                        progress.Report(e.Data);

                        if (token.IsCancellationRequested)
                        {

                            if (ProcessHelper.ProcessExists(id))
                            {
                                proc.Kill();
                                proc.Dispose();
                            }

                        }

                    };

                    proc.WaitForExit();

                    return true;

                }

                return false;

            }, token);

        }

    }
}
