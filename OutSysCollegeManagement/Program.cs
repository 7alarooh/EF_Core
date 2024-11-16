using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OutSysCollegeManagement.Models;
using OutSysCollegeManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutSysCollegeManagement
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            using var dbContext = new CollegeDbContext();
            var courseRepository =new CourseRepository(dbContext);
            try {
                // Call the CourseMenu method
                await CourseMenu(courseRepository);
                bool exit = false;
                while (!exit)
                {
                    Console.Clear();
                    Console.WriteLine("===== Main Menu =====");
                    Console.WriteLine("1. Faculty Repository");
                    Console.WriteLine("2. Department Repository");
                    Console.WriteLine("3. Exam Repository");
                    Console.WriteLine("4. Course Repository");
                    Console.WriteLine("5. Subject Repository");
                    Console.WriteLine("6. Hostel Repository");
                    Console.WriteLine("7. Exit");
                    Console.Write("Please select an option: ");
                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            //await FacultyMenu();
                            break;
                        case "2":
                            //await DepartmentMenu();
                            break;
                        case "3":
                            //await ExamMenu();
                            break;
                        case "4":
                            await CourseMenu(courseRepository);
                            break;
                        case "5":
                            //await SubjectMenu();
                            break;
                        case "6":
                            //await HostelMenu();
                            break;
                        case "7":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public static async Task CourseMenu(CourseRepository courseRepository)
        {
            try
            {
                bool exit = false;
                while (!exit)
                {
                    Console.Clear();
                    Console.WriteLine("===== Course Repository =====");
                    Console.WriteLine("1. Get All Courses");
                    Console.WriteLine("2. Get Course By ID");
                    Console.WriteLine("3. Add Course");
                    Console.WriteLine("4. Update Course");
                    Console.WriteLine("5. Delete Course");
                    Console.WriteLine("6. Get Courses By Department");
                    Console.WriteLine("7. Get Courses By Duration");
                    Console.WriteLine("8. Paginate Courses");
                    Console.WriteLine("9. Return to Main Menu");
                    Console.Write("Select an option: ");
                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            var courses = await GetAllCourses(courseRepository);
                            break;
                        case "2":
                            await GetCourseById(courseRepository);
                            break;
                        case "3":
                            await AddCourse(courseRepository);
                            break;
                        case "4":
                            //await UpdateCourse();
                            break;
                        case "5":
                            //await DeleteCourse();
                            break;
                        case "6":
                            //await GetCoursesByDepartment();
                            break;
                        case "7":
                            //await GetCoursesByDuration();
                            break;
                        case "8":
                            //await PaginateCourses();
                            break;
                        case "9":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }

                    if (!exit)
                    {
                        Console.WriteLine("\nPress any key to return to the Course Menu...");
                        Console.ReadKey();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in the Course Repository: {ex.Message}");
            }
        }
        public static async Task<List<Course>> GetAllCourses(CourseRepository courseRepository)
        {
            try
            {
                var courses = await courseRepository.GetAllCoursesAsync();

                if (courses == null || !courses.Any())
                {
                    Console.WriteLine("No courses found.");
                    return new List<Course>(); // Return an empty list if no courses are found
                }

                // Display the courses (formatted for a user-friendly view)
                Console.WriteLine("\nCourses Available:");
                Console.WriteLine("ID\tName\tDuration\tDepartment");
                foreach (var course in courses)
                {
                    Console.WriteLine($"{course.Course_id}\t{course.Course_name}\t{course.Duration}\t{course.Department?.D_name}");
                }

                return courses; // Return the list of courses
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving courses: {ex.Message}");
                return new List<Course>(); // Return an empty list in case of an exception
            }
        }
        public static async Task GetCourseById(CourseRepository courseRepository)
        {
            try
            {
                Console.Write("Enter the Course ID: ");
                if (int.TryParse(Console.ReadLine(), out int courseId))
                {
                    var course = await courseRepository.GetCourseById(courseId);
                    if (course != null)
                    {
                        Console.WriteLine($"ID: {course.Course_id}, Name: {course.Course_name}, Duration: {course.Duration}");
                    }
                    else
                    {
                        Console.WriteLine("Course not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid Course ID.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching course: {ex.Message}");
            }
        }
        private static async Task AddCourse(CourseRepository courseRepository)
        {
            try
            {
                Console.Write("Enter Course Name: ");
                var name = Console.ReadLine();
                Console.Write("Enter Course Duration (in months): ");
                if (int.TryParse(Console.ReadLine(), out int duration))
                {
                    var newCourse = new Course { Course_name = name, Duration = duration };
                    await courseRepository.AddCourse(newCourse);
                    Console.WriteLine("Course added successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid duration. Please enter a valid number.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding course: {ex.Message}");
            }
        }



    }
}
