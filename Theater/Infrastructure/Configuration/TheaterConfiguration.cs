using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

internal class TheaterConfiguration : IEntityTypeConfiguration<Theater>
{
    public void Configure( EntityTypeBuilder<Theater> builder )
    {
        builder.ToTable( nameof( Theater ) )
               .HasKey( t => t.Id );

        builder.Property( t => t.Name )
               .HasMaxLength( 100 )
               .IsRequired();

        builder.Property( t => t.OpeningDate )
               .IsRequired();

        builder.Property( t => t.Address )
               .HasMaxLength( 100 )
               .IsRequired();

        builder.Property( t => t.Description )
              .HasMaxLength( 250 )
              .IsRequired();

        builder.Property( t => t.PhoneNumber )
               .HasMaxLength( 100 )
               .IsRequired();

        // TODO plays

        builder.HasMany( t => t.BusinessHours )
               .WithOne()
               .HasForeignKey( bh => bh.TheaterId );
    }
}