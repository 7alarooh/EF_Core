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

        // UpdateFaculty: Update the details of an existing faculty member
        public async Task UpdateFaculty(Faculty faculty)
        {
            _context.Faculties.Update(faculty);
            await _context.SaveChangesAsync();
        } 
        
        // DeleteFaculty: Delete a faculty member by ID
        public async Task DeleteFaculty(int id)
        {
            var faculty = await _context.Faculties
                .Include(f => f.Faculty_Phones)  // Include related phone records
                .FirstOrDefaultAsync(f => f.Fid == id);

            if (faculty != null)
            {
                // Remove associated phone numbers first
                if (faculty.Faculty_Phones != null && faculty.Faculty_Phones.Any())
                {
                    await DeleteFacultyPhones(faculty.Faculty_Phones);
                }

                // Then remove the faculty
                _context.Faculties.Remove(faculty);
                await _context.SaveChangesAsync();
            }
        }
        // DeleteFacultyPhones: Delete all phone numbers associated with a faculty
        public async Task DeleteFacultyPhones(List<Faculty_Phone> facultyPhones)
        {
            _context.Faculty_Phone.RemoveRange(facultyPhones);  // Remove all related phone records
            await _context.SaveChangesAsync();
        }
        // GetFacultyByDepartment: List faculty members based on their department

        public async Task<List<Faculty>> GetFacultyByDepartment(int departmentId)
        {
            return await _context.Faculties
                .Where(f => f.Department_id == departmentId)
                .Include(f => f.Subjects)
                .Include(f => f.Courses)
                .ToListAsync();
        }
        // GetFacultyByMobileNumber: Search for faculty members by their mobile number
        public async Task<Faculty> GetFacultyByMobileNumber(string mobileNumber)
        {
            return await _context.Faculty_Phone
                .Where(fp => fp.Phone_no == mobileNumber)
                .Select(fp => fp.Faculty)
                .FirstOrDefaultAsync();
        }
        // CalculateAverageSalary: Use LINQ to calculate the average salary of faculty members
        public async Task<decimal> CalculateAverageSalary()
        {
            return await _context.Faculties.AverageAsync(f => f.Salary);
        }

    }
}
