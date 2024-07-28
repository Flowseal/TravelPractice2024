using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementations;

public class CompositionRepository : Repository<Composition>, ICompositionRepository
{
    public CompositionRepository( TheaterDbContext context )
        : base( context )
    {
    }

    public List<Composition> GetAll()
    {
        return Entities.ToList();
    }

    public Composition GetById( int id )
    {
        return Entities.FirstOrDefault( t => t.Id == id );
    }
}