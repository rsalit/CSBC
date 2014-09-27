using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Web.SessionState;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Components;

namespace CSBC.Admin.Web.ViewModels
{
    public class SelectSeasonVM
    {
        public List<Season> Seasons { get; set; }
        [ScaffoldColumn(false)]
        public int SeasonID { get; set; }
        [MaxLength(40)]
        [Display(Name = "Season Description")]
        public string Description { get; set; }
        [Display(Name = "From Date")]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "From Date")]
        public DateTime ToDate { get; set; }
        [Display(Name = "Current")]
        [MaxLength(20)]
        public bool CurrentSeason { get; set; }
        [Display(Name = "Current Schedule")]
        public bool CurrentSchedule { get; set; }
        [Display(Name = "Cur Signups")]
        public bool CurrentSignUps { get; set; }
        /*[Display(Name = "Sign up Date")]
        public Nullable<System.DateTime> SignUpsDate { get; set; }
        [Display(Name = "Sign up End")]
        public Nullable<System.DateTime> SignUpsEND { get; set; }
        */
        public SelectSeasonVM()
        {
            //Seasons = seasons;
        }

        public List<SelectSeasonVM> GetRecords(int companyId)
        {
            var rep = new SeasonRepository(new Core.Data.CSBCDbContext());
            IQueryable<Season> seasons = rep.GetSeasons(companyId);
            var seasonsVm = new List<SelectSeasonVM>();
            foreach (Season season in seasons)
            {
                var vm = new SelectSeasonVM();
                vm.SeasonID = season.SeasonID;
                vm.Description = season.Description;
                vm.FromDate = (DateTime)season.FromDate;
                vm.ToDate = (DateTime)season.ToDate;
                vm.CurrentSeason = season.CurrentSeason == null ? false : (bool)season.CurrentSeason;
                vm.CurrentSchedule = season.CurrentSchedule == null ? false : (bool) season.CurrentSchedule;
                vm.CurrentSignUps = season.CurrentSignUps == null ? false : (bool)season.CurrentSignUps;
                seasonsVm.Add(vm);
            }
            return seasonsVm.OrderByDescending(s => s.FromDate).ToList();
        }
 
    }
}