using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Core.Data;

namespace CSBC.Admin.Test
{
    [TestClass]
    public class Summary
    {
        [TestMethod]
        [TestCategory("Model")]
        public void GetSummaryTest()
        {
            var context = new CSBCDbContext();
            var rep = new SummaryRepository(context);
            var repSeason = new SeasonRepository(context);
            var current = repSeason.GetCurrentSeason(1);
            
            rep.GetSummary(1, current.SeasonID);
            Assert.IsTrue(false);
        }
    }
}
