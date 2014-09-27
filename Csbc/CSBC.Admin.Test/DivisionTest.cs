using System;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Core.Data;
using System.Data;
using System.Data.SqlClient;
using CSBC.Components.Season;

namespace CSBC.Admin.Test
{
    [TestClass]
    public class DivisionTest
    {

        CSBC.Core.Data.CSBCDbContext context;

        [TestInitialize]
        public void InitTest()
        {
            var context = new CSBC.Core.Data.CSBCDbContext();
            var tester = new CSBCDbInitializer();
            tester.InitDivision(context);

        }
        [TestCleanup]
        public void CleanupTest()
        {
            var tester = new TestUtils(new CSBCDbContext());
            tester.CleanupDb();
        }
        [TestMethod]
        [TestCategory("Model")]
        public void GetDivisionRecordsTest()
        {
            context = new CSBCDbContext();
            var house = new Division();
            var rep = new DivisionRepository(context);
            var seasonRep = new SeasonRepository(context);
            var season = seasonRep.GetCurrentSeason(1);
            var divisions = rep.GetDivisions(season.SeasonID);
            Assert.IsTrue(divisions.Any<Division>());
        }
        [TestMethod]
        [TestCategory("SQL"), TestCategory("Division")]
        public void GetPlayerDivisionTest()
        {
            context = new CSBCDbContext();
            var repPeople = new PlayerRepository(context);
            var player  = context.Players.FirstOrDefault();
            //var person = repPeople.GetById(2);
            var repSeason = new SeasonRepository(context);
            var season = repSeason.GetCurrentSeason(1);
            var repDivision = new DivisionRepository(context);

            var dt = repDivision.GetPlayerDivision(1, season.SeasonID, player.PeopleID);
            Assert.IsTrue(dt != 0);
        }
        [TestMethod]
        [TestCategory("Model")]
        public void LoadDivisionTest()
        {
            context = new CSBCDbContext();
            var repDivision = new DivisionRepository(context);
            var divisions = repDivision.LoadDivisions(2);
            Assert.IsTrue(divisions.Count<vw_Divisions>() != 0);
        }

        [TestMethod]
        [TestCategory("Model"), TestCategory("Division")]
        public void InitDivisionTest()
        {
            context = new CSBCDbContext();
            var repDivision = new DivisionRepository(context);
            var division = new Division
            {
                Div_Desc = "Unit Test",
                CompanyID = 1,
                Gender = "M",
                MinDate = DateTime.Now,
                MaxDate = DateTime.Now.AddDays(60)
            };
            var divisionret = repDivision.Insert(division);
            Assert.IsTrue(divisionret != null);
        }
    }
}
