using Application.Services;

namespace Infrastructure.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly TheaterDbContext _theaterDbContext;

    public UnitOfWork( TheaterDbContext theaterDbContext )
    {
        _theaterDbContext = theaterDbContext;
    }

    public void SaveChanges()
    {
        _theaterDbContext.SaveChanges();
    }
}
