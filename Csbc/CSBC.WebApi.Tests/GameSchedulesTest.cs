using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSBC.WebApi;
using CSBC.Core.Data;
using CSBC.Core.Repositories;

namespace CSBC.WebApi.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [TestCategory("Controller"), TestCategory("ScheduleGames")]
        public void GameSchedulesGetTest()
        {
            var controller = new Controllers.GameSchedulesController(new ScheduleGameRepository(new CSBCDbContext()));
            var games = controller.Get();
            Assert.IsTrue(games != null);
            Assert.IsTrue(games.Count > 0);
        }
    }
}
