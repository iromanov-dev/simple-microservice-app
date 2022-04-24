using Data.Abstractions;
using Data.EF.Context;
using Data.EF.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Core.Organizations.Test
{
    public class DbTestsBase
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly StecpointDbContext context;
        public DbTestsBase()
        {
            var options = new DbContextOptionsBuilder<StecpointDbContext>().UseInMemoryDatabase(databaseName: "DBTest").Options;
            context = new StecpointDbContext(options);
            unitOfWork = new UnitOfWork(context);

            context.Database.EnsureDeleted();
            context.SaveChanges();
        }
    }
}
