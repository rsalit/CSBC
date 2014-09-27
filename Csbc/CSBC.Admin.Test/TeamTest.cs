using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Linq.Expressions;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Core.Data;
using CSBC.Admin.Web.ViewModels;

namespace CSBC.Admin.Test
{
    [TestClass]
    public class TeamTest
    {


        public TeamTest()
        {

        }
        [TestInitialize]
        public void InitTest()
        {
            using (var context = new CSBCDbContext())
            {
                var tester = new CSBCDbInitializer();
                tester.InitHouseholds(context);
                tester.InitPersonTest(context);
                tester.InitColors(context);
                tester.InitDivision(context);
                tester.InitTeams(context);
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var context = new CSBC.Core.Data.CSBCDbContext();
            var tester = new TestUtils(context);
            tester.CleanupDb();
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("Team")]
        public void GetNumberOfDivisionTeamsTest()
        {
            var rep = new TeamRepository(new CSBC.Core.Data.CSBCDbContext());
            var team = rep.GetNumberofDivisionTeams(1492);
            Assert.IsTrue(team == 0);
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("Team")]
        public void GetDivisionTeamsTest()
        {
            var csbcDbContext = new CSBCDbContext();
            var rep = new TeamRepository(csbcDbContext);
            var divisions = csbcDbContext.Divisions.Select(s => s);
            var teams = rep.GetTeams(divisions.First<Division>().DivisionID);
            Assert.IsTrue(teams.Any<Team>());
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("Team"), TestCategory("Division")]
        public void GetDivisionTeamsTest2()
        {
            var divisions = new List<Division>();
            using (var context = new CSBCDbContext())
            {
                var repSeason = new SeasonRepository(context);
                var currentSeason = repSeason.GetCurrentSeason(1);
                var repDivision = new DivisionRepository(context);
                divisions = repDivision.GetDivisions(currentSeason.SeasonID).ToList<Division>();
            }
            var division = divisions.FirstOrDefault();
            var rep = new TeamVM();
            var teams = rep.GetDivisionTeams(division.DivisionID);

            Assert.IsTrue(teams.Any());

            var team = teams.FirstOrDefault();
            Assert.IsTrue(team.DivisionID > 0);
        }

        [TestMethod]
        [TestCategory("Model"), TestCategory("Team")]
        public void InsertTeamTest()
        {
            var rep = new TeamRepository(new CSBC.Core.Data.CSBCDbContext());
            Team team = new Team
            {
                DivisionID = 19,
                SeasonID = 2,
                TeamName = "Test",
                CompanyID = 1,
                TeamNumber = "1",
                TeamColorID = 1,
                TeamColor = "Ash",
                CreatedUser = "UnitTest"
            };
            var no = rep.Insert(team);
            Assert.IsTrue(no.TeamID > 0);

        }
    }
}
