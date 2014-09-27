using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Linq.Expressions;
using System.Diagnostics.Contracts;
using CSBC.Core.Models;
using CSBC.Core.Data;
using CSBC.Core.Repositories;

namespace CSBC.Admin.Test
{
    [TestClass]
    public class SponsorProfileTest
    {
        [TestInitialize]
        public void InitTest()
        {
            //var context = new Core.Data.CSBCDbContext();
            //var init = new CSBCDbInitializer();
        }
        [TestCleanup]
        public void CleanupTest()
        {
            //cleanup???
        }

        [TestMethod]
        [TestCategory("Model"), TestCategory("SponsorProfile")]
        public void CreateSponsorProfileTest()
        {
            using (var db = new CSBCDbContext())
            {
                var repPeople = new PersonRepository(db);
                var people = db.People.Where(p => p.Sponsor == true).ToList();
                var repColor = new ColorRepository(db);
                var color = repColor.GetByName(1, "Red").ID;

                Contract.Assert(color != 0);
                var repProfile = new SponsorProfileRepository(db);
                var person = people.FirstOrDefault();
                var sponsorProfile = repProfile.Insert(new SponsorProfile
                    {
                        SpoName = person.LastName + "Company",
                        SponsorProfileID = 0,
                        CompanyID = 1,
                        HouseID = person.HouseID,
                        State = "FL",
                        City = "Coral Springs",
                        Address = "10 Main Street",
                        ContactName = person.FirstName + " " + person.LastName

                    });

                var profile = repProfile.GetById(sponsorProfile.SponsorProfileID);
                Assert.IsTrue(profile != null);
                repProfile.Delete(profile);
                profile = repProfile.GetById(sponsorProfile.SponsorProfileID);
                Assert.IsTrue(profile == null);

            }
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("SponsorProfile")]
        public void UpdateSponsorProfileTest()
        {
            using (var db = new CSBCDbContext())
            {
                var repPeople = new PersonRepository(db);
                var people = db.People.Where(p => p.Sponsor == true).ToList();
                var repColor = new ColorRepository(db);
                var color = repColor.GetByName(1, "Red").ID;

                Contract.Assert(color != 0);
                var repProfile = new SponsorProfileRepository(db);
                var id = new Random();
                var person = people.FirstOrDefault();
                var newSponsorProfileId = id.Next(1, 32000);
                var sponsorProfile = repProfile.Insert(new SponsorProfile
                {
                    SpoName = person.LastName + "Company",
                    SponsorProfileID = 0,
                    CompanyID = 1,
                    HouseID = person.HouseID,
                    State = "FL",
                    City = "Coral Springs",
                    Address = "10 Main Street",
                    ContactName = person.FirstName + " " + person.LastName

                });

                var profile = repProfile.GetById(sponsorProfile.SponsorProfileID);
                Assert.IsTrue(profile != null);
                var name = "UpdatedCompany";
                profile.SpoName = name;
                repProfile.Insert(profile);
                profile = repProfile.GetById(sponsorProfile.SponsorProfileID);
                Assert.IsTrue(profile != null);
                Assert.IsTrue(profile.SpoName == name);
                repProfile.Delete(profile);
                profile = repProfile.GetById(sponsorProfile.SponsorProfileID);
                Assert.IsTrue(profile == null);

            }
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("SponsorProfile")]
        public void GetByIdSponsorProfileTest()
        {
            var context = new CSBCDbContext();
            var repProfile = new SponsorProfileRepository(context);
            var profile = context.SponsorProfiles.First<SponsorProfile>();
            var profile2 = repProfile.GetById(profile.SponsorProfileID);
            Assert.IsTrue(profile2 != null);
            Assert.IsTrue(profile2.SponsorProfileID == profile.SponsorProfileID);
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("SponsorProfile")]
        public void SearchSponsorProfileTest()
        {
            using (var db = new CSBCDbContext())
            {
                var repProfile = new SponsorProfileRepository(db);
                var sponsors = repProfile.GetAll(1);
                Assert.IsTrue(sponsors.Any());
            }
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("SponsorProfile")]
        public void SearchSponsorProfileTest2()
        {
            using (var db = new CSBCDbContext())
            {
                var repProfile = new SponsorProfileRepository(db);
                var sponsors = repProfile.GetAll(1, "Fa");
                Assert.IsTrue(sponsors.Any());
            }
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("SponsorProfile")]
        public void GetSponsorsNotInSeasonTest()
        {
            using (var db = new CSBCDbContext())
            {
                var repProfile = new SponsorProfileRepository(db);
                var sponsors = repProfile.GetSponsorsNotInSeason(1, 9);
                Assert.IsTrue(sponsors.Any());
            }
        }
        [TestMethod]
        [TestCategory("Model"), TestCategory("SponsorProfile")]
        public void GetSponsorsInSeasonTest()
        {
            using (var db = new CSBCDbContext())
            {
                var repProfile = new SponsorProfileRepository(db);
                var sponsors = repProfile.GetSponsorsInSeason(1, 9);
                Assert.IsTrue(sponsors.Any());
            }
        }
    }
}
