using Domain.Models;

namespace Domain.Repositories;

public interface IAuthorRepository : IRepository<Author>
{
    public List<Author> GetAll();
    public Author GetById( int id );
}