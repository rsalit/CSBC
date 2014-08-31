using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lojack.Repositories;
using Lojack.Models;
using Lojack.Data;

namespace TestLojack
{
    [TestClass]
    public class LojackAuditProcessTest
    {
        [TestMethod]
        public void GetLojackAuditProcessTest()
        {
            var rep = new LojackAuditProcessRepository(new LojackContext());
            var audit = new LojackAuditProcess
            {
                FileName = "test.csv",
                ModificationDate = DateTime.Now,
                ProcessDateTime = DateTime.Now,
                RecordsProcessed = 10,
                TotalRecords = 100,
                Status = "Tested"
            };
            var record = rep.Insert(audit);
            var found = rep.FindByFileName(record.FileName);
            Assert.IsTrue(found);
            found = rep.FindByFileName("notfound.csv");
            Assert.IsFalse(found);
        }

        [TestMethod]
        public void InsertLojackAuditProcessTest()
        {
            var rep = new LojackAuditProcessRepository(new LojackContext());
            var audit = new LojackAuditProcess
            {
                FileName = "test.csv",
                ModificationDate = DateTime.Now,
                ProcessDateTime = DateTime.Now,
                RecordsProcessed = 10,
                Status = "Fine"
            };
            var record = rep.Insert(audit);
            Assert.IsTrue(record.LojackAuditProcessId > 0);
        }

        [TestMethod]
        public void GetByDateRangeTest()
        {
            var rep = new LojackAuditProcessRepository(new LojackContext());
            var data = rep.GetByDateRange(DateTime.Today.AddDays(-7), DateTime.Today).ToList();
            Assert.IsTrue(data.Any());
        }
    }
}
