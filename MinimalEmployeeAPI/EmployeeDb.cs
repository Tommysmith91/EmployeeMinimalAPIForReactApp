
using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI
{
    public class EmployeeDb : DbContext
    {        
        public EmployeeDb(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; } = null!;     


    }
}
