using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using CSBC.Core.Data;
using CSBC.Core.Models;
using CSBC.Components;
using CSBC.Core.Repositories;

namespace CSBC.Admin.Web.ViewModels
{
    public class ColorVM
    {
        public List<Color> Colors { get; set; }
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        //[MaxLength(20)]
        //public Nullable<int> CompanyID { get; set; }
        [Display(Name="Color")]
        public string ColorName { get; set; }
        public Object Discontinued { get; set; }
        //public Nullable<System.DateTime> CreatedDate { get; set; }
        //public string CreatedUser { get; set; }
        
        public ColorVM()
        {
            //Colors = Colors;
        }

        public IQueryable<Color> GetAllRecords1(int companyId)
        {
            //var oColors = new CSBC.Components.Color.ClsColors();
            //DataTable rsData = new DataTable("Colors");
            var rep = new ColorRepository(new Core.Data.CSBCDbContext());
            var colors = rep.GetAll(companyId); //replace with Company ID at some point!

            return colors;
        }
        public List<ColorVM> GetRecords(int companyId)
        {
            //var oColors = new CSBC.Components.Color.ClsColors();
            //DataTable rsData = new DataTable("Colors");
            var rep = new ColorRepository(new CSBCDbContext());
            var Colors = rep.GetAll(companyId);
            List<ColorVM> colorVm = new List<ColorVM>();
            foreach(Color s in Colors)
            {
                var color = new ColorVM
                {
                    ID = s.ID,
                    ColorName = s.ColorName,
                    Discontinued = s.Discontinued
                };
                colorVm.Add(color);
            }
            return colorVm;
        }
        public static bool ColorUsed(int id)
        {
            using (var db = new CSBCDbContext())
            {
                return db.Teams.Any(t => t.TeamColorID == id);
            }
        }
        public static void Insert(Color entity)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new ColorRepository(db);
                rep.Insert(entity);
                db.SaveChanges();
            }
        }
    }
}