using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

internal class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure( EntityTypeBuilder<Author> builder )
    {
        builder.ToTable( nameof( Author ) )
               .HasKey( a => a.Id );

        builder.Property( a => a.Name )
               .HasMaxLength( 100 )
               .IsRequired();

        builder.Property( a => a.BirthDate )
               .IsRequired();

        builder.HasMany( a => a.Compositions )
               .WithOne( c => c.Author )
               .HasForeignKey( c => c.AuthorId );
    }
}