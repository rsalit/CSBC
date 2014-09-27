using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSBC.WebApi;

namespace CSBC.WebApi.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [TestCategory("Controller"), TestCategory("ScheduleGames")]
        public void GameScheduluesGetTest()
        {
            var controller = new Controllers.GameSchedulesController();
            var games = controller.Get();
            Assert.IsTrue(games != null);
            Assert.IsTrue(games.Count > 0);
        }
    }
}
