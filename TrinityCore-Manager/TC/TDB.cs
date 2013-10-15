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
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SevenZip;
using TrinityCore_Manager.Database;
using TrinityCore_Manager.TCM;

namespace TrinityCore_Manager.TC
{
    public class TDB
    {

        public const string UrlBase = "http://mitch528.com/api/";

        public const string TDBLatestDownloadUrl = UrlBase + "DownloadLatestTDB";
        public const string TDBPreviousDownloadUrl = UrlBase + "DownloadTDB/";

        public static Task DownloadTDBAsync(IProgress<int> progress, string downloadTo, string version = "")
        {

            return Task.Run(() =>
            {

                Uri uri;

                if (string.IsNullOrEmpty(version))
                    uri = new Uri(TDBLatestDownloadUrl);
                else
                    uri = new Uri(TDBPreviousDownloadUrl + version);

                ManualResetEvent mre = new ManualResetEvent(false);

                using (WebClient client = new WebClient())
                {
                    client.DownloadFileCompleted += (sender, e) => mre.Set();
                    client.DownloadProgressChanged += (sender, e) => progress.Report(e.ProgressPercentage);
                    client.DownloadFileAsync(uri, downloadTo);
                }

                mre.WaitOne();

            });

        }

        public static Task Extract7zAsync(string file, string outDir, IProgress<int> progress)
        {

            return Task.Run(() =>
            {

                var libPath = Path.Combine("7zip", "7z.dll");

                SevenZipExtractor.SetLibraryPath(libPath);
                SevenZipExtractor extractor = new SevenZipExtractor(file);

                ManualResetEvent mre = new ManualResetEvent(false);

                extractor.Extracting += (sender, e) => progress.Report(e.PercentDone);
                extractor.ExtractionFinished += (sender, e) => mre.Set();

                extractor.BeginExtractArchive(outDir);

                mre.WaitOne();

            });


        }

        public static Task ApplyAsync(string sqlFile, MySqlDatabase db, IProgress<int> progress, CancellationTokenSource cts)
        {
            return db.Restore(sqlFile, progress, cts.Token);
        }

    }

}
