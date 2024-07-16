using Domain.Models;
using Domain.Repositories;

namespace Infrastructure.Implementations;

public class TheaterRepository : Repository<Theater>, ITheaterRepository
{
    public TheaterRepository( TheaterDbContext context )
        : base( context )
    {
    }

    public List<Theater> GetAll()
    {
        return Entities.ToList();
    }

    public Theater GetById( int id )
    {
        return Entities.FirstOrDefault( t => t.Id == id );
    }
}