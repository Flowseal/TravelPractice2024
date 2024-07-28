using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

internal class BusinessHoursConfiguration : IEntityTypeConfiguration<BusinessHours>
{
    public void Configure( EntityTypeBuilder<BusinessHours> builder )
    {
        builder.ToTable( nameof( BusinessHours ) )
               .HasKey( bh => bh.Id );

        builder.Property( bh => bh.Day )
               .IsRequired();

        builder.Property( bh => bh.OpenTime )
               .IsRequired();

        builder.Property( bh => bh.CloseTime )
               .IsRequired();

        builder.Property( bh => bh.ValidFrom );

        builder.Property( bh => bh.ValidThrough );
    }
}