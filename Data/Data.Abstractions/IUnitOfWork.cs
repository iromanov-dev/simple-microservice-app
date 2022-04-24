namespace Data.Abstractions
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : Entity;
        void SaveChanges();
    }
}