using Microsoft.EntityFrameworkCore;
using OutSysCollegeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutSysCollegeManagement.Repositories
{
    public class StudentRepository
    {
        private readonly CollegeDbContext _context;

        public StudentRepository(CollegeDbContext context)
        {
            _context = context;
        }
        // GetAllStudents: Retrieve a list of all students with related courses, exams, and hostel information
        public async Task<List<Student>> GetAllStudents()
        {
            return await _context.Students
                .Include(s => s.Courses)
                .Include(s => s.Hostel)
                .Include(s => s.Faculty)
                .ToListAsync();
        }
        // GetStudentById: Retrieve a student by ID with related courses, exams, and hostel information
        public async Task<Student> GetStudentById(int id)
        {
            return await _context.Students
                .Include(s => s.Courses)
                .Include(s => s.Hostel)
                .Include(s => s.Faculty)
                .FirstOrDefaultAsync(s => s.SID == id);
        }
        // AddStudent: Add a new student to the database
        public async Task AddStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }
        // UpdateStudent: Update an existing student’s information
        public async Task UpdateStudent(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }
        // DeleteStudent: Remove a student by ID and ensure related data integrity
        public async Task DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

    }
}
