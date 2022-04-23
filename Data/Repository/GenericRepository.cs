using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
    {
        private readonly DbSet<TEntity> _entities;
        private readonly DbContext _db;

        public GenericRepository(DbContext db)
        {
            _db = db;
            _entities = _db.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _db.Set<TEntity>().AsNoTracking();
        }

        public TEntity Get(long id)
        {
            return _entities.Find(id);
        }

        public async Task<TEntity> GetAsync(long id)
        {
            return await _entities.FindAsync(id);
        }

        public void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _entities.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
