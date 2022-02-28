using Microsoft.EntityFrameworkCore;

namespace TaskDay8.Models
{
    public class CompanyModel:DbContext
    {
        public CompanyModel(DbContextOptions options):base(options)
        {

        }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Department> Departments { get; set; }


    }
}
