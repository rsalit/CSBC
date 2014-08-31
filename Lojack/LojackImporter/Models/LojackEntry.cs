using System;
using System.Collections.Generic;

namespace Lojack.Models
{
    public partial class LojackEntry
    {
        public int lojackentryid { get; set; }
        public string computracefile { get; set; }
        public string ReportedSerialNumber { get; set; }
        public Nullable<System.DateTime> StolenDate { get; set; }
        public Nullable<System.DateTime> DateReportedToABT { get; set; }
        public string TheftIncidentNumber { get; set; }
        public string AgencyName { get; set; }
        public string ClosedDate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
    }
}
