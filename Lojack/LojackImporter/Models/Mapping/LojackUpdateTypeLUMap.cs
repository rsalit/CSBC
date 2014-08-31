using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Lojack.Models.Mapping
{
    public class LojackUpdateTypeLUMap : EntityTypeConfiguration<LojackUpdateTypeLU>
    {
        public LojackUpdateTypeLUMap()
        {
            // Primary Key
            this.HasKey(t => t.TypeID);

            // Properties
            this.Property(t => t.TypeDescription)
                .HasMaxLength(40);

            // Table & Column Mappings
            this.ToTable("LojackUpdateTypeLU");
            this.Property(t => t.TypeID).HasColumnName("TypeID");
            this.Property(t => t.TypeDescription).HasColumnName("TypeDescription");
        }
    }
}
