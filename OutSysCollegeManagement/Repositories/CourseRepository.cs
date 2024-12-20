﻿using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses
                .Include(c => c.Students) // Enrolled students
                .Include(c => c.Faculty)  // Faculty handling the course
                .ToListAsync();
        }
        // 2. Get course details by ID, with related students and faculty details
        public async Task<Course> GetCourseById(int courseId)
        {
            return await _context.Courses
                .Include(c => c.Students) // Enrolled students
                .Include(c => c.Faculty)  // Faculty handling the course
                .FirstOrDefaultAsync(c => c.Course_id == courseId);
        }
        // 3. Add a new course to the database
        public async Task AddCourse(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }
        // 4. Update existing course details
        public async Task UpdateCourse(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }
        // 5. Delete a course by ID
        public async Task DeleteCourse(int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Course with ID {courseId} not found.");
            }
        }
        // 6. Get courses offered by a specific department
        public async Task<List<Course>> GetCoursesByDepartment(int departmentId)
        {
            return await _context.Courses
                .Where(c => c.Department_id == departmentId)
                .ToListAsync();
        }

        // 7. Filter courses by duration using LINQ
        public async Task<List<Course>> GetCoursesWithDuration(int duration)
        {

            return await _context.Courses
                .Where(c => c.Duration == duration)
                .ToListAsync();
        }
        // 8. Paginate courses (for large datasets)
        public async Task<List<Course>> PaginateCourses(int pageNumber, int pageSize)
        {
            return await _context.Courses
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
