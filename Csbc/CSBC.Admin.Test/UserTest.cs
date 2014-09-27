using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Data.Entity;
using System.Linq;
using CSBC.Core.Models;
using CSBC.Core.Data;
using CSBC.Core.Repositories;

namespace CSBC.Admin.Test
{
    [TestClass]
    public class UserTest
    {
        CSBC.Core.Data.CSBCDbContext context;
        [TestInitialize]
        public void InitTest()
        {
            context = new CSBC.Core.Data.CSBCDbContext();
            var tester = new CSBCDbInitializer();
            tester.InitUser(context);
        }

        [TestMethod]
        [TestCategory("Model"), TestCategory("User")]
        public void GetUserAccessTest()
        {
            var dataContext = new CSBCDbContext();
            var tester = new TestUtils(dataContext);
            var rep = new UserRepository(new CSBCDbContext());
            var user = dataContext.Users.FirstOrDefault(n => (n.UserName == (tester.HouseholdLastNames[0] + "1")) && (n.PWord == tester.HouseholdLastNames[0]));
            Assert.IsTrue(user.HouseID != 0);
            //return user.SeasonID
        }

        [TestMethod]
        [TestCategory("Model"), TestCategory("User")]
        public void GetUserByHouseholdIDTest()
        {
            using (var db = new CSBCDbContext())
            {
                var tester = new TestUtils(db);
                var house = new HouseholdRepository(db).GetByName(tester.HouseholdLastNames[1]).First<Household>();
                var rep = new UserRepository(db);
                var user = rep.GetUserByHouseId(house.HouseID);
                Assert.IsTrue(user != null);
            }
        }
    }
}
