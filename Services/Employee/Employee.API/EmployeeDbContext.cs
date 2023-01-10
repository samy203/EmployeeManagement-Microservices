using Microsoft.EntityFrameworkCore;

namespace Employee.API
{
    public class EmployeeDbContext : DbContext
    {
        public DbSet<API.Models.Employee> Employees { get; set; }

        public EmployeeDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
