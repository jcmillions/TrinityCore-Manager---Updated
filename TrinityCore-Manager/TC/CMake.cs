using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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


            string[] subKeys = kitWare.GetSubKeyNames();


            if (subKeys.Length == 0)
                return null;

            string cMakeFolder = subKeys[0];

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

        public static Task<bool> Generate(string sourceDir, string destDir, bool x64, IProgress<string> progress, CancellationToken token)
        {

            return Task.Run(() =>
            {

                string cMakeBinLoc = GetCMakeBinLocation();

                if (!Directory.Exists(cMakeBinLoc))
                    return false;

                string cmake = Path.Combine(cMakeBinLoc, "cmake.exe");

                if (!File.Exists(cmake))
                    return false;

                string args = String.Empty;

                VSVersion ver = VisualStudio.Version;

                if (x64)
                {
                    if (ver == VSVersion.VisualStudio12)
                        args = String.Format("/C \"\"{0}\" -G \"Visual Studio 12 Win64\" \"{1}\"\"", cmake, sourceDir);
                    else if (ver == VSVersion.VisualStudio11)
                        args = String.Format("/C \"\"{0}\" -G \"Visual Studio 11 Win64\" \"{1}\"\"", cmake, sourceDir);
                    else if (ver == VSVersion.VisualStudio10)
                        args = String.Format("/C \"\"{0}\" -G \"Visual Studio 10 Win64\" \"{1}\"\"", cmake, sourceDir);
                }
                else
                {
                    if (ver == VSVersion.VisualStudio12)
                        args = String.Format("/C \"\"{0}\" -G \"Visual Studio 12\" \"{1}\"\"", cmake, sourceDir);
                    else if (ver == VSVersion.VisualStudio11)
                        args = String.Format("/C \"\"{0}\" -G \"Visual Studio 11\" \"{1}\"\"", cmake, sourceDir);
                    else if (ver == VSVersion.VisualStudio10)
                        args = String.Format("/C \"\"{0}\" -G \"Visual Studio 10\" \"{1}\"\"", cmake, sourceDir);
                }

                if (!String.IsNullOrEmpty(args))
                {

                    var envPath = Environment.GetEnvironmentVariable("PATH");

                    if (envPath == null)
                        return false;

                    var envPathSplit = envPath.Split(';');


                    string gitLoc = TrinityCoreRepository.GetGitLocation();

                    if (string.IsNullOrEmpty(gitLoc))
                        return false;

                    gitLoc = gitLoc.Replace("git.exe", String.Empty);

                    var gitPath = envPathSplit.FirstOrDefault(p => p.Equals(gitLoc, StringComparison.OrdinalIgnoreCase));

                    var psi = new ProcessStartInfo("cmd.exe");

                    if (string.IsNullOrEmpty(gitPath))
                    {
                        psi.EnvironmentVariables["PATH"] = string.Format("{0};{1}", envPath, gitLoc);
                    }

                    psi.WorkingDirectory = destDir;

                    psi.UseShellExecute = false;

                    psi.CreateNoWindow = true;

                    psi.RedirectStandardOutput = true;
                    psi.RedirectStandardError = true;
                    psi.RedirectStandardInput = true;

                    psi.Arguments = args;

                    Process proc = ProcessHelper.StartProcess(psi);

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
