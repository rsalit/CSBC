using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Data;
using System.Runtime.InteropServices;
using Lojack;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using Lojack.Data;
using Lojack.Repositories;
using Lojack.Models;
using RPDSS;

namespace Lojack
{
    public class Program
    {
        private static void Main(string[] args)
        {
            string path = Lojack.Properties.Settings.Default.ImportDirectory;

            string[] files = Directory.GetFiles(path);

            using (var conn = RPDSS.Data.SQLUtility.GetConnection())
            {
                var lojackImporter = new LojackDataImporter();
                var blacklistedSerials = lojackImporter.GetBlacklistedSerials(conn);
                var existingComputraceIDs = lojackImporter.GetExistingComputraceIds(conn);
                lojackImporter.ProcessImportFiles(files, existingComputraceIDs, conn, blacklistedSerials);
                lojackImporter.FindMatchedRecordsandAddToDatabase();

            }

        }
    }
}
