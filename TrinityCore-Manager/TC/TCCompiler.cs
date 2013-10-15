// TrinityCore-Manager
// Copyright (C) 2013 Mitchell Kutchuk
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrinityCore_Manager.Misc;

namespace TrinityCore_Manager.TC
{
    static class TCCompiler
    {

        public static async Task<bool> Clean(string trunkDir, bool x64, IProgress<string> progress, CancellationToken token)
        {

            return await Task.Run(() =>
            {

                string args = String.Empty;

                if (x64)
                {
                    args = String.Format("\"{0}\\TrinityCore.sln\" /t:Clean", trunkDir);
                }
                else
                {
                    args = String.Format("\"{0}\\TrinityCore.sln\" /t:Clean", trunkDir);
                }

                string cpath = VisualStudio.GetCompilerPath();

                var proc = ProcessHelper.StartProcess(cpath, Path.GetDirectoryName(cpath), args);

                if (proc == null)
                    return false;

                proc.EnableRaisingEvents = true;

                proc.BeginOutputReadLine();

                proc.OutputDataReceived += (sender, e) =>
                {

                    progress.Report(e.Data);

                    if (token.IsCancellationRequested)
                    {

                        proc.Kill();
                        proc.Dispose();

                    }

                };

                proc.WaitForExit();

                return true;

            }, token);

        }

        public static async Task<bool> Compile(string trunkDir, bool x64, IProgress<string> progress, CancellationToken token)
        {

            return await Task.Run(() =>
            {

                string args;

                if (x64)
                {
                    args = String.Format("\"{0}\\TrinityCore.sln\" /t:Build /p:Configuration=Release /p:Platform=x64", trunkDir);
                }
                else
                {
                    args = String.Format("\"{0}\\TrinityCore.sln\" /t:Build /p:Configuration=Release /p:Platform=Win32", trunkDir);
                }

                string cpath = VisualStudio.GetCompilerPath();

                var proc = ProcessHelper.StartProcess(cpath, Path.GetDirectoryName(cpath), args);

                if (proc == null)
                    return false;

                int id = proc.Id;

                proc.EnableRaisingEvents = true;

                proc.BeginOutputReadLine();

                proc.OutputDataReceived += (sender, e) =>
                {

                    Thread.Sleep(50);

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

            }, token);

        }

    }
}
