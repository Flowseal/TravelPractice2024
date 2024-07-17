using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

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

    public bool Update( int id, Theater theater )
    {
        Theater existingTheater = GetById( id );
        if ( existingTheater is null )
        {
            return false;
        }

        existingTheater.Update( theater );
        return true;
    }
}