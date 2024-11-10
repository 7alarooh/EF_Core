using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OutSysCollegeManagement.Models;

namespace OutSysCollegeManagement.Models
{
    public class ExamRepository
    {
        private readonly CollegeDbContext _context;

        public ExamRepository(CollegeDbContext context)
        {
            _context = context;
        }

        // Get all exams, including the department and students taking the exam
        public async Task<List<Exams>> GetAllExams()
        {
            return await _context.Exams
                .Include(e => e.Department)  // Department of the exam
                .Include(e => e.Students)    // Students taking the exam
                .ToListAsync();
        }

        // Get exam details by ID, including navigational properties
        public async Task<Exams> GetExamById(int examId)
        {
            return await _context.Exams
                .Include(e => e.Department)  // Department of the exam
                .Include(e => e.Students)    // Students taking the exam
                .FirstOrDefaultAsync(e => e.Exam_code == examId); // Filter by ID
        }

        // Add a new exam
        public async Task AddExam(Exams exam)
        {
            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();
        }

        // Update the details of an existing exam
        public async Task UpdateExam(Exams exam)
        {
            _context.Exams.Update(exam);
            await _context.SaveChangesAsync();
        }

        // Delete an exam by ID
        public async Task DeleteExam(int examId)
        {
            var exam = await _context.Exams.FindAsync(examId);
            if (exam != null)
            {
                _context.Exams.Remove(exam);
                await _context.SaveChangesAsync();
            }
        }

        // Get exams by a specific date or date range
        public async Task<List<Exams>> GetExamsByDate(DateTime startDate, DateTime endDate)
        {
            return await _context.Exams
                .Where(e => e.Date >= startDate && e.Date <= endDate)
                .Include(e => e.Department)  // Department of the exam
                .Include(e => e.Students)    // Students taking the exam
                .ToListAsync();
        }

        // List exams taken by a specific student
        public async Task<List<Exams>> GetExamsByStudent(int studentId)
        {
            return await _context.Exams
                .Where(e => e.Students.Any(s => s.SID == studentId))
                .Include(e => e.Department)  // Department of the exam
                .Include(e => e.Students)    // Students taking the exam
                .ToListAsync();
        }

        // Count the number of exams conducted by a specific department
        public async Task<int> CountExamsByDepartment(int departmentId)
        {
            return await _context.Exams
                .Where(e => e.Department_id == departmentId)
                .CountAsync();
        }
    }
}
