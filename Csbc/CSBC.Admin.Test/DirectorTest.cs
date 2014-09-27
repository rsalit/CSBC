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
using CSBC.Admin.Web;
using CSBC.Admin.Web.ViewModels;


namespace CSBC.Admin.Test
{
    [TestClass]
    public class DirectorTest
    {
        [TestInitialize]
        public void InitTest()
        {
            using (var context = new CSBC.Core.Data.CSBCDbContext())
            {
                var tester = new CSBCDbInitializer();
                tester.InitDirectorTest();
            }
        }
        [TestMethod]
        [TestCategory("Model")]
        [TestCategory("Directors")]
        public void GetDirectorsTest()
        {
            var rep = new DirectorRepository(new CSBCDbContext());
            var directors = rep.GetAll(1);
            Assert.IsTrue(directors.Count<vw_Directors>() > 1);
        }
        [TestMethod]
        [TestCategory("Model")]
        [TestCategory("Directors")]
        public void GetDirectorVolunteersTest()
        {
            using (var db = new CSBCDbContext())
            {
            var rep = new DirectorRepository(db);
            var records = rep.GetDirectorVolunteers(1);
            Assert.IsTrue(records.Any());
            foreach(vw_Directors record in records)
            {
                Assert.IsTrue(record.PeopleID != 0);
                Assert.IsTrue(record.Name != String.Empty);
            }
            }
        }
        [TestMethod]
        [TestCategory("Model")]
        [TestCategory("Directors")]
        public void InsertDirectorTest()
        {
            using (var db = new CSBCDbContext())
            {
                var repHouse = new HouseholdRepository(db);
                var house = repHouse.Insert(new Household {CompanyID=2, Name="Frost"});
                var repPeople = new PersonRepository(db);
                var person = repPeople.Insert(new Person { FirstName = "Jack", LastName = "Frost", HouseID = house.HouseID });
                var rep = new DirectorRepository(db);
                var director = new Director{ PeopleID = person.PeopleID, CompanyID = 2, Title = "President" };
                var records = rep.Insert(director);

                    Assert.IsTrue(records.PeopleID != 0);
                    Assert.IsTrue(records.Title != String.Empty);
                //rep.Delete
            }
        }

    }
}
