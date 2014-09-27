using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lojack.Models;

namespace Lojack.Interfaces
{
    public interface ILojackEntryRepository : IRepository<LojackEntry>
    {
        List<String> GetExistingComputraceIds();
    }
}
