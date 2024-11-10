using Microsoft.EntityFrameworkCore;
using OutSysCollegeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutSysCollegeManagement.Repositories
{
    public class CourseRepository
    {
        private readonly CollegeDbContext _context;

        public CourseRepository(CollegeDbContext context)
        {
            _context = context;
        }

        // 1. Get all courses, including students enrolled and faculty details
        public async Task<List<Course>> GetAllCourses()
        {
            return await _context.Courses
                .Include(c => c.Students) // Enrolled students
                .Include(c => c.Faculty)  // Faculty handling the course
                .ToListAsync();
        }
    }
}
