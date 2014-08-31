using System;
using System.Linq;
using Lojack;
using Lojack.Data;
using Lojack.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using LojackImporter;
using Lojack.Repositories;
using RPDSS;
using Lojack.Models;
using RPDSS.Data;
using RPDSS.ServiceTransaction;


namespace TestLojack
{
    [TestClass]
    public class LojackTester
    {
        public static string Path = @"c:\bwi\rpdss\stable\lojackimporter\bin\debug\lojackimport";
        internal string[] Files;
        internal System.Data.SqlClient.SqlConnection Connection;

        [TestInitialize]
        public void Init()
        {

            //LojackImporter.Properties.Settings.Default.ImportDirectory;
            Files = Directory.GetFiles(Path);
            Connection = RPDSS.Data.SQLUtility.GetConnection();
        }

        [TestMethod]
        public void GetBlacklistedSerialsTest()
        {
            var importer = new LojackDataImporter();
            var blacklisted = importer.GetBlacklistedSerials(Connection);
            Assert.IsTrue(blacklisted.Count > 0);
        }

        [TestMethod]
        public void GetExistingComputraceIdsTest()
        {
            var importer = new LojackDataImporter();
            var traceIds = importer.GetExistingComputraceIds(Connection);
            Assert.IsTrue(traceIds.Any());
        }

        [TestMethod]
        public void ProcessLojackImportRecordTest()
        {
            var importer = new LojackDataImporter();
            var tracerIds = importer.GetExistingComputraceIds(Connection);
            var blacklisted = importer.GetBlacklistedSerials(Connection);
            var readerLine = "3,6640hur10799,,,1997-05-28 00:00:00.000,1997-05-28 00:00:00.000,97-99708,,Compaq,Armada";
            var processor = importer.ProcessLojackImportRecord(Connection, tracerIds, blacklisted, readerLine, 0);
            Assert.IsTrue(processor == 0);
            int secondsSinceMidnight = Convert.ToInt32(DateTime.Now.Subtract(DateTime.Today).TotalSeconds);
            var rand = new Random(secondsSinceMidnight);
            var newRandom = rand.Next(800000, 1200000);
            readerLine = newRandom.ToString() +
                         ",6640test0799,,,1997-05-28 00:00:00.000,1997-05-28 00:00:00.000,97-99708,,Compaq,Armada";
            processor = importer.ProcessLojackImportRecord(Connection, tracerIds, blacklisted, readerLine, 0);
            Assert.IsTrue(processor == 1);
        }

        [TestMethod]
        public void GetExistingComputraceIdsFromRepositoryTest()
        {
            var rep = new LojackEntryRepository(new LojackContext());
            var ids = rep.GetExistingComputraceIds();
            Assert.IsTrue(ids.Any());
        }

        [TestMethod]
        public void RunGetRetrievedHitsTest()
        {
            var rep = new MatchedLojackRepository(new LojackContext());
            var result = rep.SetLojackMatches();
            Assert.IsTrue(result == 0);
        }

        [TestMethod]
        public void GetActiveDataMartsTest()
        {
            var rep = new MatchedLojackRepository(new LojackContext());
            var result = rep.GetActiveDataMarts();
            Assert.IsTrue(result.Any());
        }

 

    

    }
}
