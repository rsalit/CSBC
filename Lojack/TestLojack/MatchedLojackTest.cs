using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lojack.Repositories;
using Lojack.Models;
using Lojack.Data;



namespace TestLojack
{
    [TestClass]
    public class MatchedLojackTest
    {
        [TestMethod]
        public void GetMatchedRecordsTest()
        {
            var matched = new List<MatchedLojack>();
            var rep = new MatchedLojackRepository(new LojackContext());
            var mart = rep.FindDataMartByDatabaseName("TestDM1");
            var propertyGuids = new List<Guid>();
            propertyGuids.Add(new Guid("83512420-D2E9-4990-83DA-88FECFDB98FF"));
            propertyGuids.Add(new Guid("DC77DCF4-7582-4649-AD96-2CB94837DFEB"));
            propertyGuids.Add(new Guid("B6ABB6B7-D2C8-4C32-B490-12DF06ECFEB0"));
            propertyGuids.Add(new Guid("7EF0A5A4-3820-4988-BB6D-BC154C1E6A7F"));
            if (mart != null)
                matched = rep.GetMatchedRecords(mart, propertyGuids);
            Assert.IsTrue(matched[0].PropertyGuid.ToString() != "");
        }
        [TestMethod]
        public void GetSqlTest()
        {
            var rep = new MatchedLojackRepository(new LojackContext());
            var sql = rep.GetSql("TestDM1");
            Assert.IsTrue(sql.Length > 0); //not a great test - just makes sure something is built - verify by putting in SQL database
        }
    }
}
