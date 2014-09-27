using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Core.Data;

namespace CSBC.Admin.Web.ViewModels
{
    public class SponsorProfileVM
    {

        public static void AddSponsorToSeason(int companyId, int seasonId, int sponsorProfileId)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new SponsorRepository(db);
                if (!(rep.IsSeasonSponsor(seasonId, sponsorProfileId)))
                {
                    var sponsor = rep.Insert(
                        new Sponsor
                        {
                            SponsorProfileID = sponsorProfileId,
                            SeasonID = seasonId,
                            CompanyID = companyId
                        }
                        );
                }
            }
        }
    }
}