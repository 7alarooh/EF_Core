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
            var courseRepository = new CourseRepository(dbContext);
            var facultyRepository = new FacultyRepository(dbContext);
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
                            await FacultyMenu(facultyRepository);
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

        //                       CourseMenu
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
                            await UpdateCourse(courseRepository);
                            break;
                        case "5":
                            await DeleteCourse(courseRepository);
                            break;
                        case "6":
                            await GetCoursesByDepartment(courseRepository);
                            break;
                        case "7":
                            await GetCoursesByDuration(courseRepository);
                            break;
                        case "8":
                            await PaginateCourses(courseRepository);
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
        //1. Get All Courses
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
        //2. Get Course By ID
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
        //3. Add Course
        public static async Task AddCourse(CourseRepository courseRepository)
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
        //4. Update Course
        public static async Task UpdateCourse(CourseRepository courseRepository)
        {
            try
            {
                Console.Write("Enter the Course ID to update: ");
                if (int.TryParse(Console.ReadLine(), out int courseId))
                {
                    var existingCourse = await courseRepository.GetCourseById(courseId);
                    if (existingCourse != null)
                    {
                        Console.Write("Enter New Course Name (leave blank to keep current): ");
                        var name = Console.ReadLine();
                        Console.Write("Enter New Course Duration (in months, leave blank to keep current): ");
                        var durationInput = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(name)) existingCourse.Course_name = name;
                        if (int.TryParse(durationInput, out int duration)) existingCourse.Duration = duration;

                        await courseRepository.UpdateCourse(existingCourse);
                        Console.WriteLine("Course updated successfully.");
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
                Console.WriteLine($"Error updating course: {ex.Message}");
            }
        }
        //5. Delete Course
        public static async Task DeleteCourse(CourseRepository courseRepository)
        {
            try
            {
                Console.Write("Enter the Course ID to delete: ");
                if (int.TryParse(Console.ReadLine(), out int courseId))
                {
                    await courseRepository.DeleteCourse(courseId);
                    Console.WriteLine("Course deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid Course ID.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting course: {ex.Message}");
            }
        }
        //6. Get Courses By Department
        public static async Task GetCoursesByDepartment(CourseRepository courseRepository)
        {
            try
            {
                Console.Write("Enter the Department ID: ");
                if (int.TryParse(Console.ReadLine(), out int departmentId))
                {
                    var courses = await courseRepository.GetCoursesByDepartment(departmentId);
                    foreach (var course in courses)
                    {
                        Console.WriteLine($"ID: {course.Course_id}, Name: {course.Course_name}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid Department ID.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching courses by department: {ex.Message}");
            }
        }
        //7. Get Courses By Duration
        public static async Task GetCoursesByDuration(CourseRepository courseRepository)
        {
            try
            {
                Console.Write("Enter minimum duration (in months): ");
                if (int.TryParse(Console.ReadLine(), out int minDuration))
                {
                    var courses = await courseRepository.GetCoursesWithDuration(minDuration);
                    foreach (var course in courses)
                    {
                        Console.WriteLine($"ID: {course.Course_id}, Name: {course.Course_name}, Duration: {course.Duration}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid duration.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching courses by duration: {ex.Message}");
            }
        }
        //8. Paginate Courses
        public static async Task PaginateCourses(CourseRepository courseRepository)
        {
            try
            {
                Console.Write("Enter the page number: ");
                if (int.TryParse(Console.ReadLine(), out int pageNumber))
                {
                    Console.Write("Enter the page size: ");
                    if (int.TryParse(Console.ReadLine(), out int pageSize))
                    {
                        var courses = await courseRepository.PaginateCourses(pageNumber, pageSize);
                        foreach (var course in courses)
                        {
                            Console.WriteLine($"ID: {course.Course_id}, Name: {course.Course_name}, Duration: {course.Duration}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid page size. Please enter a valid number.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid page number. Please enter a valid number.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching paginated courses: {ex.Message}");
            }
        }

        //                 FacultyMenu
        public static async Task FacultyMenu(FacultyRepository facultyRepository)
        {
            try
            {
                bool exit = false;
                while (!exit)
                {
                    Console.Clear();
                    Console.WriteLine("===== Faculty Management =====");
                    Console.WriteLine("1. View All Faculties");
                    Console.WriteLine("2. Add New Faculty");
                    Console.WriteLine("3. Update Faculty");
                    Console.WriteLine("4. Delete Faculty");
                    Console.WriteLine("5. Get Faculty By ID");
                    Console.WriteLine("6. Get Faculties By Department");
                    Console.WriteLine("7. Return to Main Menu");
                    Console.Write("Select an option: ");
                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            await GetAllFaculties(facultyRepository);
                            break;
                        case "2":
                            //await AddFaculty(facultyRepository);
                            break;
                        case "3":
                            //await UpdateFaculty(facultyRepository);
                            break;
                        case "4":
                            //await DeleteFaculty(facultyRepository);
                            break;
                        case "5":
                            //await GetFacultyById(facultyRepository);
                            break;
                        case "6":
                            //await GetFacultyByDepartment(facultyRepository);
                            break;
                        case "7":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }

                    if (!exit)
                    {
                        Console.WriteLine("\nPress any key to return to the Faculty Menu...");
                        Console.ReadKey();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in the Faculty Menu: {ex.Message}");
            }
        }
        private static async Task GetAllFaculties(FacultyRepository facultyRepository)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== All Faculties =====");

                var faculties = await facultyRepository.GetAllFaculties();
                if (faculties.Any())
                {
                    foreach (var faculty in faculties)
                    {
                        Console.WriteLine($"ID: {faculty.Fid}");
                        Console.WriteLine($"Name: {faculty.Name}");
                        Console.WriteLine($"Department: {faculty.Department?.D_name ?? "N/A"}");
                        Console.WriteLine($"Subjects: {string.Join(", ", faculty.Subjects.Select(s => s.Subject_name))}");
                        Console.WriteLine(new string('-', 40));

                        // Display all phone numbers
                        if (faculty.Faculty_Phones != null && faculty.Faculty_Phones.Any())
                        {
                            Console.WriteLine($"Mobile(s): {string.Join(", ", faculty.Faculty_Phones.Select(fp => fp.Phone_no))}");
                        }
                        else
                        {
                            Console.WriteLine("Mobile(s): None");
                        }

                        Console.WriteLine($"Salary: {faculty.Salary:C}");
                        Console.WriteLine(new string('-', 40));
                    }
                }
                else
                {
                    Console.WriteLine("No faculties found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving faculties: {ex.Message}");
            }
        }



    }
}
