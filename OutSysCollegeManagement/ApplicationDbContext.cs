using Microsoft.EntityFrameworkCore;
using OutSysCollegeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutSysCollegeManagement
{
    public class CollegeDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(" Data Source=Localhost; Initial Catalog=OutSysCollegeManagement; Integrated Security=true; TrustServerCertificate=True ");
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<student_Phone> Student_Phones { get; set; }
        public DbSet<Course>Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Exams>Exams { get; set; }
        public DbSet<Faculty_Phone> Faculty_Phones { get; set; }
        public DbSet<Hostel> Hostels { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Faculty> Faculties { get; set; }

    }
}
