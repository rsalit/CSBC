using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Csbchoops.Web;
using Csbchoops.Web.ViewModels;


namespace CSBC.Web.Test
{
    [TestClass]
    public class GameScheduleTest
    {
        [TestMethod]
        [TestCategory("ViewModel"), TestCategory("ScheduleGames")]
        public void GetGameScheduleTest()
        {
            var vm = new Csbchoops.Web.ViewModels.GameSchedulesViewModel();

            var games = vm.GetGames(71);
            Assert.IsTrue(games.Any());
        }
        [TestMethod]
        [TestCategory("ViewModel"), TestCategory("ScheduleGames")]
        public void GetGameScheduleByDivisionTest()
        {
            var vm = new Csbchoops.Web.ViewModels.GameSchedulesViewModel();
            var games = vm.GetGames(71, 757);
            Assert.IsTrue(games.Any());
        }
        [TestMethod]
        [TestCategory("ViewModel"), TestCategory("ScheduleGames")]
        public void GetGameScheduleByDivisionandTeamTest()
        {
        var vm = new Csbchoops.Web.ViewModels.GameSchedulesViewModel();
            var games = vm.GetGames(71, 757, 4615);
            Assert.IsTrue(games.Any());
        }
        [TestMethod]
        [TestCategory("ViewModel"), TestCategory("ScheduleGames")]
        public void GetStandingsTest()
        {
            var vm = new ScheduleStandingsViewModel();
            var standings =  vm.GetStandings(757);
            Assert.IsTrue(standings.Any<Csbchoops.Web.ViewModels.ScheduleStandingsViewModel>());
        }
    }
}
