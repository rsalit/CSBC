using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Lojack.Models.Mapping
{
    public class LojackEntryMap : EntityTypeConfiguration<LojackEntry>
    {
        public LojackEntryMap()
        {
            // Primary Key
            this.HasKey(t => t.lojackentryid);

            // Properties
            this.Property(t => t.computracefile)
                .HasMaxLength(15);

            this.Property(t => t.ReportedSerialNumber)
                .HasMaxLength(80);

            this.Property(t => t.TheftIncidentNumber)
                .HasMaxLength(100);

            this.Property(t => t.AgencyName)
                .HasMaxLength(50);

            this.Property(t => t.ClosedDate)
                .HasMaxLength(100);

            this.Property(t => t.Make)
                .HasMaxLength(30);

            this.Property(t => t.Model)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("LojackEntries");
            this.Property(t => t.lojackentryid).HasColumnName("lojackentryid");
            this.Property(t => t.computracefile).HasColumnName("computracefile");
            this.Property(t => t.ReportedSerialNumber).HasColumnName("ReportedSerialNumber");
            this.Property(t => t.StolenDate).HasColumnName("StolenDate");
            this.Property(t => t.DateReportedToABT).HasColumnName("DateReportedToABT");
            this.Property(t => t.TheftIncidentNumber).HasColumnName("TheftIncidentNumber");
            this.Property(t => t.AgencyName).HasColumnName("AgencyName");
            this.Property(t => t.ClosedDate).HasColumnName("ClosedDate");
            this.Property(t => t.Make).HasColumnName("Make");
            this.Property(t => t.Model).HasColumnName("Model");
        }
    }
}
