using System.Reflection;
using dewi.Models;
using Microsoft.EntityFrameworkCore;

namespace dewi.Contexts
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Position> Positions { get; set; }

    }
}
