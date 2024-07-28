using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

internal class PlayConfiguration : IEntityTypeConfiguration<Play>
{
    public void Configure( EntityTypeBuilder<Play> builder )
    {
        builder.ToTable( nameof( Play ) )
               .HasKey( p => p.Id );

        builder.Property( p => p.Name )
               .HasMaxLength( 100 )
               .IsRequired();

        builder.Property( p => p.Description )
               .HasMaxLength( 500 )
               .IsRequired();

        builder.Property( p => p.StartDate )
               .IsRequired();

        builder.Property( p => p.EndDate )
               .IsRequired();

        builder.Property( p => p.Price )
               .IsRequired();
    }
}