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
