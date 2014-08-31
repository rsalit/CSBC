using Lojack.Models;
using Lojack.Models.Mapping;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Lojack.Data
{
    public partial class CentralContext : DbContext
    {
        static CentralContext()
        {
            Database.SetInitializer<LojackContext>(null);
        }

        public CentralContext()
            : base("Name=CentralContext")
        {
        }
    }
}