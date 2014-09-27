using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.Entity;
//using LojackImporter.Data;
using Lojack.Interfaces;
using Lojack.Models;

namespace Lojack.Repositories
{
    public class LojackAuditProcessRepository : EFRepository<LojackAuditProcess>, ILojackAuditProcessRepository
    {
        public LojackAuditProcessRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");
            DataContext = dbContext;
            DbSet = DataContext.Set<LojackAuditProcess>();
        }

        public bool FindByFileName(string fileName)
        {
            return File.Exists(fileName);
        }

        public IQueryable<LojackAuditProcess> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            var data =
                DataContext.Set<LojackAuditProcess>()
                    .Where(d => d.ProcessDateTime >= startDate && d.ProcessDateTime <= endDate);
            return data;
        }
    }
}
