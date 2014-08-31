using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Lojack.Data;
using Lojack.Repositories;
using Lojack.Models;
using RPDSS;
using System.IO;

namespace Lojack
{
    public class LojackDataImporter
    {
        public LojackDataImporter()
        {

        }
        public int TotalRecords { get; set; }

        public List<string> GetBlacklistedSerials(SqlConnection conn)
        {
            var blacklistedSerials = new List<string>();
            ConsoleMessage(InsertStatus.Info, "Retrieving Blacklisted Serial numbers");
            var getBlacklists =
                new SqlCommand("SELECT BLACKLISTVALUE FROM " + RPDSS.Data.SQLUtility.ExpandName("BlackList") + " B, " +
                               RPDSS.Data.SQLUtility.ExpandName("BlackListTypeLU") +
                               " L where b.blacklisttypeid = l.blacklisttypeid and l.BLACKLISTTYPE = 'Serial Number'", conn);

            if (conn.State == ConnectionState.Closed) conn.Open();
            var blacklistReader = getBlacklists.ExecuteReader();
            while (blacklistReader.Read())
            {
                blacklistedSerials.Add(blacklistReader["BLACKLISTVALUE"].ToString().ToLower());
            }
            blacklistReader.Close();
            ConsoleMessage(InsertStatus.Info, "Done retrieving Blacklisted Serial numbers");
            return blacklistedSerials;
        }

        public List<string> GetExistingComputraceIds(SqlConnection conn)
        {
            ConsoleMessage(InsertStatus.Info, "Retrieving Existing Computrace IDs");
            var rep = new LojackEntryRepository(new LojackContext());
            var existingComputraceIDs = rep.GetExistingComputraceIds();
            ConsoleMessage(InsertStatus.Info, "Done retrieving Existing Computrace IDs");
            return existingComputraceIDs;
        }

        public void ProcessImportFiles(IEnumerable<string> files, List<string> existingComputraceIDs, SqlConnection conn,
            List<string> blacklistedSerials)
        {
            var rep = new LojackAuditProcessRepository(new LojackContext());
            foreach (string file in files)
            {
                if (rep.FindByFileName(file))
                {
                    var counter = ProcessImportFile(existingComputraceIDs, conn, blacklistedSerials, file);
                    InsertLojackAuditRecord(file, counter, TotalRecords);
                }
            }
        }

        private static void InsertLojackAuditRecord(string file, int counter, int totalRecords)
        {
            using (var db = new LojackContext())
            {
                var rep = new LojackAuditProcessRepository(db);
                var lojack = new LojackAuditProcess
                {
                    FileName = file,
                    ModificationDate = DateTime.Now,
                    ProcessDateTime = DateTime.Now,
                    RecordsProcessed = counter,
                    TotalRecords = totalRecords,
                    Status = "Processed"
                };
                rep.Insert(lojack);
            }
        }

        private int ProcessImportFile(List<string> existingComputraceIDs, SqlConnection conn, List<string> blacklistedSerials,
            string file)
        {
            int counter = 0;
            try
            {
                StreamReader rdr = File.OpenText(file);
                string verifyheaders = rdr.ReadLine(); //skip header row
                string[] headerscols = verifyheaders.Split(',');

                bool valid = false;

                if (headerscols.Length > 5)
                {
                    if (headerscols[0].ToLower().Trim().Equals("computracefile")) valid = true;
                }
                TotalRecords = 0;
                while (valid == true && !rdr.EndOfStream)
                {
                    TotalRecords ++;
                    counter = ProcessLojackImportRecord(conn, existingComputraceIDs, blacklistedSerials, rdr.ReadLine(), counter);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return counter;
        }

        public int ProcessLojackImportRecord(SqlConnection conn, List<string> existingComputraceIDs, List<string> blacklistedSerials,
            string readLine, int counter)
        {
            string[] line = readLine.Replace("\0", String.Empty).Split(',');

            if (line.Length > 9)
            {
                var importRecord = LojackImportFileRecord(line);

                if (!existingComputraceIDs.Contains(importRecord.CompuTraceFile))
                {
                    try
                    {
                        counter += InsertRecords(blacklistedSerials, importRecord, counter);

                        existingComputraceIDs.Add(importRecord.CompuTraceFile);
                    }
                    catch (Exception ex)
                    {
                        //need to modify this!
                        if (ex is IndexOutOfRangeException)
                        {
                        }
                        else
                        {
                        }
                    }
                }
                else
                {
                    ConsoleNotInsertedMessage(importRecord.CompuTraceFile);
                }
            }

            return counter;
        }

        private LojackImportFileRecord LojackImportFileRecord(string[] line)
        {
            var importRecord = new LojackImportFileRecord();
            importRecord.CompuTraceFile = line[0].Trim() ?? "";
            importRecord.SerialNumber1 = line[1].Trim() ?? "";
            importRecord.SerialNumber2 = line[2].Trim() ?? "";
            importRecord.SerialNumber3 = line[3].Trim() ?? "";
            importRecord.StolenDate = line[4].Trim() ?? "";
            importRecord.DateReportedToAbt = line[5].Trim() ?? "";
            importRecord.TheFtIncidentNumber = line[6].Trim() ?? "";
            importRecord.AgencyName = line[7].Trim() ?? "";
            importRecord.Make = line[8].Trim() ?? "";
            importRecord.Model = line[9].Trim() ?? "";
            return importRecord;
        }

        public int InsertRecords(List<string> blacklistedSerials, LojackImportFileRecord importRecord, int counter)
        {

            using (var db = new LojackContext())
            {
                DateTime date;
                var rep = new LojackEntryRepository(db);
                var lojack = new LojackEntry
                {
                    computracefile = importRecord.CompuTraceFile,
                    AgencyName = importRecord.AgencyName,
                    DateReportedToABT =
                        DateTime.TryParse(importRecord.DateReportedToAbt, out date) ? date : (DateTime?)null,
                    Make = importRecord.Make.Length > 30 ? importRecord.Make.Remove(30) : importRecord.Make,
                    Model = importRecord.Model.Length > 30 ? importRecord.Model.Remove(30) : importRecord.Model,
                    StolenDate = DateTime.TryParse(importRecord.StolenDate, out date) ? date : (DateTime?)null
                };

                if (importRecord.SerialNumber2 == importRecord.SerialNumber1 ||
                    importRecord.SerialNumber2 == importRecord.SerialNumber3) importRecord.SerialNumber2 = String.Empty;
                if (importRecord.SerialNumber3 == importRecord.SerialNumber1 ||
                    importRecord.SerialNumber3 == importRecord.SerialNumber2) importRecord.SerialNumber3 = String.Empty;
                int success = 0;
                for (int i = 1; i < 4; i++)
                {
                    string serialNo = String.Empty;
                    if (i == 1)
                        serialNo = importRecord.SerialNumber1;
                    if (i == 2)
                        serialNo = importRecord.SerialNumber2;
                    if (i == 3)
                        serialNo = importRecord.SerialNumber3;

                    if (!String.IsNullOrEmpty(serialNo) && !blacklistedSerials.Contains(serialNo))
                    {
                        lojack.ReportedSerialNumber = serialNo;
                        try
                        {
                            rep.Insert(lojack);
                            ConsoleInsertSuccessMessage(counter + 1, lojack.computracefile, lojack.ReportedSerialNumber);
                            success++;
                        }
                        catch (Exception)
                        {
                            ConsoleInsertFailureMessage(counter, lojack.computracefile, lojack.ReportedSerialNumber);
                        }
                    }

 
                }

                return success;
            }
        }

        public void FindMatchedRecordsandAddToDatabase()
        {
            using (var db = new LojackContext())
            {
                var rep = new MatchedLojackRepository(db);
                rep.GetNewMatches(rep.GetActiveDataMarts());
            }
        }

        private static void ConsoleInsertFailureMessage(int counter, string computracefile, string reportedserialnumber1)
        {
            var message = counter.ToString() + " - ERROR INSERTING computrace#:" + computracefile + " serial: " +
                              reportedserialnumber1 + "- counter = " + counter.ToString();
            ConsoleMessage(InsertStatus.Failure, message);
        }

        private static void ConsoleInsertSuccessMessage(int counter, string computracefile, string reportedserialnumber1)
        {

            var message = counter.ToString() + " - Inserted computrace#:" + computracefile + ": serial: " +
                              reportedserialnumber1 + "- counter = " + counter.ToString(); 
            ConsoleMessage(InsertStatus.Success, message);
        }

        private static void ConsoleNotInsertedMessage(string computracefile)
        {

            var message = "Record found - Computrace#:" + computracefile + "                                   ";
            ConsoleMessage(InsertStatus.Found, message);
        }

        private enum InsertStatus
        {
            Failure,
            Success,
            Found, 
            Info
        };

        private static void ConsoleMessage(InsertStatus status, string message)
        {
            Console.ForegroundColor = GetForegroundColor(status);
            Console.CursorLeft = 5;
            Console.CursorTop = 1;
            //if (counter % 50 == 0)
            Console.WriteLine(message);
        }

        private static ConsoleColor GetForegroundColor(InsertStatus status)
        {
            var color = Console.ForegroundColor;
            switch (status)
            {
                case InsertStatus.Success :
                    color = ConsoleColor.Green;
                    break;
                case InsertStatus.Failure:
                    color = ConsoleColor.Red;
                    break;
                case InsertStatus.Found:
                    color = ConsoleColor.Yellow;
                    break;
                case InsertStatus.Info:
                    color = ConsoleColor.White;
                    break;
            }
            return color;
        }
    }

    public class LojackImportFileRecord
    {
        public string CompuTraceFile { get; set; }
        public string StolenDate { get; set; }
        public string DateReportedToAbt { get; set; }
        public string TheFtIncidentNumber { get; set; }
        public string AgencyName { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string SerialNumber1 { get; set; }
        public string SerialNumber2 { get; set; }
        public string SerialNumber3 { get; set; }
    }
}


