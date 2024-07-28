namespace Application.Services;

public interface IUnitOfWork
{
    public void SaveChanges();
}