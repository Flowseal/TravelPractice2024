using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

internal class CompositionConfiguration : IEntityTypeConfiguration<Composition>
{
    public void Configure( EntityTypeBuilder<Composition> builder )
    {
        builder.ToTable( nameof( Composition ) )
               .HasKey( c => c.Id );

        builder.Property( c => c.Name )
               .HasMaxLength( 100 )
               .IsRequired();

        builder.Property( c => c.Description )
               .HasMaxLength( 500 )
               .IsRequired();

        builder.Property( c => c.CharactersInfo )
               .HasMaxLength( 250 )
               .IsRequired();

        builder.HasMany( c => c.Plays )
               .WithOne( p => p.Composition )
               .HasForeignKey( p => p.CompositionId );
    }
}