using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using System.Data.Entity;
using System.Data.Linq;
using System.Web;
using CSBC.Core.Data;
using CSBC.Core.Models;
using CSBC.Core.Repositories;

namespace Csbchoops.Web.ViewModels
{
    public class GameSchedulesViewModel
    {
        public int DivisionID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> SeasonID { get; set; }
        public string DivisionDescription { get; set; }
        public DateTime GameDate { get; set; }
        public String GameTimeString { get; set; }
        public DateTime GameTime { get; set; }
        public string LocationName { get; set; }
        public int GameNumber { get; set; }
        public int? VisitingTeamNumber { get; set; }
        public int? HomeTeamNumber { get; set; }
        public int VisitingTeamSeasonNumber { get; set; }
        public int HomeTeamSeasonNumber { get; set; }
        public string VisitingTeamName { get; set; }
        public string HomeTeamName { get; set; }
        public int ScheduleNumber { get; set; }
        public int HomeTeamScore { get; set; }
        public int VisitingTeamScore { get; set; }
        public string GameDescription { get; set; }

        public List<GameSchedulesViewModel> GetGames(int seasonId)
        {
            using (var db = new CSBCDbContext())
            {
                var result = (from d in db.Divisions
                              from s in db.ScheduleDivisions
                              from g in db.ScheduleGames
                              from l in db.ScheduleLocations

                              where (d.Div_Desc == s.ScheduleName)
                              where g.SeasonId == seasonId
                              where s.ScheduleNumber == g.ScheduleNumber
                              where g.LocationNumber == l.LocationNumber
                              select new GameSchedulesViewModel
                              {
                                  DivisionDescription = d.Div_Desc,
                                  DivisionID = d.DivisionID,
                                  GameDate = g.GameDate,
                                  GameTimeString = g.GameTime,
                                  LocationName = l.LocationName,
                                  GameNumber = g.GameNumber,
                                  VisitingTeamNumber = g.VisitingTeamNumber,
                                  HomeTeamNumber = g.HomeTeamNumber,
                                  ScheduleNumber = s.ScheduleNumber,
                                  HomeTeamScore = (int)g.HomeTeamScore,
                                  VisitingTeamScore = (int)g.VisitingTeamScore
                              });

                List<TeamViewModel> teams;
                var games = GetTeamNamesFromScheduledGames(db, result);
                var playoffGames = GetPlayoffGames(seasonId);
                games.AddRange(playoffGames);
                return games;
            }
        }


        public List<GameSchedulesViewModel> GetGames(int seasonId, int divisionId)
        {
            using (var db = new CSBCDbContext())
            {
                var result = (from d in db.Divisions
                              from g in db.ScheduleGames
                              from l in db.ScheduleLocations
                              where g.SeasonId == seasonId
                              where g.DivisionId == divisionId
                              where g.LocationNumber == l.LocationNumber
                              where d.DivisionID == g.DivisionId
                              select new GameSchedulesViewModel
                              {
                                  DivisionDescription = d.Div_Desc,
                                  DivisionID = d.DivisionID,
                                  GameDate = g.GameDate,
                                  GameTimeString = g.GameTime,
                                  LocationName = l.LocationName,
                                  GameNumber = g.GameNumber,
                                  VisitingTeamNumber = g.VisitingTeamNumber,
                                  HomeTeamNumber = g.HomeTeamNumber,
                                  ScheduleNumber = g.ScheduleNumber,
                                  HomeTeamScore = (int)g.HomeTeamScore,
                                  VisitingTeamScore = (int)g.VisitingTeamScore
                              });
                List<TeamViewModel> teams;
                var games = GetTeamNamesFromScheduledGames(db, result);
                var playoffGames = GetPlayoffGames(divisionId);
                games.AddRange(playoffGames);

                return games;
            }
        }
        public List<GameSchedulesViewModel> GetGames(int seasonId, int divisionId, int teamId)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new TeamRepository(db);
                var team = rep.GetById(teamId);
                int teamNo;
                if (Int32.TryParse(team.TeamNumber, out teamNo))
                {
                    var scheduleNumber = db.Set<ScheduleGame>().FirstOrDefault(g => g.DivisionId == divisionId).ScheduleNumber;
                    var teamNumber = db.Set<ScheduleDivTeam>()
                        .FirstOrDefault(s => s.ScheduleTeamNumber == teamNo && s.ScheduleNumber == scheduleNumber).TeamNumber;

                    var result = (from t in db.Teams
                                  from g in db.ScheduleGames
                                  from l in db.ScheduleLocations

                                  where g.SeasonId == seasonId
                                  where t.TeamID == teamId
                                  where (g.VisitingTeamNumber == teamNumber || g.HomeTeamNumber == teamNumber)
                                  where g.ScheduleNumber == scheduleNumber
                                  where g.LocationNumber == l.LocationNumber
                                  select new GameSchedulesViewModel
                                  {
                                      DivisionID = t.DivisionID,
                                      GameDate = g.GameDate,
                                      GameTimeString = g.GameTime,
                                      LocationName = l.LocationName,
                                      GameNumber = g.GameNumber,
                                      VisitingTeamNumber = g.VisitingTeamNumber,
                                      HomeTeamNumber = g.HomeTeamNumber,
                                      ScheduleNumber = g.ScheduleNumber,
                                      HomeTeamScore = (int)g.HomeTeamScore,
                                      VisitingTeamScore = (int)g.VisitingTeamScore
                                  });
                    List<TeamViewModel> teams;
                    //var count = result.Count<GameSchedulesViewModel>();
                    var games = GetTeamNamesFromScheduledGames(db, result);
                    var playoffGames = GetPlayoffGames(divisionId);
                    games.AddRange(playoffGames);
                    return games;
                }
                else
                {
                    return new List<GameSchedulesViewModel>();
                }
            }
        }
        private static List<GameSchedulesViewModel> GetTeamNamesFromScheduledGames(CSBCDbContext db, IQueryable<GameSchedulesViewModel> result)
        {
            List<GameSchedulesViewModel> games = new List<GameSchedulesViewModel>();
            var teams = TeamViewModel.GetDivisionTeams(result.FirstOrDefault<GameSchedulesViewModel>().DivisionID);
            var schedDiv = db.Set<ScheduleDivTeam>().Select(s => s).ToList<ScheduleDivTeam>();
            foreach (GameSchedulesViewModel game in result)
            {
                //first get real game time
                DateTime time;
                if (DateTime.TryParse(game.GameTimeString, out time))
                    game.GameTime = time;
                game.GameDate = CombineDateAndTime(game.GameDate, game.GameTime);
                game.VisitingTeamSeasonNumber = schedDiv.FirstOrDefault(s => s.ScheduleNumber == game.ScheduleNumber &&
                    s.TeamNumber == game.VisitingTeamNumber).ScheduleTeamNumber;

                if (teams.Any())
                {
                    var team = teams.FirstOrDefault(t => t.TeamNo == game.VisitingTeamSeasonNumber);
                    if (team != null)
                        game.VisitingTeamName = team.TeamName;
                }
                game.HomeTeamSeasonNumber = schedDiv.FirstOrDefault(s => s.ScheduleNumber == game.ScheduleNumber &&
                    s.TeamNumber == game.HomeTeamNumber).ScheduleTeamNumber;

                if (teams.Any()) ////get teamID!
                {
                    var team = teams.FirstOrDefault(t => t.TeamNo == game.HomeTeamSeasonNumber);
                    if (team != null)
                        game.HomeTeamName = team.TeamName;
                }
                games.Add(game);
            }
            return games;

        }
        private List<GameSchedulesViewModel> GetPlayoffGames(int divisionId)
        {
            using (var db = new CSBCDbContext())
            {
                var games = (from g in db.SchedulePlayoffs
                             from l in db.ScheduleLocations
                             from d in db.Divisions
                             where g.DivisionId == divisionId
                             where g.LocationNumber == l.LocationNumber
                             where g.DivisionId == d.DivisionID
                             select new
                             {
                                 g.ScheduleNumber,
                                 d.DivisionID,
                                 g.GameDate,
                                 g.GameTime,
                                 d.Div_Desc,
                                 g.LocationNumber,
                                 l.LocationName,
                                 g.GameNumber,
                                 g.HomeTeam,
                                 g.VisitingTeam,
                                 g.Descr
                             });
                var schedGames = new List<GameSchedulesViewModel>();
                DateTime time;
                foreach (var g in games)
                {
                    var game = new GameSchedulesViewModel();

                    game.ScheduleNumber = g.ScheduleNumber;
                    //game.DivisionId = g.DivisionID;
                    game.GameDate = (DateTime)g.GameDate;
                    if (DateTime.TryParse(g.GameTime, out time))
                        game.GameTime = time;
                    //game.D = g.Div_Desc;
                    //game.LocationNumber = (int)g.LocationNumber;
                    game.LocationName = g.LocationName;
                    game.GameNumber = g.GameNumber;
                    game.HomeTeamName = g.HomeTeam;
                    game.VisitingTeamName = g.VisitingTeam;
                    game.GameDescription = g.Descr;
                    //game.GameType = GameTypes.Playoff;
                    schedGames.Add(game);
                }

                if (divisionId != 0)
                {
                    schedGames = schedGames
                    .OrderBy(g => g.GameDate).ThenBy(g => g.GameTime).ThenBy(g => g.DivisionID).ToList<GameSchedulesViewModel>();
                }
                return schedGames;
            }
        }
        private static DateTime CombineDateAndTime(DateTime date, DateTime time)
        {
            var dateString = date.ToShortDateString() + " " + time.ToShortTimeString();
            DateTime newDate = date;
            if (DateTime.TryParse(dateString, out newDate))
                date = newDate;
            return newDate;
        }

        public static void UpdateScore(int gameNo, int divisionId, int homeScore, int visitorScore)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new ScheduleGameRepository(db);
                var game = db.ScheduleGames.FirstOrDefault(g => g.GameNumber == gameNo && g.DivisionId == divisionId);
                if (game != null)
                {
                    game.HomeTeamScore = homeScore;
                    game.VisitingTeamScore = visitorScore;
                    rep.Update(game);

                }

            }
        }

    }


}