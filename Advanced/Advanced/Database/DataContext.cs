using Advanced.Models;
using Microsoft.EntityFrameworkCore;

namespace Advanced.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opts)    : base(opts) { }

        public DbSet<Person> People => Set<Person>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Location> Locations => Set<Location>();
    }
}