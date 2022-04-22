using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class StecpointDbContext : DbContext
    {
        public StecpointDbContext(DbContextOptions<StecpointDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(x => x.Organization).WithMany(x => x.Users).HasForeignKey(x => x.OrganizationId);

            modelBuilder.Entity<Organization>().HasData(
                new Organization
                {
                    Id = 1,
                    Name = "Организация 1"
                },
                new Organization
                {
                    Id = 2,
                    Name = "Организация 2"
                },
                new Organization
                {
                    Id = 3,
                    Name = "Организация 3"
                }
            );
        }
    }
}
