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
                return teamRecords.OrderBy(t => t.Pct).ToList();
            }


            //var games = from g in db.ScheduleGames
            //            from d in db.Divisions
            //            where g.DivisionId == d.DivisionID
            //            && g.ScheduleNumber == divisionNo
            //                select new 
            //              {
            //                  DivisionDescription = d.Div_Desc,
            //                  DivisionID = d.DivisionID,
            //                  GameDate = g.GameDate,
            //                  GameTimeString = g.GameTime,
            //                  LocationName = l.LocationName,
            //                  GameNumber = g.GameNumber,
            //                  VisitingTeamNumber = g.VisitingTeamNumber,
            //                  HomeTeamNumber = g.HomeTeamNumber,
            //                  ScheduleNumber = s.ScheduleNumber
            //              });;


            //var teamRecords = new List<ScheduleStandingsViewModel>();
            //foreach (var game in games)
            //{
            //    ScheduleStandingsViewModel record = new ScheduleStandingsViewModel();
            //    record.TeamNo = (int)row[0];
            //    record.TeamName = (string)row[1];
            //    record.ScheduleName = (string)row[2];
            //    record.DivNo = (string)row[3];
            //    record.Team = (string)row[4];
            //    record.Won = (int)row[5];
            //    record.Lost = (int)row[6];
            //    record.Pct = (decimal)row[7];
            //    record.Tiebreaker = (int)row[8];
            //    record.Streak = (int)row[9];
            //    record.PF = (int)row[10];
            //    record.PA = (int)row[11];
            //    record.GB = (int)row[12];
            //    teamRecords.Add(record);
            //}

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
                var scheduleTeamNo = GetTeamNo(games.First().ScheduleNumber, teamNo);
                var seasonRecord = new ScheduleStandingsViewModel
                   {
                       TeamName = team.TeamName,
                       DivNo = team.DivisionID.ToString(),
                       Won = 0,
                       Lost = 0,
                       PF = 0,
                       PA = 0
                   };

                var records = games.Where(g => g.HomeTeamNumber == teamNo || g.VisitingTeamNumber == teamNo);
                foreach (var record in records)
                {
                    if (record.HomeTeamScore > 0 || record.VisitingTeamScore > 0)
                    {
                        if (teamNo == record.HomeTeamNumber)
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
                            if (teamNo == record.VisitingTeamNumber)
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
                    seasonRecord.Pct = seasonRecord.Won / (seasonRecord.Won + seasonRecord.Lost) / 100;
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
                return db.Set<ScheduleDivTeam>().FirstOrDefault(t => t.DivisionNumber == divisionNo && t.ScheduleTeamNumber == teamNo).ScheduleTeamNumber;
            }

        }

        private List<Team> GetDivisionTeams(int divisionNo)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new TeamRepository(db);
                var teams = rep.GetDivisionTeams(divisionNo);
                return teams.ToList<Team>();
            }
        }
    }
}