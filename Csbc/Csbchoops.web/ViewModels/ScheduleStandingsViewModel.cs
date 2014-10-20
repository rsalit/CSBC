using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CSBC.Core.Data;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using System.Data;

namespace Csbchoops.Web.ViewModels
{
    public class ScheduleStandingsViewModel
    {
        public int TeamNo { get; set; }
        public string TeamName { get; set; }
        public string ScheduleName { get; set; }
        public string DivNo { get; set; }
        public string Team { get; set; }
        public int Won { get; set; }
        public int Lost { get; set; }
        public decimal Pct { get; set; }
        public int Tiebreaker { get; set; }
        public int Streak { get; set; }
        public int PF { get; set; }
        public int PA { get; set; }
        public decimal GB { get; set; }


        public List<ScheduleStandingsViewModel> GetStandings(int divisionNo)
        {
            //var sql = "Exec GetStanding @ScheduleNumber = " + divisionNo.ToString();
            //DataTable whatIsThis = db.ExecuteGetSQL(sql);
            using (var db = new CSBCDbContext())
            {
                var rep = new ScheduleGameRepository(db); //make this a method here....
                var games = rep.GetSeasonGames(divisionNo).ToList<ScheduleGame>();
                var teams = GetDivisionTeams(divisionNo);
                var teamRecords = GetTeamRecords(teams, games);
                return teamRecords.OrderByDescending(t => t.Pct).ThenByDescending(t => t.Won).ToList();
            }
        }

        private List<ScheduleStandingsViewModel> GetTeamRecords(List<Team> teams, List<ScheduleGame> games)
        {
            using (var db = new CSBCDbContext())
            {
                var teamRecords = new List<ScheduleStandingsViewModel>();
                var rep = new ScheduleGameRepository(db);
                foreach (var team in teams)
                {
                    var teamRecord = GetTeamRecord(team, games);
                    teamRecords.Add(teamRecord);
                }
                return teamRecords;
            }
        }

        private ScheduleStandingsViewModel GetTeamRecord(Team team, List<ScheduleGame> games)
        {
            int teamNo;

            if (Int32.TryParse(team.TeamNumber, out teamNo)) ///getting team no!
            {
                var teamNumber = GetTeamNo(games.First().ScheduleNumber, teamNo);
                var seasonRecord = new ScheduleStandingsViewModel
                   {
                       TeamNo = Convert.ToInt32(team.TeamNumber),
                       TeamName = team.TeamName,
                       DivNo = team.DivisionID.ToString(),
                       Won = 0,
                       Lost = 0,
                       PF = 0,
                       PA = 0
                   };

                var records = games.Where(g => g.HomeTeamNumber == teamNumber || g.VisitingTeamNumber == teamNumber);
                foreach (var record in records)
                {
                    if (record.HomeTeamScore > 0 || record.VisitingTeamScore > 0)
                    {
                        if (teamNumber == record.HomeTeamNumber)
                        {
                            seasonRecord.PA += (int)record.VisitingTeamScore;
                            seasonRecord.PF += (int)record.HomeTeamScore;

                            if (record.HomeTeamScore > record.VisitingTeamScore)
                                seasonRecord.Won++;
                            else
                                seasonRecord.Lost++;
                        }
                        else
                        {
                            if (teamNumber == record.VisitingTeamNumber)
                            {
                                seasonRecord.PF += (int)record.VisitingTeamScore;
                                seasonRecord.PA += (int)record.HomeTeamScore;
                                if (record.VisitingTeamScore > record.HomeTeamScore)
                                    seasonRecord.Won++;
                                else
                                    seasonRecord.Lost++;
                            }
                        }
                    }
                }
                if ((seasonRecord.Won + seasonRecord.Lost) > 0)
                {
                    Decimal Pct = ((decimal)seasonRecord.Won / (decimal)(seasonRecord.Won + seasonRecord.Lost)) * 100;
                    seasonRecord.Pct = Pct;
                }
                else
                    seasonRecord.Pct = 0;
                return seasonRecord;
            }
            else
                return new ScheduleStandingsViewModel();

        }

        private int GetTeamNo(int divisionNo, int teamNo)
        {
            using (var db = new CSBCDbContext())
            {
                return db.Set<ScheduleDivTeam>().FirstOrDefault(t => t.DivisionNumber == divisionNo && t.ScheduleTeamNumber == teamNo).TeamNumber;
            }

        }

        private List<Team> GetDivisionTeams(int divisionNo)
        {
            using (var db = new CSBCDbContext())
            {
                var repoColor = new ColorRepository(db);
                var colors = repoColor.GetAll();
                var rep = new TeamRepository(db);
                var teams = rep.GetDivisionTeams(divisionNo);
                var listTeams = GetTeamNames(teams, colors).ToList<Team>();
                return listTeams;
            }
        }

        private IQueryable<CSBC.Core.Models.Team> GetTeamNames(IQueryable<CSBC.Core.Models.Team> teams, IQueryable<Color> colors)
        {

            foreach (var team in teams)
            {
                //team.Color = colors.FirstOrDefault(c => c.ID == ;
                team.TeamName = team.Color.ColorName + "(" + team.TeamNumber + ")";
            }
            return teams;
        }
    }
}