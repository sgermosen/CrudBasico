using EmployeeSystem.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSystem.Web
{
    public class EmployeeDataContext: DbContext
    {
        public EmployeeDataContext(DbContextOptions<EmployeeDataContext> options): base (options)   
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Sex> Sexs { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
    }
}
