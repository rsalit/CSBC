using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CSBC.Core.Data;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Admin.Web.ViewModels;

namespace CSBC.Admin.Web.ViewModels
{
    public class ScheduleGamesVM
    {

        public enum GameTypes { Regular, Playoff }

        public int ScheduleNumber { get; set; }
        public int DivisionId { get; set; }
        public DateTime GameDate { get; set; }
        public string GameTime { get; set; }
        public GameTypes GameType { get; set; }
        public string Description { get; set; }
        public string Division { get; set; }
        public int LocationNumber { get; set; }
        public string LocationName { get; set; }
        public int GameNumber { get; set; }

        public string HomeTeam { get; set; }
        public string VisitorTeam { get; set; }

        public IQueryable<ScheduleGamesVM> GetGames(int seasonId, int divisionId)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new ScheduleGameRepository(db);
                var games = (from g in db.ScheduleGames
                             join l in db.ScheduleLocations on g.LocationNumber equals l.LocationNumber
                             join sd in db.ScheduleDivisions on g.ScheduleNumber equals sd.ScheduleNumber
                             join d in db.Divisions on sd.ScheduleName equals d.Div_Desc
                             where g.ScheduleNumber == divisionId
                             where d.SeasonID == seasonId
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
                                 g.HomeTeamNumber,
                                 g.VisitingTeamNumber,
								 HomeTeam = db.Set<Team>().FirstOrDefault(t => t.TeamNumber == g.HomeTeamNumber.ToString()).TeamName,
                                 VisitorTeam = db.Set<Team>().FirstOrDefault(t => t.TeamNumber == g.VisitingTeamNumber.ToString()).TeamName
                             }) as IQueryable<ScheduleGamesVM>;
                foreach (var g in games)
                {
                    g.GameType = GameTypes.Regular;
                }
                return games;
            }
        }
        public static List<ScheduleGamesVM> GetGames(int seasonId, int divisionId, DateTime date)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new ScheduleGameRepository(db);
                var games = (from g in db.ScheduleGames
                             join l in db.ScheduleLocations on g.LocationNumber equals l.LocationNumber
                             join sd in db.ScheduleDivisions on g.ScheduleNumber equals sd.ScheduleNumber
                             join sdh in db.ScheduleDivTeams on g.HomeTeamNumber equals sdh.TeamNumber
                             join sdv in db.ScheduleDivTeams on g.VisitingTeamNumber equals sdv.TeamNumber
                             join d in db.Divisions on sd.ScheduleName equals d.Div_Desc
                             where (d.SeasonID == seasonId)

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
                                 HomeTeam = (int)sdh.ScheduleTeamNumber,
                                 VisitorTeam = (int)sdv.ScheduleTeamNumber
                             });
                var schedGames = new List<ScheduleGamesVM>();
                foreach (var g in games)
                {
                    var game = new ScheduleGamesVM();

                    game.ScheduleNumber = g.ScheduleNumber;
                    game.DivisionId = g.DivisionID;
                    game.GameDate = (DateTime)g.GameDate;
                    game.GameTime = g.GameTime;
                    game.Division = g.Div_Desc;
                    game.LocationNumber = (int)g.LocationNumber;
                    game.LocationName = g.LocationName;
                    game.GameNumber = g.GameNumber;
                    game.HomeTeam = g.HomeTeam.ToString();
                    game.VisitorTeam = g.VisitorTeam.ToString();
                    game.GameType = GameTypes.Regular;
                    schedGames.Add(game);
                }

                if (date != DateTime.MinValue) {
                    schedGames = schedGames.Where(g => g.GameDate.Date == date.Date)
                    .OrderBy(g => g.GameDate).ThenBy(g => g.GameTime).ToList<ScheduleGamesVM>();
                }
                if (divisionId != 0) {
                    schedGames = schedGames.Where(g => g.ScheduleNumber == divisionId)
                    .OrderBy(g => g.GameDate).ThenBy(g => g.GameTime).ThenBy(g => g.DivisionId).ToList<ScheduleGamesVM>();
                }
                    
                return schedGames;
            }
        }
        public static List<ScheduleGamesVM> GetPlayoffGames(int seasonId, int divisionId, DateTime date)
        {
            using (var db = new CSBCDbContext())
            {
                //var rep = new SchedulePlayoffRepository(db);
                var games = (from g in db.SchedulePlayoffs
                             join l in db.ScheduleLocations on g.LocationNumber equals l.LocationNumber
                             join sd in db.ScheduleDivisions on g.ScheduleNumber equals sd.ScheduleNumber
                             join d in db.Divisions on sd.ScheduleName equals d.Div_Desc
                             where (d.SeasonID == seasonId)

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
                                 g.VisitingTeam
                             });
                var schedGames = new List<ScheduleGamesVM>();
                foreach (var g in games)
                {
                    var game = new ScheduleGamesVM();

                    game.ScheduleNumber = g.ScheduleNumber;
                    game.DivisionId = g.DivisionID;
                    game.GameDate = (DateTime)g.GameDate;
                    game.GameTime = g.GameTime;
                    game.Division = g.Div_Desc;
                    game.LocationNumber = (int)g.LocationNumber;
                    game.LocationName = g.LocationName;
                    game.GameNumber = g.GameNumber;
                    game.HomeTeam = g.HomeTeam;
                    game.VisitorTeam = g.VisitingTeam;

                    game.GameType = GameTypes.Playoff;
                    schedGames.Add(game);
                }
                if (date != DateTime.MinValue)
                {
                    schedGames = schedGames.Where(g => g.GameDate.Date == date.Date)
                    .OrderBy(g => g.GameDate).ThenBy(g => g.GameTime).ToList<ScheduleGamesVM>();
                }
                if (divisionId != 0)
                {
                    schedGames = schedGames.Where(g => g.ScheduleNumber == divisionId)
                    .OrderBy(g => g.GameDate).ThenBy(g => g.GameTime).ThenBy(g => g.DivisionId).ToList<ScheduleGamesVM>();
                }
                return schedGames;
            }
        }
        public static int GetScheduleNumber(string divisionName)
        {
            using (var db = new CSBCDbContext())
            {
                var scheduleNo = db.Set<ScheduleDivision>().FirstOrDefault(s => s.ScheduleName.ToUpper() == divisionName.ToUpper()).ScheduleNumber;
                return scheduleNo;
            }
        }

        public static ScheduleGamesVM GetGame(int scheduleNo, int gameNo, int seasonId)
        {
            using (var db = new CSBCDbContext())
            {
                var games = (from g in db.ScheduleGames
                             join l in db.ScheduleLocations on g.LocationNumber equals l.LocationNumber
                             join sd in db.ScheduleDivisions on g.ScheduleNumber equals sd.ScheduleNumber
                             join sdh in db.ScheduleDivTeams on g.HomeTeamNumber equals sdh.TeamNumber
                             join sdv in db.ScheduleDivTeams on g.VisitingTeamNumber equals sdv.TeamNumber
                             join d in db.Divisions on sd.ScheduleName equals d.Div_Desc
                             where (g.ScheduleNumber == scheduleNo && g.GameNumber == gameNo && d.SeasonID == seasonId)

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
                                 HomeTeam = (int)sdh.ScheduleTeamNumber,
                                 VisitorTeam = (int)sdv.ScheduleTeamNumber
                             });
                var schedGames = new List<ScheduleGamesVM>();
                foreach (var g in games)
                {
                    var game = new ScheduleGamesVM
                    {
                        ScheduleNumber = g.ScheduleNumber,
                        DivisionId = g.DivisionID,
                        GameDate = (DateTime)g.GameDate,
                        GameTime = g.GameTime,
                        Division = g.Div_Desc,
                        LocationNumber = (int)g.LocationNumber,
                        LocationName = g.LocationName,
                        GameNumber = g.GameNumber,
                        HomeTeam = g.HomeTeam.ToString(),
                        VisitorTeam = g.VisitorTeam.ToString()
                    };
                    game.GameType = GameTypes.Regular;
                    schedGames.Add(game);
                }
                //var game = db.ScheduleGames.FirstOrDefault(s => s.ScheduleNumber == scheduleNo && s.GameNumber == gameNo);
                var sgame = schedGames.FirstOrDefault<ScheduleGamesVM>();
                return sgame;

            }
        }
        public static ScheduleGame GetByScheduleAndGameNo(int scheduleNo, int gameNo)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new ScheduleGameRepository(db);
                var game = rep.GetByScheduleAndGameNo(scheduleNo, gameNo);
                return game;
            }
        }
        public static SchedulePlayoff GetPlayoffByScheduleAndGameNo(int scheduleNo, int gameNo)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new SchedulePlayoffRepository(db);
                var game = rep.GetByScheduleAndGameNo(scheduleNo, gameNo);
                return game;
            }
        }
   
        internal static ScheduleGamesVM GetPlayoffGame(int scheduleNo, int gameNo, int seasonId)
        {
            using (var db = new CSBCDbContext())
            {
                var games = (from g in db.SchedulePlayoffs
                             join l in db.ScheduleLocations on g.LocationNumber equals l.LocationNumber
                             join sd in db.ScheduleDivisions on g.ScheduleNumber equals sd.ScheduleNumber
                             join d in db.Divisions on sd.ScheduleName equals d.Div_Desc
                             where (g.ScheduleNumber == scheduleNo && g.GameNumber == gameNo && d.SeasonID == seasonId)

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
                var schedGames = new List<ScheduleGamesVM>();
                foreach (var g in games)
                {
                    var game = new ScheduleGamesVM
                    {
                        ScheduleNumber = g.ScheduleNumber,
                        DivisionId = g.DivisionID,
                        GameDate = (DateTime)g.GameDate,
                        GameTime = g.GameTime,
                        Division = g.Div_Desc,
                        LocationNumber = (int)g.LocationNumber,
                        LocationName = g.LocationName,
                        GameNumber = g.GameNumber,
                        HomeTeam = g.HomeTeam,
                        VisitorTeam = g.VisitingTeam,
                        Description = g.Descr
                    };
                    game.GameType = GameTypes.Playoff;
                    schedGames.Add(game);
                }
            var sgame = schedGames.FirstOrDefault<ScheduleGamesVM>();
            return sgame;
            }
        }

        internal static void AddPlayoffGame(int scheduleNo, DateTime? date, string gameTime, int locationNumber, 
            string homeTeam, string visitorTeam, string description)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new SchedulePlayoffRepository(db);
                var game = new SchedulePlayoff
                {
                    ScheduleNumber = scheduleNo,
                    GameDate = date,
                    GameTime = gameTime,
                    LocationNumber = locationNumber,
                    HomeTeam = homeTeam,
                    VisitingTeam = visitorTeam,
                    Descr = description
                };
                rep.Insert(game);
                db.SaveChanges();
            }
        }
        internal static int GetScheduleTeamNumberFromTeamNumber(int scheduleNumber, int teamNo)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new ScheduleDivTeamsRepository(db);
                return rep.GetTeamNo(scheduleNumber, teamNo);
            }
        }
    }
}