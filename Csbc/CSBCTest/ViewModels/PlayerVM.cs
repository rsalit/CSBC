using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CSBC.Core.Models;
using CSBC.Core.Data;
using System.Data;
using CSBC.Core.Repositories;

namespace CSBC.Admin.Web.ViewModels
{
    public class PlayerVM
    {
        public CSBCDbContext Db { get; set; }

        public PlayerVM()
        {
            
        }

        public PlayerVM(CSBCDbContext db)
        {
            Db = db;
        }
        public static DataTable GetDraftList(int divisionId, int seasonId, int companyId)
        {
            var oPlayer = new CSBC.Components.Season.ClsPlayers();
            DataTable rsData = default(DataTable);
            try
            {
                rsData = oPlayer.GetDraftList(divisionId, seasonId, companyId);
            }
            catch (Exception ex)
            {
                string text = "LoadGrid::" + ex.Message;
            }
            return rsData;
        }

        public static List<SeasonPlayer> GetSeasonPlayers(int seasonId)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new PlayerRepository(db);
                var players = rep.GetSeasonPlayers(seasonId);
                return players.ToList();
            }

        }
        public static List<SeasonPlayer> GetDivisionPlayers(int divisionId)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new PlayerRepository(db);
                var players = rep.GetDivisionPlayers(divisionId);
                return players.ToList();
            }

        }
        private static SeasonPlayer ConvertDataTableToSeasonPlayer(DataRow row)
        {
            var player = new SeasonPlayer();
            player.PeopleID = Convert.ToInt32(row["PeopleID"]);
            player.PlayerID = Convert.ToInt32(row["PlayerID"]);
            player.DraftID = row["PlayerID"].ToString();
            //player.LastName = row["LastName"].ToString();
            //player.FirstName = row["FirstName"].ToString();
            player.Name = row["Name"].ToString();
            player.BirthDate = Convert.ToDateTime(row["BirthDate"].ToString());
            return player;
        }

        public static Player LastSeason(int peopleId)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new PlayerRepository(db);
                var player = rep.GetLastSeasonPlayed(peopleId);
                return player;
            }
        }

        
    }
}