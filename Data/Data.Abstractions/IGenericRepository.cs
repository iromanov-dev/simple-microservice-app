using System.Linq;
using System.Threading.Tasks;

namespace Data.Abstractions
{
    public interface IGenericRepository<TEntity> where TEntity : Entity
    {
        IQueryable<TEntity> GetAll();
        TEntity Get(long id);
        Task<TEntity> GetAsync(long id);
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void SaveChanges();
    }
}
