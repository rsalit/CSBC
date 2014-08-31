using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Lojack.Models.Mapping
{
    public class MatchedLojackHitUpdateMap : EntityTypeConfiguration<MatchedLojackHitUpdate>
    {
        public MatchedLojackHitUpdateMap()
        {
            // Primary Key
            this.HasKey(t => t.UpdateGUID);

            // Properties
            this.Property(t => t.UpdateDescription)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("MatchedLojackHitUpdate");
            this.Property(t => t.UpdateGUID).HasColumnName("UpdateGUID");
            this.Property(t => t.UpdateDescription).HasColumnName("UpdateDescription");
            this.Property(t => t.UpdateTypeID).HasColumnName("UpdateTypeID");
            this.Property(t => t.CreationDate).HasColumnName("CreationDate");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.UserGUID).HasColumnName("UserGUID");
            this.Property(t => t.HitGUID).HasColumnName("HitGUID");
        }
    }
}
