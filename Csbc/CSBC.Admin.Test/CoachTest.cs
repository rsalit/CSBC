using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Xml;
using System.Linq;
using System.Linq.Expressions;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Core.Data;

namespace CSBC.Admin.Test
{
    [TestClass]
    public class PersonTest
    {
        private const string shirtSize = "Z-Large";
        private const string coachPhone = "999-999-9990";

        [TestInitialize]
        public void InitTest()
        {
            var context = new Core.Data.CSBCDbContext();
            var init = new CSBCDbInitializer();
            //init.InitCoaches(context);
            //init.InitPersonTest(context);
        }
        [TestCleanup]
        public void CleanupTest()
        {
            //cleanup???
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("Coach")]
        public void GetCoachByIdTest()
        {
            var context = new Core.Data.CSBCDbContext();
            var testinit = new CSBCDbInitializer();
            var rep = new CoachRepository(context);
            var dt = rep.GetCoaches((testinit.CurrentSeason).SeasonID);
            Assert.IsTrue(dt.Rows.Count > 0);
        }


        [TestMethod]
        [TestCategory("Model"), TestCategory("Coach")]
        public void GetCoachVolunteerTest()
        {
            var context = new Core.Data.CSBCDbContext();
            var testinit = new CSBCDbInitializer();
            var rep = new CoachRepository(context);
            var coaches = rep.GetCoachVolunteers(1, testinit.CurrentSeason.SeasonID);

            Assert.IsTrue(coaches.Count<vw_Coaches>() > 0);
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("Coach")]
        public void GetSeasonCoachesTest()
        {
            var context = new Core.Data.CSBCDbContext();
            var testinit = new CSBCDbInitializer();
            var rep = new CoachRepository(context);
            var coaches = rep.GetSeasonCoaches(testinit.CurrentSeason.SeasonID);

            Assert.IsTrue(coaches.Any<vw_Coaches>() );
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("Coach")]
        public void UpdateCoachesTest()
        {
            var context = new Core.Data.CSBCDbContext();
            var testinit = new CSBCDbInitializer();
            var rep = new CoachRepository(context);
            var coach = context.Coaches.FirstOrDefault();
            coach.ShirtSize = shirtSize;
            coach.CoachPhone = coachPhone;
            rep.Update(coach);
            var newcoach = rep.GetById(coach.CoachID);
            Assert.IsTrue(newcoach.ShirtSize == shirtSize);
            Assert.IsTrue(newcoach.CoachPhone == coachPhone);

        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("Coach")]
        public void CreateCoachTest()
        {
            var context = new Core.Data.CSBCDbContext();

            var rep = new CoachRepository(context);
            Debug.Assert(context.People != null, "context.People != null");
            var coach = new Coach
            {
                CompanyID = 1,
                PeopleID = context.People.FirstOrDefault().PeopleID,
                SeasonID = context.Seasons.First(s => s.CurrentSeason == true).SeasonID,
                CoachPhone = "999-000-9090"
            };
            var i = rep.Insert(coach);


            Assert.IsTrue(coach.CoachID > 0);
        }
    }
}
