using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SqlChangeTrackingPoc.Entity.Models.Mapping
{
    public class DevTestMap : EntityTypeConfiguration<DevTest>
    {
        public DevTestMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.CampaignName)
                .HasMaxLength(255);

            this.Property(t => t.AffiliateName)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("DevTest");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.CampaignName).HasColumnName("CampaignName");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Clicks).HasColumnName("Clicks");
            this.Property(t => t.Conversions).HasColumnName("Conversions");
            this.Property(t => t.Impressions).HasColumnName("Impressions");
            this.Property(t => t.AffiliateName).HasColumnName("AffiliateName");            
        }
    }
}
