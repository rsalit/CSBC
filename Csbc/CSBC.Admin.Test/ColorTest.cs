using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Linq.Expressions;
using CSBC.Core.Models;
using CSBC.Core.Data;
using CSBC.Admin.Web.ViewModels;
using CSBC.Core.Repositories;

namespace CSBC.Admin.Test
{
    [TestClass]
    public class ColorTest
    {
        [TestInitialize]
        public void InitTest()
        {
            var tester = new TestUtils(new CSBCDbContext());
            tester.InitColors(new CSBCDbContext());
        }

        [TestMethod]
        [TestCategory("Model")]
        public void GetColorsTest()
        {
            var rep = new ColorRepository(new CSBCDbContext());
            var colors = rep.GetAll();
            var numberOfColors = colors.Count<Color>();
            Assert.IsTrue(numberOfColors > 1);
        }
        [TestMethod]
        [TestCategory("ViewModel")]
        public void GetColorVMTest()
        {
            var rep = new ColorVM();
            var colors = rep.GetRecords(1);
            Assert.IsTrue(colors.Count > 1);
        }

        [TestMethod]
        [TestCategory("ViewModel")]
        public void GetSponsorVMTest()
        {
            var rep = new SponsorVM();
            var sponsors = rep.GetSeasonSponsors(9);//get a valid sponsor season
            Assert.IsTrue(sponsors.Any());
        }

        [TestMethod]
        [TestCategory("Model")]
        public void GetColorByNameTest()
        {
            var name = "Red";
            var rep = new ColorRepository(new CSBCDbContext());
            var color = rep.GetByName(1, name);
            
            Assert.IsTrue(color.ID != 0);
            Assert.IsTrue(color.ColorName == name);
        }
    }
}
