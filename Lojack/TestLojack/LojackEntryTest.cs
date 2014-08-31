using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Lojack.Models;
using Lojack.Repositories;
using Lojack.Data;

namespace TestLojack
{
    [TestClass]
    public class LojackEntryTest
    {
        [TestMethod]
        public void InsertLojackEntryTest()
        {
            var rep = new LojackEntryRepository(new LojackContext());
            var entry = new LojackEntry
            {
                computracefile = "78439",
                AgencyName = "",
                DateReportedToABT = DateTime.Now,
                Make = "Compaq Impresario",
                Model = "Dell",
                StolenDate = DateTime.Now,
                ReportedSerialNumber = "73849875"
            };
            var record = rep.Insert(entry);
            Assert.IsTrue(record.lojackentryid > 0);
        }
    }
}
