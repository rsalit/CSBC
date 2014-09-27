using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Core.Data;
using CSBC.Admin.Web.ViewModels;

namespace CSBC.Admin.Test
{
    public class TestUtils
    {
        CSBCDbContext Context;
        public const int CompanyId = 1;
        public List<string> HouseholdLastNames = new List<string>(new string[] { "Fallon", "Leno", "Obrien", "Letterman", "Morgan" });
        public List<string> FirstNames = new List<string>(new string[] { "Mike", "Rich", "Conan", "David", "Fred" });
        public List<string> ColorNames = new List<string>(new string[] { "Redi", "Bluei", "Greeni", "Yellowi", "Blacki", "Whitei", "Orangei", "Heatheri", "Tani" });

        public const string Household1 = "Leno";
        public const string Household2 = "Smith";
        public const string Household3 = "Jones";
        public const string Household4 = "Lesh";
        public const string Household5 = "Weir";
        public const string PersonFirstName1 = "John";
        public const string PersonFirstName2 = "Barry";
        public const string PersonFirstName3 = "Edward";
        public const string PersonFirstName4 = "Richard";
        public const string PersonFirstName5 = "Phil";

        public TestUtils(CSBCDbContext context)
        {
            Context = new CSBCDbContext();
        }

  
        public void InitColors(CSBCDbContext context)
        {
            var rep = new ColorRepository(context);
            for (int i = 0; i < ColorNames.Count; i++)
            {
                rep.Insert(new Color { ColorName = ColorNames[i], CompanyID = 1 });
            }
            rep.Insert(new Color { ColorName = "Chartreuse", CompanyID = 1, Discontinued = true });
            context.SaveChanges();
        }

  
        public void CleanupDb()
        {
            var connection = Context.Database.Connection;
            if (connection.DataSource == "(localDb)\\v11.0")
            {
                var init = new CSBCDbInitializer();
                using (Context)
                {
                    init.DeleteTestPlayers(Context);
                    init.DeleteTestTeams();
                    init.DeleteTestDivisions(Context);
                }
            }
        }

       
   
     

     




    }
}
