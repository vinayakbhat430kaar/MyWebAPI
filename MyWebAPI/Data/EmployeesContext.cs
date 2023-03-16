using Microsoft.EntityFrameworkCore;
using MyWebAPI.Models;

namespace MyWebAPI.Data
{
    public class EmployeesContext:DbContext
    {
        public EmployeesContext(DbContextOptions<EmployeesContext> options) : base(options) { }

        public DbSet<Books> Books { get; set; }
        public DbSet<EmployeesModel> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeesModel>().HasKey(e => e.emp_id);
        }

    }
}
