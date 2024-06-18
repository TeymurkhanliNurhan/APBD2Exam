using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test2.Models;

namespace Test2.Configurations
{
    public class PublishingHouseConfiguration : IEntityTypeConfiguration<PublishingHouse>
    {
        public void Configure(EntityTypeBuilder<PublishingHouse> builder)
        {
            builder.HasKey(p => p.IdPublishingHouse);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Country)
                .HasMaxLength(50);

            builder.Property(p => p.City)
                .HasMaxLength(50);

            builder.HasMany(p => p.Books)
                .WithOne(b => b.PublishingHouse)
                .HasForeignKey(b => b.IdPublishingHouse);
        }
    }
}