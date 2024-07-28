using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementations;

public class BusinessHoursRepository : Repository<BusinessHours>, IBusinessHoursRepository
{
    public BusinessHoursRepository( TheaterDbContext context )
        : base( context )
    {
    }

    public List<BusinessHours> GetAll()
    {
        return Entities.ToList();
    }

    public List<BusinessHours> GetByTheaterId( int id )
    {
        return Entities.Include( bh => bh.Theater ).Where( bh => bh.TheaterId == id ).ToList();
    }

    public BusinessHours GetById( int id )
    {
        return Entities.FirstOrDefault( bh => bh.Id == id );
    }
}