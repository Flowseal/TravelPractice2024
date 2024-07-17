using Domain.Models;

namespace Domain.Repositories;

public interface ICompositionRepository : IRepository<Composition>
{
    public List<Composition> GetAll();
    public Composition GetById( int id );
}