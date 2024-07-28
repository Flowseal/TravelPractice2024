namespace Domain.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    public void Add( TEntity entity );
    public void Add( IEnumerable<TEntity> entities );

    public void Remove( TEntity entity );
    public void Remove( IEnumerable<TEntity> entities );
}
