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
        // 2. Get subject by ID, including faculty details
        public async Task<Subject> GetSubjectById(int subjectId)
        {
            return await _context.Subjects
                .Include(s => s.Faculty) // Include faculty details
                .FirstOrDefaultAsync(s => s.Subject_id == subjectId);
        }

        // 3. Add a new subject to the database
        public async Task AddSubject(Subject subject)
        {
            if (subject == null)
                throw new ArgumentNullException(nameof(subject));

            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
        }
        // 4. Update details of an existing subject
        public async Task UpdateSubject(Subject subject)
        {
            if (subject == null)
                throw new ArgumentNullException(nameof(subject));

            _context.Subjects.Update(subject);
            await _context.SaveChangesAsync();
        }

        // 5. Delete a subject by ID
        public async Task DeleteSubject(int subjectId)
        {
            var subject = await _context.Subjects.FindAsync(subjectId);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Subject with ID {subjectId} not found.");
            }
        }
        // 6. Get subjects taught by a specific faculty using LINQ
        public async Task<List<Subject>> GetSubjectsTaughtByFaculty(int facultyId)
        {
            return await _context.Subjects
                .Where(s => s.F_id == facultyId)
                .ToListAsync();
        }

        // 7. Count the total number of subjects offered
        public async Task<int> CountSubjects()
        {
            return await _context.Subjects.CountAsync();
        }
    }
}
