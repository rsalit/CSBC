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
        public int HomeTeamNo { get; set; }
        public int VisitorTeamNo { get; set; }
        public string HomeTeam { get; set; }
        public string VisitorTeam { get; set; }

        //public IQueryable<ScheduleGamesVM> GetGames(int seasonId, int divisionId)
        //{
        //    using (var db = new CSBCDbContext())
        //    {
        //        var rep = new ScheduleGameRepository(db);
        //        var games = (from g in db.ScheduleGames
        //                     from d in db.Divisions
        //                     join l in db.ScheduleLocations on g.LocationNumber equals l.LocationNumber
        //                     join sd in db.ScheduleDivisions on g.ScheduleNumber equals sd.ScheduleNumber
        //                     where d.DivisionID == divisionId
        //                     where d.SeasonID == seasonId
        //                     select new 
        //                     {
        //                         g.ScheduleNumber,
        //                         d.DivisionID,
        //                         g.GameDate,
        //                         g.GameTime,
        //                         d.Div_Desc,
        //                         g.LocationNumber,
        //                         l.LocationName,
        //                         g.GameNumber,
        //                         g.HomeTeamNumber,
        //                         g.VisitingTeamNumber,
        //                         HomeTeamNo = db.Set<ScheduleDivTeam>().FirstOrDefault(t => t.TeamNumber == g.HomeTeamNumber).ScheduleTeamNumber,
        //                         VisitorTeamNo = db.Set<ScheduleDivTeam>().FirstOrDefault(t => t.TeamNumber == g.VisitingTeamNumber).ScheduleTeamNumber
        //                     }) as IQueryable<ScheduleGamesVM>;
        //        foreach (var g in games)
        //        {
        //            g.GameType = GameTypes.Regular;
        //        }
        //        return games;
        //    }
        //}

        public List<ScheduleGamesVM> GetGames(int seasonId, int divisionId, DateTime date)
        {
            var schedGames = GetGames(divisionId);
            if (date != DateTime.MinValue)
            {
                schedGames = schedGames.Where(g => g.GameDate.Date == date.Date)
                .OrderBy(g => g.GameDate).ThenBy(g => g.GameTime).ToList<ScheduleGamesVM>();
            }
            if (divisionId != 0)
            {
                schedGames = schedGames.Where(g => g.DivisionId == divisionId)
                .OrderBy(g => g.GameDate).ThenBy(g => g.GameTime).ThenBy(g => g.DivisionId).ToList<ScheduleGamesVM>();
            }
            return schedGames;
        }

        public List<ScheduleGamesVM> GetGames(int divisionId)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new ScheduleGameRepository(db);
                var games = (from g in db.ScheduleGames
                             from d in db.Divisions
                             from l in db.ScheduleLocations
                             where (g.DivisionId == divisionId)
                             where (d.DivisionID == divisionId)
                             where (g.LocationNumber == l.LocationNumber)
                             select new
                             {
                                 g.ScheduleNumber,
                                 g.DivisionId,
                                 g.GameDate,
                                 g.GameTime,
                                 d.Div_Desc,
                                 g.LocationNumber,
                                 l.LocationName,
                                 g.GameNumber,
                                 HomeTeamNo = db.Set<ScheduleDivTeam>().FirstOrDefault(t => t.TeamNumber == g.HomeTeamNumber).ScheduleTeamNumber,
                                 VisitorTeamNo = db.Set<ScheduleDivTeam>().FirstOrDefault(t => t.TeamNumber == g.VisitingTeamNumber).ScheduleTeamNumber
                             });
                var schedGames = new List<ScheduleGamesVM>();
                var teams = db.Set<Team>().Where(t => t.DivisionID == divisionId).ToList<Team>();
                foreach (var g in games)
                {
                    var game = new ScheduleGamesVM();

                    game.ScheduleNumber = g.ScheduleNumber;
                    game.DivisionId = (int)g.DivisionId;
                    game.GameDate = (DateTime)g.GameDate;
                    game.GameTime = g.GameTime;
                    game.Division = g.Div_Desc;
                    game.LocationNumber = (int)g.LocationNumber;
                    game.LocationName = g.LocationName;
                    game.GameNumber = g.GameNumber;
                    game.HomeTeam = GetTeamName(teams, g.HomeTeamNo);
                    game.VisitorTeam = GetTeamName(teams, g.VisitorTeamNo);
                    game.GameType = GameTypes.Regular;
                    schedGames.Add(game);
                }

                schedGames = schedGames.Where(g => g.DivisionId == divisionId)
                 .OrderBy(g => g.GameDate).ThenBy(g => g.GameTime).ThenBy(g => g.DivisionId).ToList<ScheduleGamesVM>();
                return schedGames;
            }
        }
        public List<ScheduleGamesVM> GetPlayoffGames(int divisionId)
        {
            using (var db = new CSBCDbContext())
            {
                //var rep = new SchedulePlayoffRepository(db);
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

                if (divisionId != 0)
                {
                    schedGames = schedGames.Where(g => g.DivisionId == divisionId)
                    .OrderBy(g => g.GameDate).ThenBy(g => g.GameTime).ThenBy(g => g.DivisionId).ToList<ScheduleGamesVM>();
                }
                return schedGames;
            }
        }
        public List<ScheduleGamesVM> GetPlayoffGames(int seasonId, int divisionId, DateTime date)
        {
            var schedGames = GetPlayoffGames(divisionId);
            if (date != DateTime.MinValue)
            {
                schedGames = schedGames.Where(g => g.GameDate.Date == date.Date)
                .OrderBy(g => g.GameDate).ThenBy(g => g.GameTime).ToList<ScheduleGamesVM>();
            }
            if (divisionId != 0)
            {
                schedGames = schedGames.Where(g => g.DivisionId == divisionId)
                .OrderBy(g => g.GameDate).ThenBy(g => g.GameTime).ThenBy(g => g.DivisionId).ToList<ScheduleGamesVM>();
            }
            return schedGames;

        }

        public int GetScheduleNumber(string divisionName)
        {
            try
            {
                using (var db = new CSBCDbContext())
                {
                    var scheduleNo = db.Set<ScheduleDivision>().FirstOrDefault(s => s.ScheduleName.ToUpper() == divisionName.ToUpper()).ScheduleNumber;
                    return scheduleNo;
                }
            }
            catch (Exception e)
            {
                //MasterVM.MsgBox(Msg.e()
                return 0;
            }
        }

        public static int GetScheduleNumber(int divisionId)
        {
            try
            {
                using (var db = new CSBCDbContext())
                {
                    var scheduleNo = db.Set<ScheduleGame>().FirstOrDefault(s => s.DivisionId == divisionId).ScheduleNumber;
                    return scheduleNo;
                }
            }
            catch (Exception e)
            {
                //MasterVM.MsgBox(Msg.e()
                return 0;
            }
        }
        public ScheduleGamesVM GetGame(int divisionId, int gameNo, int seasonId)
        {
            using (var db = new CSBCDbContext())
            {
                var games = (
                    from g in db.ScheduleGames
                    from d in db.Divisions
                    from l in db.ScheduleLocations
                    where (g.DivisionId == divisionId && g.GameNumber == gameNo)
                    where (d.DivisionID == divisionId)
                    where (g.LocationNumber == l.LocationNumber)
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
                        HomeTeamNo = db.Set<ScheduleDivTeam>().FirstOrDefault(t => t.TeamNumber == g.HomeTeamNumber).ScheduleTeamNumber,
                        VisitorTeamNo = db.Set<ScheduleDivTeam>().FirstOrDefault(t => t.TeamNumber == g.VisitingTeamNumber).ScheduleTeamNumber
                    });
                var schedGames = new List<ScheduleGamesVM>();
                var teams = db.Set<Team>().Where(t => t.DivisionID == divisionId).ToList<Team>();
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
                        HomeTeamNo = g.HomeTeamNo,
                        VisitorTeamNo = g.VisitorTeamNo
                    };
                    game.HomeTeam = GetTeamName(teams, g.HomeTeamNo);
                    game.VisitorTeam = GetTeamName(teams, g.VisitorTeamNo);
                    game.GameType = GameTypes.Regular;
                    schedGames.Add(game);
                }
                //var game = db.ScheduleGames.FirstOrDefault(s => s.ScheduleNumber == scheduleNo && s.GameNumber == gameNo);
                var sgame = schedGames.FirstOrDefault<ScheduleGamesVM>();
                return sgame;

            }
        }
        public ScheduleGame GetByScheduleAndGameNo(int scheduleNo, int gameNo)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new ScheduleGameRepository(db);
                var game = rep.GetByScheduleAndGameNo(scheduleNo, gameNo);
                return game;
            }
        }
        public SchedulePlayoff GetPlayoffByScheduleAndGameNo(int scheduleNo, int gameNo)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new SchedulePlayoffRepository(db);
                var game = rep.GetByScheduleAndGameNo(scheduleNo, gameNo);
                return game;
            }
        }

        internal ScheduleGamesVM GetPlayoffGame(int divisionId, int gameNo, int seasonId)
        {
            using (var db = new CSBCDbContext())
            {
                var games = (
                    from g in db.SchedulePlayoffs
                    from l in db.ScheduleLocations
                    from d in db.Divisions
                    where g.DivisionId == divisionId && g.GameNumber == gameNo
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
            string homeTeam, string visitorTeam, string description, int divisionId)
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
                    Descr = description,
                    DivisionId = divisionId
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
        private static string GetTeamName(List<Team> teams, int teamNo)
        {
            try
            {
                var team = teams.FirstOrDefault(t => Convert.ToInt32(t.TeamNumber) == teamNo);
                if (String.IsNullOrEmpty(team.TeamName))
                {
                    return team.Color.ColorName + "(" + teamNo + ")";
                }
                else
                {
                    return team.TeamName;
                }
            }

            catch
            {
                return "";
            }
        }
    }
}