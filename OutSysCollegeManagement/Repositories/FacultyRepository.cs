using Microsoft.EntityFrameworkCore;
using OutSysCollegeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutSysCollegeManagement.Repositories
{
    public class FacultyRepository
    {
        private readonly CollegeDbContext _context;

        public FacultyRepository(CollegeDbContext context)
        {
            _context = context;
        }

        // GetAllFaculties: List all faculty members, including associated subjects and courses
        public async Task<List<Faculty>> GetAllFaculties()
        {
            return await _context.Faculties
        .Include(f => f.Subjects) // Now this will work
        .Include(f => f.Courses)
        .ToListAsync();
        }
        // GetFacultyById: Fetch a faculty member's complete details by ID
        public async Task<Faculty> GetFacultyById(int id)
        {
            return await _context.Faculties
                .Include(f => f.Subjects)
                .Include(f => f.Courses)
                .FirstOrDefaultAsync(f => f.Fid == id);
        }  
        // AddFaculty: Add a new faculty member
        public async Task AddFaculty(Faculty faculty)
        {
            _context.Faculties.Add(faculty);
            await _context.SaveChangesAsync();
        }
    }
}
