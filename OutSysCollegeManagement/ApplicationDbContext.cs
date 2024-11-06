using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutSysCollegeManagement
{
    public class ApplicationDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(" Data Source=Localhost; Initial Catalog=OutSysCollegeManagement; Integrated Security=true; TrustServerCertificate=True ");
        }
        //public DbSet<Employee> Employees { get; set; }
    }
}
