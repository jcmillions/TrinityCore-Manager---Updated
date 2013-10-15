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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TrinityCore_Manager.Misc
{
    static class ProcessHelper
    {

        public static Process GetFirstProcessByName(string name)
        {

            var processes = Process.GetProcessesByName(name);

            if (processes.Length > 0)
                return processes[0];

            return null;

        }

        public static bool KillProcess(string name)
        {

            var processes = Process.GetProcessesByName(name);

            foreach (var process in processes)
            {

                try
                {

                    process.Kill();

                    return true;

                }
                catch (Exception)
                {
                }

            }

            return false;

        }

        public static bool KillProcess(int pid)
        {

            try
            {

                var proc = Process.GetProcessById(pid);

                proc.Kill();

                return true;
            
            }
            catch (Exception)
            {
            }

            return false;

        }

        public static bool ProcessExists(string name)
        {
            return Process.GetProcessesByName(name).Length > 0;
        }

        public static bool ProcessExists(int pid)
        {

            try
            {

                Process.GetProcessById(pid);

                return true;

            }
            catch (Exception)
            {
            }

            return false;

        }

        /// <summary>
        /// Start a process
        /// </summary>
        /// <param name="exeLoc">The location of the exe file</param>
        /// <param name="workingDir">The directory in which to start the exe from</param>
        /// <param name="arguments">Optional arguments to pass</param>
        /// <returns>The process that has been started</returns>
        public static Process StartProcess(string exeLoc, string workingDir, string arguments = "")
        {

            var psi = new ProcessStartInfo(exeLoc);

            psi.WorkingDirectory = workingDir;

            psi.UseShellExecute = false;

            psi.CreateNoWindow = true;

            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.RedirectStandardInput = true;

            psi.Arguments = arguments;

            return StartProcess(psi);
        }

        public static Process StartProcess(ProcessStartInfo psi)
        {

            var proc = new Process();
            proc.StartInfo = psi;
            proc.EnableRaisingEvents = true;

            try
            {
                proc.Start();
                return proc;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

    }
}
