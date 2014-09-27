using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Lojack.Models.Mapping
{
    public class MatchedLojackMap : EntityTypeConfiguration<MatchedLojack>
    {
        public MatchedLojackMap()
        {
            // Primary Key
            this.HasKey(t => t.HitGUID);

            // Properties
            this.Property(t => t.matchtype)
                .HasMaxLength(3);

            this.Property(t => t.dateendregistration)
                .HasMaxLength(10);

            this.Property(t => t.computracefile)
                .HasMaxLength(15);

            this.Property(t => t.matchedserial)
                .HasMaxLength(80);

            this.Property(t => t.propertydescription)
                .HasMaxLength(250);

            this.Property(t => t.custfirstname)
                .HasMaxLength(50);

            this.Property(t => t.custcity)
                .HasMaxLength(50);

            this.Property(t => t.custstate)
                .HasMaxLength(20);

            this.Property(t => t.custlastname)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MatchedLojack");
            this.Property(t => t.HitGUID).HasColumnName("HitGUID");
            this.Property(t => t.TransactionGuid).HasColumnName("TransactionGuid");
            this.Property(t => t.JurisdictionGuid).HasColumnName("JurisdictionGuid");
            this.Property(t => t.HitCreationDate).HasColumnName("HitCreationDate");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.StatusChangeDate).HasColumnName("StatusChangeDate");
            this.Property(t => t.matchtype).HasColumnName("matchtype");
            this.Property(t => t.dateendregistration).HasColumnName("dateendregistration");
            this.Property(t => t.entryCreationDate).HasColumnName("entryCreationDate");
            this.Property(t => t.computracefile).HasColumnName("computracefile");
            this.Property(t => t.resolved).HasColumnName("resolved");
            this.Property(t => t.resolutionDate).HasColumnName("resolutionDate");
            this.Property(t => t.investigatedby).HasColumnName("investigatedby");
            this.Property(t => t.discarded).HasColumnName("discarded");
            this.Property(t => t.matchedserial).HasColumnName("matchedserial");
            this.Property(t => t.PropertyGuid).HasColumnName("PropertyGuid");
            this.Property(t => t.propertydescription).HasColumnName("propertydescription");
            this.Property(t => t.transactioneffectivedate).HasColumnName("transactioneffectivedate");
            this.Property(t => t.custfirstname).HasColumnName("custfirstname");
            this.Property(t => t.custdob).HasColumnName("custdob");
            this.Property(t => t.custcity).HasColumnName("custcity");
            this.Property(t => t.custstate).HasColumnName("custstate");
            this.Property(t => t.custlastname).HasColumnName("custlastname");
        }
    }
}
