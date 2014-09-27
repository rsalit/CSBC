using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Xml;
using System.Linq;
using System.Linq.Expressions;
using CSBC.Core.Models;
using CSBC.Core.Data;
using CSBC.Core.Repositories;

namespace CSBC.Admin.Test
{
    [TestClass]
    public class CoachTest
    {
        [TestInitialize]
        public void InitTest()
        {
            using (var context = new CSBCDbContext())
            {
                var init = new CSBCDbInitializer();
                init.InitHouseholds(context);
                init.InitPersonTest(context);
            }
        }
        [TestCleanup]
        public void CleanupTest()
        {
            using (var context = new CSBCDbContext())
            {
                var init = new CSBCDbInitializer();
                init.DeleteTestPeople(context);
                init.DeleteTestHouseholds(context);
            }

        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("Person")]
        public void InsertPersonTest()
        {
            var context = new Core.Data.CSBCDbContext();
            var repHouse = new HouseholdRepository(context);
            var houses = repHouse.GetByName(TestUtils.Household1);
            var house  = houses.FirstOrDefault();
            var rep = new PersonRepository(context);
            int no= rep.Insert(new Person { 
                CompanyID = TestUtils.CompanyId, FirstName = "Sam", LastName = "Fred", HouseID = house.HouseID })
                .PeopleID;
   
            Assert.IsTrue(no > 0);
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("Person")]
        public void GetPersonByFirstAndLastNameTest()
        {
            var context = new Core.Data.CSBCDbContext();
            var testinit = new TestUtils(context);
            var rep = new PersonRepository(context);
            var person = rep.FindPersonByLastAndFirstName(testinit.HouseholdLastNames[0], testinit.FirstNames[0]);
            Assert.IsTrue(person.PeopleID != 0);
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("Person")]
        public void GetPersonByIdTest()
        {
            var context = new Core.Data.CSBCDbContext();
            var testinit = new TestUtils(context);
            var rep = new PersonRepository(context);
            var people = rep.GetAll(1);
            var person5 = people.ElementAt<Person>(5); //assume at least 5 people in table
            var id = person5.PeopleID;
            var person = rep.GetById(id);
            Assert.IsTrue(person.PeopleID != 0);
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("Person")]
        public void GetParentsTest()
        {
            using (var context = new Core.Data.CSBCDbContext())
            {
                var testinit = new TestUtils(context);
                var rep = new PersonRepository(context);
                
                var person = context.People.FirstOrDefault();
                if (person != null)
                {
                    var parents = rep.GetParents(person.PeopleID);
                    Assert.IsTrue(person.PeopleID != 0);
                    Assert.IsTrue(parents.Any());
                }
            }
        }
        [TestMethod]
        [TestCategory("WebUI"), TestCategory("Person")]
        public void GetEmailGroupTest()
        {
            using (var context = new CSBCDbContext())
            {
                var testinit = new TestUtils(context);
                var rep = new PersonRepository(context);
                //var people = rep.GetByGroup(1, testinit);
            }
        }
    }
}
