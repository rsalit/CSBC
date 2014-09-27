using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Core.Data;
using System.Data;
using System.Xml;
using Moq;

namespace CSBC.Admin.Test
{
    [TestClass]
    public class HousholdTest
    {
        private const string HouseholdName1 = "Smith";

        [TestInitialize]
        public void InitTest()
        {
            using (var context = new Core.Data.CSBCDbContext())
            {
                var init = new CSBCDbInitializer();
                init.InitHouseholds(context);
            }
        }

        [TestCleanup]
        public void CleanupTest()
        {
            using (var context = new Core.Data.CSBCDbContext())
            {
                var init = new CSBCDbInitializer();
                init.DeleteTestHouseholds(context);
            }
        }

        [TestMethod]
        [TestCategory("Model"), TestCategory("Household")]
        public void InsertHouseholdRecord()
        {
            var house = new Household { Name = HouseholdName1, Address1 = "10 Minute Lane", City = "Plainview", State = "NY", Email = "joe@aol.com", Phone = "516-222-2222" };
            var rep = new HouseholdRepository(new CSBCDbContext());
            int no = 0;
            no = rep.Insert(house).HouseID;
            Assert.IsTrue(no > 0);
        }

        [TestMethod]
        [TestCategory("Model"), TestCategory("Household")]
        public void DeleteHouseholdRecord()
        {
            var house = new Household { Name = HouseholdName1, CompanyID = 1, Address1 = "10 Minute Lane", 
                City = "Plainview", 
                State = "NY", 
                Email = "joe@aol.com", 
                Phone = "516-222-2222" };
            using (var db = new CSBCDbContext())
            {
                var rep = new HouseholdRepository(db);
                
                var no = rep.Insert(house);
                Assert.IsTrue(no != null);
                var household = rep.GetById(no.HouseID);
                Assert.IsTrue(household != null );
                rep.Delete(household);
                household = rep.GetById(no.HouseID);
                Assert.IsTrue(household == null);
            }
        }


        [TestMethod]
        [TestCategory("Model"), TestCategory("Household")]
        public void FindHouseholdById()
        {
            var house = new Household();
            var rep = new HouseholdRepository(new CSBCDbContext());
            var houses = rep.GetByName(HouseholdName1);
            Assert.IsTrue(houses.Count<Household>() > 0);
            house = rep.GetById(houses.FirstOrDefault().HouseID);
            Assert.IsTrue(house != null);
            Assert.IsTrue(house.Name == HouseholdName1);

        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("Household")]
        public void FindHouseholdByName()
        {

            var rep = new HouseholdRepository(new CSBCDbContext());
            var houses = rep.GetByName(HouseholdName1);
            Assert.IsTrue(houses.Count<Household>() > 0);
            var house = houses.First<Household>();
            Assert.IsTrue(house.Name == HouseholdName1);
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("Household")]
        public void GetHouseholdSearchRecordsTest()
        {
            var house = new Household();
            using (var db = new CSBCDbContext())
            {
                var rep = new HouseholdRepository(db);
                var homes = rep.GetRecords(1, name: "Fa").ToList();
                Assert.IsTrue(homes.Any());
                homes = rep.GetRecords(1, address: "Main").ToList();
                Assert.IsTrue(homes.Any());
                homes = rep.GetRecords(1, name: "Fa", phone: "954").ToList();
                Assert.IsTrue(homes.Any());
                homes = rep.GetRecords(1, name: "Fa", address: "123", email: "yahoo.com").ToList();
                Assert.IsTrue(homes.Any());
            }
        }
    }
}
