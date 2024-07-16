using Domain.Models;

namespace Domain.Repositories;

public interface ITheaterRepository : IRepository<Theater>
{
    public List<Theater> GetAll();
    public Theater GetById( int id );
}