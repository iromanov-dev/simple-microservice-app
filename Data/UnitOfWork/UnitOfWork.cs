using Data.Context;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StecpointDbContext _db;

        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public UnitOfWork(StecpointDbContext db)
        {
            _db = db;
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : Entity
        {
            if (_repositories.ContainsKey(typeof(TEntity)))
                return _repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
            IGenericRepository<TEntity> repository = new GenericRepository<TEntity>(_db);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
