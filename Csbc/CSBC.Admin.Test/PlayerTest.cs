using System;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Core.Data;
using CSBC.Admin.Web;


namespace CSBC.Admin.Test
{
    [TestClass]
    public class PlayerTest
    {
        [TestInitialize]
        public void InitTest()
        {
            /*var context = new CSBC.Core.Data.CSBCDbContext();
            var tester = new CSBCDbInitializer();
            tester.InitDivision(context);
            tester.InitPlayers(context);    */
        }

        [TestCleanup]
        public void TestCleanup()
        {
            /*var context = new CSBC.Core.Data.CSBCDbContext();
            var tester = new TestUtils(context);
            tester.CleanupDb();          */
        }

        [TestMethod]
        [TestCategory("Model"), TestCategory("Players")]
        public void GetPlayersTest()
        {
            var testinit = new CSBCDbInitializer();
            var context = new CSBC.Core.Data.CSBCDbContext();
            var house = new Player();
            var rep = new PlayerRepository(context);
            var players = rep.GetSeasonPlayers(testinit.CurrentSeason.SeasonID);
            int no = players.Count<SeasonPlayer>();
            
            Assert.IsTrue(players.Count<SeasonPlayer>() > 1);
            var player = players.First();
            Assert.IsTrue(player.PeopleID > 0);
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("Players")]
        public void InsertPlayerTest()
        {
            var context = new CSBC.Core.Data.CSBCDbContext();
            var player = new Player { DivisionID = 25, PeopleID = 2, CompanyID = 1, CoachID = 2, SeasonID = 2 };
            var rep = new PlayerRepository(context);
            var id = rep.Insert(player);
            Assert.IsTrue(context.Players.Count<Player>(p=>p.CompanyID==1 && p.SeasonID ==2 && p.DivisionID == 25) > 0);
            Assert.IsTrue(context.Players.Find( id.PlayerID ) != null);
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("Players")]
        public void GetNextDraftIdTest()
        {
            var context = new CSBC.Core.Data.CSBCDbContext();
            var player = new Player { DivisionID = 25, PeopleID = 2, CompanyID = 1, CoachID = 2, SeasonID = 2 };
            var rep = new PlayerRepository(context);
            var count = rep.GetNextDraftId(1, 2, 25);
            Assert.IsTrue(!String.IsNullOrEmpty(count));
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("Players")]
        public void DeletePlayerTest()
        {
            var context = new CSBCDbContext();
            var player = new Player { DivisionID = 25, PeopleID = 2, CompanyID = 1, CoachID = 2, SeasonID = 2 };
            var rep = new PlayerRepository(context);
            var id = rep.Insert(player);

            Assert.IsTrue(context.Players.Any<Player>(p => p.CompanyID == 1 && p.SeasonID == 2 && p.DivisionID == 25));
            rep.Delete(player);
            Assert.IsTrue(context.Players.Find(id) == null);
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("Players")]
        public void GetSeasonPlayersTest()
        {
            var context = new CSBC.Core.Data.CSBCDbContext();
            var rep = new PlayerRepository(context);
            var repSeason = new SeasonRepository(context);
            var seasonId = repSeason.GetCurrentSeason(1).SeasonID;
            var players = rep.GetPlayers(seasonId);

            Assert.IsTrue(players.Count<SeasonPlayer>() > 0);  
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("Players")]
        public void GetTeamPlayersTest()
        {
            var context = new CSBC.Core.Data.CSBCDbContext();
            var rep = new PlayerRepository(context);
            var repTeam = new TeamRepository(context);
            //var seasonId = repSeason.GetCurrentSeason(1).SeasonID;
            var players = rep.GetTeamPlayers(5954);       //test value

            Assert.IsTrue(players.Count<SeasonPlayer>() > 0);
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("Players")]
        public void GetSponsorPlayersTest()
        {
            var context = new CSBC.Core.Data.CSBCDbContext();
            var rep = new PlayerRepository(context);
            var players = rep.GetSponsorPlayers(7478, 23893);       //test value

            Assert.IsTrue(players.Any<SeasonPlayer>() );


        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("Players")]
        public void GetDraftPlayersTest()
        {
            var players = CSBC.Admin.Web.ViewModels.PlayerVM.GetSeasonPlayers(0);
            Assert.IsTrue(false);
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("Players")]
        public void GetByPeopleIDTest()
        {
            var rep = new PlayerRepository(new CSBCDbContext());
            var player = rep.GetByPeopleId(2);
            Assert.IsTrue(player != null);
            Assert.IsTrue(player.PlayerID > 0);
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("Players")]
        public void GetLastSeasonPlayedTest()
        {
            var rep = new PlayerRepository(new CSBCDbContext());
            var season = rep.GetLastSeasonPlayed(2);
            Assert.IsTrue(season.SeasonID > 0);
        }

        [TestMethod]
        [TestCategory("Model"), TestCategory("Players")]
        public void GetPlayerByPersonAndSeasonIdTest()
        {
            var rep = new PlayerRepository(new CSBCDbContext());
            var season = rep.GetPlayerByPersonAndSeasonId(2, 2);
            Assert.IsTrue(season != null);
        }

    }
}
