using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Core.Data;
using CSBC.Admin.Web.ViewModels;
using System.Linq;

namespace CSBC.Admin.Test
{
    [TestClass]
    public class SeasonTests
    {
        CSBC.Core.Data.CSBCDbContext context;
        [TestInitialize]
        public void InitTest()
        {
            context = new CSBC.Core.Data.CSBCDbContext();
            //context.CSDBConnectionString = ConfigurationManager.ConnectionStrings["CSBCContext"];
            var tester = new CSBCDbInitializer();
            tester.InitSeasons(context);
        }

        [TestMethod]
        [TestCategory("Model")]
        public void GetCurrentSeasonTest()
        {
            var rep = new SeasonRepository(context);
            var season = new Season();
            season = rep.GetCurrentSeason(1);
            Assert.IsTrue(season != null);
            Assert.IsTrue(season.SeasonID != 0);
        }

        [TestMethod]
        [TestCategory("ViewModel")]
        public void GetSeasonsTest()
        {
            var vm = new SelectSeasonVM();
            var seasons = vm.GetRecords(1);
            Assert.IsTrue(seasons.Any());
        }

        [TestMethod]
        [TestCategory("Model")]
        public void GetSeasonsFromRepository()
        {
           var rep = new SeasonRepository(context);
           var seasons = rep.GetSeasons(1);
            Assert.IsTrue(seasons.Count<Season>() > 0);
        }
    }
}
