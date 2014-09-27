using System;
using System.Collections.Generic;

namespace Lojack.Models
{
    public partial class MatchedLojack
    {
        public System.Guid HitGUID { get; set; }
        public System.Guid TransactionGuid { get; set; }
        public Nullable<System.Guid> JurisdictionGuid { get; set; }
        public Nullable<System.DateTime> HitCreationDate { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<System.DateTime> StatusChangeDate { get; set; }
        public string matchtype { get; set; }
        public string dateendregistration { get; set; }
        public Nullable<System.DateTime> entryCreationDate { get; set; }
        public string computracefile { get; set; }
        public Nullable<bool> resolved { get; set; }
        public Nullable<System.DateTime> resolutionDate { get; set; }
        public Nullable<System.Guid> investigatedby { get; set; }
        public Nullable<bool> discarded { get; set; }
        public string matchedserial { get; set; }
        public Nullable<System.Guid> PropertyGuid { get; set; }
        public string propertydescription { get; set; }
        public Nullable<System.DateTime> transactioneffectivedate { get; set; }
        public string custfirstname { get; set; }
        public Nullable<System.DateTime> custdob { get; set; }
        public string custcity { get; set; }
        public string custstate { get; set; }
        public string custlastname { get; set; }
    }
}
