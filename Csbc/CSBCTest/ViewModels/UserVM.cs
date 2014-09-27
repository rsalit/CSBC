using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSBC.Admin.Web.ViewModels
{
    public class UserVM
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }

        public List<String> Roles = new List<String> {"Calendar", "Coaches", "Colors", "Content", "Directors", "Divisions", "Emails", "Games", "Households", "Payments", "People", "Refunds", "Scores", "Seasons", "Sponsors", "Teams", "Users" };
        
        
    }
}