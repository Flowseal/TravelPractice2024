using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementations;

public class PlayRepository : Repository<Play>, IPlayRepository
{
    public PlayRepository( TheaterDbContext context )
        : base( context )
    {
    }

    public List<Play> GetAll()
    {
        return Entities.ToList();
    }

    public Play GetById( int id )
    {
        return Entities.FirstOrDefault( t => t.Id == id );
    }

    public List<Play> GetPlaysInDatesRange( DateTime start, DateTime end )
    {
        return Entities.Where( p => p.StartDate >= start && p.EndDate <= end ).Include( p => p.Composition ).ToList();
    }
}