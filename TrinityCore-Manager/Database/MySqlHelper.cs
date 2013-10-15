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
using MySql.Data.MySqlClient;
using TrinityCore_Manager.TCM;
using System.Diagnostics;
using TrinityCore_Manager.Misc;

namespace TrinityCore_Manager.Database
{
    static class MySqlHelper
    {

        public static async Task BackupDatabase(this MySqlDatabase db, string outputFile, CancellationToken token)
        {

            await Task.Run(() =>
            {

                string mdump = TCManager.MySQLDumpLocation;
                string args = String.Format("/c \"{0}\" -u {1} -p{2} -h {3} -P {4} {5}", mdump, db.Username, db.Password, db.Host, db.Port, db.DatabaseName);

                if (!File.Exists(mdump))
                    return;

                Process dumpProc = ProcessHelper.StartProcess("cmd.exe", Path.GetDirectoryName(outputFile), args);

                if (dumpProc == null)
                    return;

                StreamReader reader = dumpProc.StandardOutput;
                
                StreamWriter writer = new StreamWriter(outputFile);

                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    writer.WriteLine(line);
                }

                dumpProc.WaitForExit();

                writer.Flush();
                writer.Close();
                writer.Dispose();

            });

        }

        public static Task CreateDatabaseAsync(this MySqlDatabase db)
        {

            return Task.Run(() =>
            {

                MySqlConnectionStringBuilder connStr = new MySqlConnectionStringBuilder();
                connStr.Server = db.Host;
                connStr.Port = (uint)db.Port;
                connStr.UserID = db.Username;
                connStr.Password = db.Password;
                
                string connString = connStr.ToString();

                string nonQuery = String.Format("DROP DATABASE IF EXISTS {0}; CREATE DATABASE {0};", db.DatabaseName);

                using (MySqlConnection conn = new MySqlConnection(connString))
                {

                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(nonQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                
                }

            });

        }

        public static async Task Restore(this MySqlDatabase db, string inputFile, IProgress<int> progress, CancellationToken token)
        {

            await db.ExecuteScript("SET GLOBAL max_allowed_packet = 1024*1024*1024;");

            await Task.Run(() =>
            {

                using (var backup = new MySqlBackup(db.ConnectionString))
                {

                    var tcs = new TaskCompletionSource<bool>();

                    backup.ImportInfo.FileName = inputFile;
                    backup.ImportInfo.AsynchronousMode = true;
                    backup.ImportInfo.AutoCloseConnection = true;
                    backup.ImportInfo.IgnoreSqlError = true;

                    backup.Import();
                    backup.ImportProgressChanged += (sender, e) => progress.Report(e.PercentageCompleted);
                    backup.ImportCompleted += (sender, e) => tcs.SetResult(true);

                    tcs.Task.Wait();

                }

            });

        }

        public static async Task ExecuteScript(this MySqlDatabase db, FileInfo sqlFile)
        {
            await ExecuteScript(db, File.ReadAllText(sqlFile.FullName));
        }

        public static async Task ExecuteScript(this MySqlDatabase db, string sql)
        {

            await Task.Run(() =>
            {

                using (var conn = new MySqlConnection(db.ConnectionString))
                {

                    var script = new MySqlScript(conn, sql);

                    var tcs = new TaskCompletionSource<bool>();

                    script.ScriptCompleted += (sender, e) => tcs.SetResult(true);
                    script.Error += (sender, e) => tcs.SetResult(true);

                    script.Execute();

                    tcs.Task.Wait();

                    conn.Close();

                }

            });

        }

        public static async Task<bool> TestConnection(this MySqlDatabase db)
        {

            return await Task.Run(() =>
            {
                using (var conn = new MySqlConnection(db.ConnectionString))
                {

                    try
                    {

                        conn.Open();

                        conn.Close();

                        return true;

                    }
                    catch (Exception)
                    {
                        return false;
                    }

                }
            });

        }

    }
}
