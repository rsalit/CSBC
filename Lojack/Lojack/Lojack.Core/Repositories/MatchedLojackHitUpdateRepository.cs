using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Lojack.Data;
using Lojack.Models;
using Lojack.Interfaces;
using RPDSS.Data;

namespace Lojack.Repositories
{
    public class MatchedLojackHitUpdateRepository : EFRepository<MatchedLojackHitUpdate>, IMatchedLojackHitUpdate
    {
          public MatchedLojackHitUpdateRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");
            DataContext = dbContext;
            DbSet = DataContext.Set<MatchedLojackHitUpdate>();
        }
    }
}
