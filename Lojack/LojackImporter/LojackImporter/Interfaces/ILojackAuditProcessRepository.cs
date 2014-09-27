using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lojack.Models;

namespace Lojack.Interfaces
{
    public interface ILojackAuditProcessRepository
    {
        IQueryable<LojackAuditProcess> GetByDateRange(DateTime startDate, DateTime endDate);
    }
}
