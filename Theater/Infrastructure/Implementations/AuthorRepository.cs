using Domain.Models;
using Domain.Repositories;

namespace Infrastructure.Implementations;

public class AuthorRepository : Repository<Author>, IAuthorRepository
{
    public AuthorRepository( TheaterDbContext context )
        : base( context )
    {
    }

    public List<Author> GetAll()
    {
        return Entities.ToList();
    }

    public Author GetById( int id )
    {
        return Entities.FirstOrDefault( t => t.Id == id );
    }
}