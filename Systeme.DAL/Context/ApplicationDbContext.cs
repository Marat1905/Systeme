using Microsoft.EntityFrameworkCore;
using Systeme.DAL.Configurations;
using Systeme.Domain.Entityes;

namespace Systeme.DAL.Context
{
    public class ApplicationDbContext:DbContext
    {

        public DbSet<Car> Cars { get; set; }
        public DbSet<Driver> Drivers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CarConfiguration());
            modelBuilder.ApplyConfiguration(new DriverConfiguration());
            

            base.OnModelCreating(modelBuilder);
        }
    }
}
