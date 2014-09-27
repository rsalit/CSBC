using System;
using System.Collections.Generic;

namespace Lojack.Models
{
    public partial class MatchedLojackHitUpdate
    {
        public System.Guid UpdateGUID { get; set; }
        public string UpdateDescription { get; set; }
        public Nullable<int> UpdateTypeID { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public System.Guid UserGUID { get; set; }
        public System.Guid HitGUID { get; set; }
    }
}
