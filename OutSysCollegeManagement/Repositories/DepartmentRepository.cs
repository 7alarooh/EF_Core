using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OutSysCollegeManagement.Models; 

namespace OutSysCollegeManagement.Repositories
{
    public class DepartmentRepository
    {
        private readonly CollegeDbContext _context;

        public DepartmentRepository(CollegeDbContext context)
        {
            _context = context;
        }

        // 1. Get all departments, including the courses they handle and exams conducted
        public async Task<List<Department>> GetAllDepartments()
        {
            return await _context.Departments
                .Include(d => d.Courses)  // Courses handled by the department
                .Include(d => d.Exams)    // Exams conducted by the department
                .ToListAsync();
        }

        // 2. Get department details by ID, including related courses and exams
        public async Task<Department> GetDepartmentById(int departmentId)
        {
            return await _context.Departments
                .Include(d => d.Courses)  // Courses handled by the department
                .Include(d => d.Exams)    // Exams conducted by the department
                .FirstOrDefaultAsync(d => d.Department_id == departmentId);
        }

        // 3. Add a new department
        public async Task AddDepartment(Department department)
        {
            if (department == null)
                throw new ArgumentNullException(nameof(department));

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
        }

        // 4. Update details of an existing department
        public async Task UpdateDepartment(Department department)
        {
            if (department == null)
                throw new ArgumentNullException(nameof(department));

            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        // 5. Delete a department by ID
        public async Task DeleteDepartment(int departmentId)
        {
            var department = await _context.Departments.FindAsync(departmentId);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Department with ID {departmentId} not found.");
            }
        }

        // 6. Get departments that offer courses (using LINQ Join or Include)
        public async Task<List<Department>> GetDepartmentsWithCourses()
        {
            return await _context.Departments
                .Where(d => d.Courses.Any())  // Filter departments that have courses
                .Include(d => d.Courses)      // Include related courses
                .ToListAsync();
        }

        // 7. Retrieve just the names of all departments (using projection in LINQ)
        public async Task<List<string>> GetDepartmentNames()
        {
            return await _context.Departments
                .Select(d => d.D_name)  // Project only department names
                .ToListAsync();
        }
    }
}
