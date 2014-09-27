using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Linq;
using CSBC.Core.Interfaces;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Core.Data;
using CSBC.Core.Models.Validation;
using Moq;
using System.Web.Mvc;
  
namespace CSBC.Admin.Test
{
    [TestClass]
    public class ScheduleGames
    {
        private Mock<IScheduleGameRepository> _mockRepository;
        private ModelStateDictionary _modelState;
        private IScheduleGameService _service;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IScheduleGameRepository>();
            _modelState = new ModelStateDictionary();
            _service = new ScheduleGameService(new ModelStateWrapper(_modelState), _mockRepository.Object);
            
            using (var db = new CSBCDbContext())
            {
                var rep = new ScheduleGameRepository(db);
                db.ScheduleGames.Add(
                    new ScheduleGame {
                        ScheduleNumber=1001,
                        GameNumber = 1,
                        HomeTeamNumber = 10 });
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            /*var context = new CSBC.Core.Data.CSBCDbContext();
            var tester = new TestUtils(context);
            tester.CleanupDb();          */
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("ScheduleGames")]
        public void GameSchedulesGetAllTest()
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new ScheduleGameRepository(db);
                var games = rep.GetAll();
                Assert.IsTrue(games.Any());
            }
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("ScheduleGames")]
        public void CreateScheduleGame()
        {
            var game = new ScheduleGame {GameNumber = 10, GameTime = "4:50 PM", GameDate = DateTime.Today};
            var result = _service.CreateScheduleGame(game);
            Assert.IsFalse(result);
            var error = _modelState["ScheduleNumber"].Errors[0];
            Assert.AreEqual("Schedule Number must be greater than 0.", error.ErrorMessage);
        }
        
        [TestMethod]
        [TestCategory("Model"), TestCategory("ScheduleGames")]
        public void CreateScheduleGameREquiredGameNumber()
        {
            var game = new ScheduleGame {ScheduleNumber = 10, GameTime = "4:50 PM", GameDate = DateTime.Today};
            var result = _service.CreateScheduleGame(game);
            Assert.IsFalse(result);
            var error = _modelState["GameNumberNumber"].Errors[0];
            Assert.AreEqual("Game Number must be greater than 0.", error.ErrorMessage);
        }

        [TestMethod]
        [TestCategory("Model"), TestCategory("ScheduleLocation")]
        public void TestGetVenues()
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new ScheduleLocationRepository(db);
                var venues = rep.GetAll();
                Assert.IsTrue(venues.Any());
            }
        }

        [TestMethod]
        [TestCategory("Model"), TestCategory("ScheduleGames")]
        public void TestGetGamesByDate()
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new ScheduleGameRepository(db);
                var venues = rep.GetAll();
                Assert.IsTrue(venues.Any());
            }
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("ScheduleGames")]
        public void GetGamesAllTeamTest()
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new ScheduleGameRepository(db);
                var table = rep.GetGames(1, 7, 0, "MEN", "");
                Assert.IsTrue(table.Tables[0].Rows.Count > 0);
            }
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("ScheduleGames")]
        public void GetSeasonGamesTest()
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new ScheduleGameRepository(db);
                var games = rep.GetSeasonGames(757);
                Assert.IsTrue(games.Any());
            }
        }

    }
}
