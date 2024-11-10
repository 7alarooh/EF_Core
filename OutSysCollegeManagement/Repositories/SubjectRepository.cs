using Microsoft.EntityFrameworkCore;
using OutSysCollegeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutSysCollegeManagement.Repositories
{
    public class SubjectRepository
    {
        private readonly CollegeDbContext _context;

        public SubjectRepository(CollegeDbContext context)
        {
            _context = context;
        }

        // 1. Get all subjects, including faculty details
        public async Task<List<Subject>> GetAllSubjects()
        {
            return await _context.Subjects
                .Include(s => s.Faculty) // Include faculty details
                .ToListAsync();
        }
    }
}
