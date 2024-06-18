using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test2.Models;

namespace Test2.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.IdBook);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.ReleaseDate)
                .IsRequired();

            builder.HasOne(b => b.PublishingHouse)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.IdPublishingHouse);

            builder.HasMany(b => b.Authors)
                .WithMany(a => a.Books);

            builder.HasMany(b => b.Genres)
                .WithMany(g => g.Books);
        }
    }
}