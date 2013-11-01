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
using LibGit2Sharp;
using LibGit2Sharp.Handlers;
using TrinityCore_Manager.Misc;
using TrinityCore_Manager.TCM;
using Xceed.Wpf.Toolkit;

namespace TrinityCore_Manager.TC
{

    static class TrinityCoreRepository
    {

        private const string TrinityCoreGit = "git://github.com/TrinityCore/TrinityCore.git";

        public static async Task Clone(string cloneTo, IProgress<double> progress)
        {

            await Task.Run(() =>
            {

                var thandler = new TransferProgressHandler(h =>
                {

                    progress.Report(((double)h.ReceivedObjects / h.TotalObjects) * 100);
                    //progress.Report(String.Format("{0}/{1}", (double)h.ReceivedObjects, (double)h.TotalObjects));

                    return 0;

                });

                var chandler = new CheckoutProgressHandler((path, completedSteps, totalSteps) =>
                {

                    //progress.Report(String.Format("{0}/{1}", (double)completedSteps, (double)totalSteps));

                    progress.Report(((double)completedSteps / totalSteps) * 100);

                });

                try
                {
                    Repository.Clone(TrinityCoreGit, cloneTo, false, true, thandler, chandler);
                }
                catch (LibGit2SharpException)
                {
                    //MessageBox.Show("The selected trunk location is not a valid git repository.", "Something went wrong!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
            });
        }

        public static async Task Pull(string gitDir, IProgress<string> progress)
        {

            await Task.Run(() =>
            {

                string args = String.Format("/c \"{0}\" pull -v --progress", GetGitLocation());

                Process pullProc = ProcessHelper.StartProcess("cmd.exe", gitDir, args);

                if (pullProc == null)
                    return;

                pullProc.BeginErrorReadLine();
                pullProc.BeginOutputReadLine();

                pullProc.ErrorDataReceived += (sender, e) => progress.Report(e.Data);
                pullProc.OutputDataReceived += (sender, e) => progress.Report(e.Data);

                pullProc.WaitForExit();

            });

        }

        public static string GetGitLocation()
        {

            string git;

            if (Environment.Is64BitOperatingSystem)
            {
                git = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Git",
                "bin", "git.exe");
            }
            else
            {
                git = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Git", "bin",
                "git.exe");
            }

            if (!File.Exists(git))
                return String.Empty;

            return git;

        }

    }

}
