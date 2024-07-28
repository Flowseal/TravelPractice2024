using Domain.Models;

namespace Domain.Repositories;

public interface IPlayRepository : IRepository<Play>
{
    public List<Play> GetAll();
    public Play GetById( int id );
    public List<Play> GetPlaysInDatesRange( DateTime start, DateTime end );
}