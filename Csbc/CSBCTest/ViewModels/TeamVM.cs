using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CSBC.Core.Models;
using CSBC.Core.Data;

namespace CSBC.Admin.Web.ViewModels
{
    public class TeamVM
    {
        public List<vw_Team> GetDivisionTeams(int divisionId)
        {
            List<vw_Team> vwTeams = new List<vw_Team>();
            using (var db = new CSBCDbContext())
            {
                foreach (Team t in db.Teams.Where(t => t.DivisionID == divisionId).ToList<Team>())
                {
                    var vw = new vw_Team();
                    vw.DivisionID = t.DivisionID;
                    vw.TeamID = t.TeamID;
                    vw.TeamName = t.TeamName;
                    vw.TeamNumber = t.TeamNumber;
                    if (t.Color != null)
                    {
                        vw.TeamColor = t.Color.ColorName;
                        vw.TeamColorID = t.TeamColorID;
                    }
                    else
                    {
                        vw.TeamColor = String.Empty;
                        vw.TeamColorID = 0;
                    }
                    if (t.Coach != null)
                    {
                        vw.CoachID = t.CoachID;
                        vw.CoachPhone = t.Coach.Person.Household.Phone;
                        vw.CoachName = t.Coach.Person.FirstName + " " + t.Coach.Person.LastName;
                    }
                    else
                    {
                        vw.CoachID = 0;
                        vw.CoachPhone = String.Empty;
                        vw.CoachName = "No Coach";
                    }
                    vwTeams.Add(vw);
                }
            }
            return vwTeams;
        }
    }

    public partial class vw_Team
    {
        [Key]
        public int TeamID { get; set; }
        public int SeasonID { get; set; }
        public int DivisionID { get; set; }
        public Nullable<int> CoachID { get; set; }
        public string TeamName { get; set; }
        [MaxLength(25)]
        public string TeamColor { get; set; }
        public int TeamColorID { get; set; }
        [MaxLength(4)]
        public string TeamNumber { get; set; }
        public string CoachName { get; set; }
        public string CoachPhone { get; set; }
        public string CoachCell { get; set; }
    }
    
}