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
using System.Threading.Tasks;

namespace TrinityCore_Manager.Misc
{
    static class FileHelper
    {

        public static string GenerateTempDirectory()
        {

            string newDir = Path.GetTempFileName() + ".tcm";

            Directory.CreateDirectory(newDir);

            return newDir;

        }

        public static void DeleteDirectory(string directory)
        {
            string[] files = Directory.GetFiles(directory);
            string[] dirs = Directory.GetDirectories(directory);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(directory, true);

        }

        public static void CopyDirectory(string directory, string dest)
        {

            if (!Directory.Exists(dest))
                Directory.CreateDirectory(dest);

            string[] files = Directory.GetFiles(directory);

            foreach (string file in files)
            {

                string filename = Path.GetFileName(file);
                string dFolder = Path.Combine(dest, filename);

                File.Copy(file, dFolder, true);

            }

            string[] dirs = Directory.GetDirectories(directory);

            foreach (string dir in dirs)
            {

                string dirname = Path.GetFileName(dir);
                string dFolder = Path.Combine(dest, dirname);
                CopyDirectory(dir, dFolder);

            }

        }

    }
}
