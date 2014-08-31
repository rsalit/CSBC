using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Lojack.Data;
using Lojack.Models;
using Lojack.Interfaces;
using Lojack.Repositories;
using RPDSS.Data;

namespace TestLojack
{
    [TestClass]
    public class MatchedLojackHitUpdateTest
    {
        public Guid _userGuid;

        [TestInitialize]
        public void InitTest()
        {
            _userGuid = new Guid("97F21818-4924-4857-8557-EE9DCCC17768");
        }
        [TestMethod]
        public void MatchedLojackHitUpdateInsertTest()
        {
            using (var db = new LojackContext())
            {
                var rep = new MatchedLojackHitUpdateRepository(db);
                var hitUpdate = new MatchedLojackHitUpdate
                {
                    UpdateGUID = Guid.NewGuid(),
                    UpdateDescription = "Test record for matched update",
                    UpdateTypeID = 1,
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                    UserGUID = _userGuid,
                    HitGUID = new Guid("3890C846-F9DA-4297-8B67-0A9320FFDB6B")
                };
                rep.Insert(hitUpdate);


            }
        }
    }
}
