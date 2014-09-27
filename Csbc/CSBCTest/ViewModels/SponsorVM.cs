using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CSBC.Core.Models;
using CSBC.Core.Data;

namespace CSBC.Admin.Web.ViewModels
{
    public class SponsorVM
    {
        public int SponsorId { get; set; }
        public string SponsorName { get; set; }

        public List<SponsorVM> GetSeasonSponsors(int seasonId)
        {
            using (var db = new CSBCDbContext())
            {
                var sponsors = db.Sponsors.Where(s => s.SeasonID == seasonId)
                                    .OrderBy(p => p.SponsorProfile.SpoName)
                                    .ToList();

                var vm = new List<SponsorVM>();
                foreach (Sponsor s in sponsors)
                {
                    vm.Add(new SponsorVM { SponsorId = s.SponsorID, SponsorName = s.SponsorProfile.SpoName });
                }

                return vm;
            }
        }
    }
}