using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Web;
using CSBC.Core.Data;
using CSBC.Core.Models;
using CSBC.Core.Repositories;

namespace Csbchoops.Web.ViewModels
{
    public class TeamViewModel
    {
        public int TeamID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public int SeasonID { get; set; }
        public int DivisionId { get; set; }
        public Nullable<int> CoachID { get; set; }
        public Nullable<int> AssCoachID { get; set; }
        public Nullable<int> SponsorID { get; set; }
        [MaxLength(50)]
        public string TeamName { get; set; }
        [MaxLength(50)]
        public string TeamColor { get; set; }
        public int TeamColorID { get; set; }
        public string TeamNumber { get; set; }
        public int TeamNo { get; set; } //kludge for team number being a string in the DB!!

        public static TeamViewModel ConvertRecordForTeamNumber(Team team)
        {
            var newTeam = new TeamViewModel
            {
                TeamID = team.TeamID,
                CompanyID = team.CompanyID,
                SeasonID = team.SeasonID,
                DivisionId = team.DivisionID,
                CoachID = team.CoachID,
                TeamName = team.TeamName,
                TeamNumber = team.TeamNumber,
                TeamColor = team.TeamColor,
                TeamColorID = team.TeamColorID
            };

            int teamNo = 0;
            if (Int32.TryParse(team.TeamNumber, out teamNo))
            {
                newTeam.TeamNo = teamNo;
            }
            if (String.IsNullOrEmpty(team.TeamName))
            {
                if (team.TeamColorID > 0)
                {
                    using (var db = new CSBCDbContext())
                    {
                        newTeam.TeamName = db.Set<Color>().FirstOrDefault(c => c.ID == team.TeamColorID).ColorName + " (" + team.TeamNumber.ToString() + ")";
                    }
                }
                else
                    newTeam.TeamName = team.TeamNumber;
            }
            return newTeam;
        }
        public static List<TeamViewModel> GetSeasonTeams(int seasonId)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new TeamRepository(db);
                var teams = rep.GetSeasonTeams(seasonId);
                var newTeams = new List<TeamViewModel>();
                foreach(Team team in teams)
                {
                    newTeams.Add(ConvertRecordForTeamNumber(team));
                }
                return newTeams;
            }
        }
        public static List<TeamViewModel> GetDivisionTeams(int divisionId)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new TeamRepository(db);
                var teams = rep.GetTeams(divisionId);
                var newTeams = new List<TeamViewModel>();
                foreach (Team team in teams)
                {
                    newTeams.Add(ConvertRecordForTeamNumber(team));
                }
                return newTeams;
            }
        }
    
    
    
    }

    
}