using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CSBC.Core.Models;

namespace CSBC.Admin.Web.ViewModels
{
    public class CoachVM
    {
        //private List<vw_Coaches> coaches;
        public List<vw_Coaches> Coaches
        {
            get { return GetCoaches(); }
        }
        public List<Player> Players { get; set; }

        public int CoachID { get; set; }
        public int CompanyID { get; set; }      
        public Object SeasonID { get; set; }
        public Object PeopleID { get; set; }
        public Object PlayerID { get; set; }
        public string ShirtSize { get; set; }
        public string CoachPhone { get; set; }
        public Object CreatedDate { get; set; }
        public string CreatedUser { get; set; }

        public string Name { get; set; }
        public string Housephone { get; set; }
        public string Cellphone { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public List<string> ShirtSizes= new List<string> 
            {
                "Small", "Medium", "Large"
            }; 

        public static List<string> GetShirtSizes()
        {
            var shirtSizes = new List<string> 
            {
                "Small", "Medium", "Large"
            };
            return shirtSizes;
        }
        public List<vw_Coaches> GetCoaches()
        {
            var info = new List<vw_Coaches>
            { 
                new vw_Coaches { CoachID=1, CoachPhone = "954-338-3345", CompanyID = 2, PeopleID=2, Name = "Phil Lesh"  }, 
                new vw_Coaches { CoachID=2, CoachPhone = "954-373-3375", CompanyID = 2, PeopleID=3, Name = "Bob Weir" }, 
                new vw_Coaches { CoachID=3, CoachPhone = "954-353-2345", CompanyID = 2, PeopleID=6, Name = "Freddie King" }

                };
            return info;
        }
    }
}