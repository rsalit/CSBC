using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CSBC.Core.Models;

namespace CSBC.Admin.Web.ViewModels
{
    public class SeasonSummary
    {
        public string DivisionName { get; set; }
        public int Players { get; set; }
        public int Coaches { get; set; }
        public int Sponsors { get; set; }
        public int OnlinePlayers { get; set; }
        public int OnlineCoaches { get; set; }
        public int OnlineSponsors { get; set; }
        public List<Summary> SeasonSummaryList { get; set; }

        public SeasonSummary()
        {
        }

    }
}
