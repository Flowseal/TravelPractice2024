using Domain.Models;

namespace Domain.Repositories;

public interface IBusinessHoursRepository : IRepository<BusinessHours>
{
    public List<BusinessHours> GetAll();
    public List<BusinessHours> GetByTheaterId( int id );
    public BusinessHours GetById( int id );
}