using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class TheaterDbContext : DbContext
{
    public TheaterDbContext( DbContextOptions<TheaterDbContext> options )
        : base( options )
    {
    }

    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
    {
        if ( !optionsBuilder.IsConfigured )
        {
            optionsBuilder.UseSqlServer( "Server=localhost\\SQLEXPRESS;Database=Theater;Trusted_Connection=True;TrustServerCertificate=True;" );
        }
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        base.OnModelCreating( modelBuilder );

        modelBuilder.ApplyConfiguration( new AuthorConfiguration() );
        modelBuilder.ApplyConfiguration( new CompositionConfiguration() );
        modelBuilder.ApplyConfiguration( new BusinessHoursConfiguration() );
        modelBuilder.ApplyConfiguration( new PlayConfiguration() );
        modelBuilder.ApplyConfiguration( new TheaterConfiguration() );
    }
}