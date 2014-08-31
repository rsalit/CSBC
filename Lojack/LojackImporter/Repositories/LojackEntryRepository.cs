using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Lojack.Interfaces;
using Lojack.Models;
using Lojack.Data;
using RPDSS.Data;
using MatchedLojack = RPDSS.Lojack.MatchedLojack;

namespace Lojack.Repositories
{
    public class LojackEntryRepository : EFRepository<LojackEntry>, ILojackEntryRepository
    {
        public LojackEntryRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");
            DataContext = dbContext;
            DbSet = DataContext.Set<LojackEntry>();
        }

        public List<string> GetExistingComputraceIds()
        {
            return DbSet.Select(l => l.computracefile).Distinct().ToList();
        }
    }
}
