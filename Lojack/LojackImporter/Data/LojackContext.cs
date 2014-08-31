using Lojack.Models;
using Lojack.Models.Mapping;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Lojack.Data
{
    public partial class LojackContext : DbContext
    {
        static LojackContext()
        {
            Database.SetInitializer<LojackContext>(null);
        }

        public LojackContext()
            : base("Name=LojackContext")
        {
        }

        public DbSet<LojackEntry> LojackEntries { get; set; }

        public DbSet<LojackUpdateTypeLU> LojackUpdateTypeLUs { get; set; }

        public DbSet<MatchedLojack> MatchedLojacks { get; set; }

        public DbSet<MatchedLojackHitUpdate> MatchedLojackHitUpdates { get; set; }

        public DbSet<LojackAuditProcess> LojackAuditProcesses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new LojackEntryMap());
            modelBuilder.Configurations.Add(new LojackUpdateTypeLUMap());
            modelBuilder.Configurations.Add(new MatchedLojackMap());
            modelBuilder.Configurations.Add(new MatchedLojackHitUpdateMap());
            modelBuilder.Entity<LojackAuditProcess>().ToTable("LojackAuditProcess");
        }
    }
}