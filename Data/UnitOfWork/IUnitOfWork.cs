using Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : Entity;
        void SaveChanges();
        void RollbackChanges();
    }
}
