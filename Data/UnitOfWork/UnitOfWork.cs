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
        //private readonly TransactionScope _transactionScope;
        //private bool _transactionCompleted;

        public UnitOfWork(StecpointDbContext db)
        {
            _db = db;
            //_transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled, TransactionScopeOption.Required, new TransactionOptions{IsolationLevel = IsolationLevel.ReadCommitted});
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : Entity
        {
            if (_repositories.ContainsKey(typeof(TEntity)))
                return _repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
            IGenericRepository<TEntity> repository = new GenericRepository<TEntity>(_db);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public void RollbackChanges()
        {
            //_CompleteTransaction(false);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
            // _CompleteTransaction();
        }

        // private void _CompleteTransaction(bool commitChanges = true)
        // {
        // if(_transactionCompleted)
        // throw new InvalidOperationException("Current transaction is already completed");
        // if (commitChanges)
        // _transactionScope.Complete();
        // _transactionScope.Dispose();
        // _transactionCompleted = true;
        // }
    }
}
