using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lojack.Models
{
     [Table]
    public partial class LojackAuditProcess
    {
        public LojackAuditProcess()
        {
        }

        //[Key]
       
        public int LojackAuditProcessId { get; set; }
        public string FileName { get; set; }
        public DateTime ProcessDateTime { get; set; }
        public string Status { get; set; }
        public int RecordsProcessed { get; set; }
        public int TotalRecords { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
