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
            var departmentRepository = new DepartmentRepository(dbContext);
            var examRepository = new ExamRepository(dbContext);
            var subjectRepository = new SubjectRepository(dbContext);
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
                            await DepartmentMenu(departmentRepository);
                            break;
                        case "3":
                            await ExamMenu(examRepository);
                            break;
                        case "4":
                            await CourseMenu(courseRepository);
                            break;
                        case "5":
                            await SubjectMenu(subjectRepository);
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
                            await AddFaculty(facultyRepository);
                            break;
                        case "3":
                            await UpdateFaculty(facultyRepository);
                            break;
                        case "4":
                            await DeleteFaculty(facultyRepository);
                            break;
                        case "5":
                            await GetFacultyById(facultyRepository);
                            break;
                        case "6":
                            await GetFacultyByDepartment(facultyRepository);
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
        //1. View All Faculties
        public static async Task GetAllFaculties(FacultyRepository facultyRepository)
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
        //2. Add New Faculty
        private static async Task AddFaculty(FacultyRepository facultyRepository)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== Add New Faculty =====");

                // Input Faculty Details
                Console.Write("Enter Faculty Name: ");
                var name = Console.ReadLine();

                Console.Write("Enter Department ID (or leave blank for no department): ");
                var departmentInput = Console.ReadLine();
                int? departmentId = string.IsNullOrEmpty(departmentInput) ? null : int.Parse(departmentInput);

                Console.Write("Enter Salary: ");
                var salary = decimal.Parse(Console.ReadLine());

                // Input Phone Numbers
                var phoneNumbers = new List<Faculty_Phone>();
                bool addMorePhones = true;

                while (addMorePhones)
                {
                    Console.Write("Enter Phone Number (8 digits): ");
                    var phoneNo = Console.ReadLine();
                    phoneNumbers.Add(new Faculty_Phone { Phone_no = phoneNo });

                    Console.Write("Do you want to add another phone number? (y/n): ");
                    var choice = Console.ReadLine();
                    addMorePhones = choice?.ToLower() == "y";
                }

                // Create Faculty Object
                var faculty = new Faculty
                {
                    Name = name,
                    Department_id = departmentId,
                    Salary = salary,
                    Faculty_Phones = phoneNumbers
                };

                // Save Faculty to Repository
                await facultyRepository.AddFaculty(faculty);
                Console.WriteLine("Faculty added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding faculty: {ex.Message}");
            }
        }
        //3. Update Faculty
        private static async Task UpdateFaculty(FacultyRepository facultyRepository)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== Update Faculty =====");

                // Get Faculty ID
                Console.Write("Enter Faculty ID to update: ");
                if (!int.TryParse(Console.ReadLine(), out var facultyId))
                {
                    Console.WriteLine("Invalid Faculty ID. Please enter a numeric value.");
                    return;
                }

                // Retrieve Faculty
                var faculty = await facultyRepository.GetFacultyById(facultyId);
                if (faculty == null)
                {
                    Console.WriteLine($"Faculty with ID {facultyId} not found.");
                    return;
                }

                // Display Current Details
                Console.WriteLine($"Current Name: {faculty.Name}");
                Console.WriteLine($"Current Department ID: {faculty.Department_id}");
                Console.WriteLine($"Current Salary: {faculty.Salary}");
                Console.WriteLine("Current Phone Numbers:");
                foreach (var phone in faculty.Faculty_Phones)
                {
                    Console.WriteLine($"- {phone.Phone_no}");
                }

                // Update Faculty Details
                Console.Write("Enter new Name (leave blank to keep current): ");
                var name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name))
                {
                    faculty.Name = name;
                }

                Console.Write("Enter new Department ID (leave blank to keep current): ");
                var departmentInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(departmentInput))
                {
                    faculty.Department_id = int.Parse(departmentInput);
                }

                Console.Write("Enter new Salary (leave blank to keep current): ");
                var salaryInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(salaryInput))
                {
                    faculty.Salary = decimal.Parse(salaryInput);
                }

                // Update Phone Numbers
                Console.WriteLine("Do you want to update phone numbers? (y/n): ");
                var updatePhonesChoice = Console.ReadLine()?.ToLower();
                if (updatePhonesChoice == "y")
                {
                    // Clear existing phone numbers
                    faculty.Faculty_Phones.Clear();

                    // Add new phone numbers
                    var newPhoneNumbers = new List<Faculty_Phone>();
                    bool addMorePhones = true;

                    while (addMorePhones)
                    {
                        Console.Write("Enter new Phone Number (8 digits): ");
                        var phoneNo = Console.ReadLine();
                        newPhoneNumbers.Add(new Faculty_Phone { Fid = faculty.Fid, Phone_no = phoneNo });

                        Console.Write("Do you want to add another phone number? (y/n): ");
                        var choice = Console.ReadLine();
                        addMorePhones = choice?.ToLower() == "y";
                    }

                    faculty.Faculty_Phones = newPhoneNumbers;
                }

                // Save Changes
                await facultyRepository.UpdateFaculty(faculty);
                Console.WriteLine("Faculty details updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating faculty: {ex.Message}");
            }
        }
        //4. Delete Faculty
        public static async Task DeleteFaculty(FacultyRepository facultyRepository)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== Delete Faculty =====");

                // Get Faculty ID
                Console.Write("Enter Faculty ID to delete: ");
                if (!int.TryParse(Console.ReadLine(), out var facultyId))
                {
                    Console.WriteLine("Invalid Faculty ID. Please enter a numeric value.");
                    return;
                }

                // Retrieve Faculty
                var faculty = await facultyRepository.GetFacultyById(facultyId);
                if (faculty == null)
                {
                    Console.WriteLine($"Faculty with ID {facultyId} not found.");
                    return;
                }

                // Confirm Deletion
                Console.WriteLine($"Are you sure you want to delete faculty: {faculty.Name}? (y/n): ");
                var confirmDelete = Console.ReadLine()?.ToLower();
                if (confirmDelete != "y")
                {
                    Console.WriteLine("Faculty deletion cancelled.");
                    return;
                }

                // Delete Associated Data (Faculty Phones)
                if (faculty.Faculty_Phones != null && faculty.Faculty_Phones.Any())
                {
                    await facultyRepository.DeleteFacultyPhones(faculty.Faculty_Phones);
                    Console.WriteLine("Associated phone numbers deleted.");
                }

                // Delete the Faculty by passing the Faculty ID
                await facultyRepository.DeleteFaculty(facultyId);

                Console.WriteLine("Faculty deleted successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the faculty: {ex.Message}");
            }
        }
        //5. Get Faculty By ID
        public static async Task GetFacultyById(FacultyRepository facultyRepository)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== Get Faculty By ID =====");

                // Get Faculty ID
                Console.Write("Enter Faculty ID to fetch details: ");
                if (!int.TryParse(Console.ReadLine(), out var facultyId))
                {
                    Console.WriteLine("Invalid Faculty ID. Please enter a numeric value.");
                    return;
                }

                // Retrieve Faculty
                var faculty = await facultyRepository.GetFacultyById(facultyId);
                if (faculty == null)
                {
                    Console.WriteLine($"Faculty with ID {facultyId} not found.");
                    return;
                }

                // Display Faculty details
                Console.WriteLine($"ID: {faculty.Fid}");
                Console.WriteLine($"Name: {faculty.Name}");
                Console.WriteLine($"Department: {faculty.Department?.D_name}");
                Console.WriteLine($"Salary: {faculty.Salary:C}");
                Console.WriteLine("Subjects: " + (faculty.Subjects != null && faculty.Subjects.Any() ? string.Join(", ", faculty.Subjects.Select(s => s.Subject_name)) : "None"));
                Console.WriteLine("Courses: " + (faculty.Courses != null && faculty.Courses.Any() ? string.Join(", ", faculty.Courses.Select(c => c.Course_name)) : "None"));
                Console.WriteLine(new string('-', 40));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving the faculty: {ex.Message}");
            }
        }
        //6. Get Faculties By Department
        public static async Task GetFacultyByDepartment(FacultyRepository facultyRepository)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== Get Faculty By Department =====");

                // Get Department ID
                Console.Write("Enter Department ID to fetch faculty members: ");
                if (!int.TryParse(Console.ReadLine(), out var departmentId))
                {
                    Console.WriteLine("Invalid Department ID. Please enter a numeric value.");
                    return;
                }

                // Retrieve Faculty by Department
                var faculties = await facultyRepository.GetFacultyByDepartment(departmentId);
                if (faculties == null || !faculties.Any())
                {
                    Console.WriteLine($"No faculty members found in department with ID {departmentId}.");
                    return;
                }

                // Display Faculty details
                Console.WriteLine($"Faculty Members in Department ID {departmentId}:");
                foreach (var faculty in faculties)
                {
                    Console.WriteLine($"ID: {faculty.Fid}");
                    Console.WriteLine($"Name: {faculty.Name}");
                    Console.WriteLine($"Department: {faculty.Department?.D_name}");
                    Console.WriteLine($"Salary: {faculty.Salary:C}");
                    Console.WriteLine("Subjects: " + (faculty.Subjects != null && faculty.Subjects.Any() ? string.Join(", ", faculty.Subjects.Select(s => s.Subject_name)) : "None"));
                    Console.WriteLine("Courses: " + (faculty.Courses != null && faculty.Courses.Any() ? string.Join(", ", faculty.Courses.Select(c => c.Course_name)) : "None"));
                    Console.WriteLine(new string('-', 40));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving faculty members by department: {ex.Message}");
            }
        }

        //                       DepartmentMenu
        public static async Task DepartmentMenu(DepartmentRepository departmentRepository)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Department Menu =====");
                Console.WriteLine("1. View All Departments");
                Console.WriteLine("2. Add New Department");
                Console.WriteLine("3. Update Department");
                Console.WriteLine("4. Delete Department");
                Console.WriteLine("5. Return to Main Menu");
                Console.Write("Choose an option: ");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        await ViewAllDepartments(departmentRepository);
                        break;
                    case "2":
                        await AddNewDepartment(departmentRepository);
                        break;
                    case "3":
                      await UpdateDepartment(departmentRepository);
                        break;
                    case "4":
                       await DeleteDepartment(departmentRepository);
                        break;
                    case "5":
                        return; // Exit to main menu
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }
        public static async Task ViewAllDepartments(DepartmentRepository departmentRepository)
        {
            var departments = await departmentRepository.GetAllDepartments();
            if (departments != null && departments.Any())
            {
                Console.Clear();
                Console.WriteLine("===== All Departments =====");
                foreach (var department in departments)
                {
                    Console.WriteLine($"ID: {department.Department_id}");
                    Console.WriteLine($"Name: {department.D_name}");
                    Console.WriteLine(new string('-', 30));
                }
            }
            else
            {
                Console.WriteLine("No departments found.");
            }
        }
        public static async Task AddNewDepartment(DepartmentRepository departmentRepository)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== Add New Department =====");

                Console.Write("Enter Department Name: ");
                var departmentName = Console.ReadLine();

                // Validate user input
                if (string.IsNullOrEmpty(departmentName))
                {
                    Console.WriteLine("Department name is required.");
                    return;
                }

                var department = new Department { D_name = departmentName };

                await departmentRepository.AddDepartment(department);
                Console.WriteLine("Department added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public static async Task UpdateDepartment(DepartmentRepository departmentRepository)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== Update Department =====");

                // Get Department ID
                Console.Write("Enter Department ID to update: ");
                if (!int.TryParse(Console.ReadLine(), out var departmentId))
                {
                    Console.WriteLine("Invalid Department ID. Please enter a numeric value.");
                    return;
                }

                var department = await departmentRepository.GetDepartmentById(departmentId);
                if (department == null)
                {
                    Console.WriteLine($"Department with ID {departmentId} not found.");
                    return;
                }

                // Update department name
                Console.Write($"Enter new name for department (current: {department.D_name}): ");
                var newName = Console.ReadLine();

                if (!string.IsNullOrEmpty(newName))
                {
                    department.D_name = newName;
                    await departmentRepository.UpdateDepartment(department);
                    Console.WriteLine("Department updated successfully!");
                }
                else
                {
                    Console.WriteLine("Department name cannot be empty.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public static async Task DeleteDepartment(DepartmentRepository departmentRepository)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== Delete Department =====");

                // Get Department ID
                Console.Write("Enter Department ID to delete: ");
                if (!int.TryParse(Console.ReadLine(), out var departmentId))
                {
                    Console.WriteLine("Invalid Department ID. Please enter a numeric value.");
                    return;
                }

                var department = await departmentRepository.GetDepartmentById(departmentId);
                if (department == null)
                {
                    Console.WriteLine($"Department with ID {departmentId} not found.");
                    return;
                }

                // Confirm Deletion
                Console.WriteLine($"Are you sure you want to delete the department: {department.D_name}? (y/n): ");
                var confirmDelete = Console.ReadLine()?.ToLower();
                if (confirmDelete != "y")
                {
                    Console.WriteLine("Department deletion cancelled.");
                    return;
                }

                // Delete the department
                await departmentRepository.DeleteDepartment(departmentId);
                Console.WriteLine("Department deleted successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        //                       ExamMenu
        public static async Task ExamMenu(ExamRepository examRepository)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Exam Menu =====");
                Console.WriteLine("1. View All Exams");
                Console.WriteLine("2. Add New Exam");
                Console.WriteLine("3. Update Exam");
                Console.WriteLine("4. Delete Exam");
                Console.WriteLine("5. View Exams by Date");
                Console.WriteLine("6. View Exams by Student");
                Console.WriteLine("7. Return to Main Menu");
                Console.Write("Choose an option: ");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        await ViewAllExams(examRepository);
                        break;
                    case "2":
                        await AddNewExam(examRepository);
                        break;
                    case "3":
                        await UpdateExam(examRepository);
                        break;
                    case "4":
                        await DeleteExam(examRepository);
                        break;
                    case "5":
                        await ViewExamsByDate(examRepository);
                        break;
                    case "6":
                        await ViewExamsByStudent(examRepository);
                        break;
                    case "7":
                        return; // Exit to main menu
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }
        public static async Task ViewAllExams(ExamRepository examRepository)
        {
            var exams = await examRepository.GetAllExams();
            if (exams != null && exams.Any())
            {
                Console.Clear();
                Console.WriteLine("===== All Exams =====");
                foreach (var exam in exams)
                {
                    Console.WriteLine($"Exam Code: {exam.Exam_code}");
                    Console.WriteLine($"Exam Name: {exam.D_name}");
                    Console.WriteLine($"Department: {exam.Department?.D_name}");
                    Console.WriteLine($"Date: {exam.Date.ToShortDateString()}");
                    Console.WriteLine($"Students Enrolled: {exam.Students?.Count}");
                    Console.WriteLine(new string('-', 30));
                }
            }
            else
            {
                Console.WriteLine("No exams found.");
            }
        }
        public static async Task AddNewExam(ExamRepository examRepository)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== Add New Exam =====");

                // Exam Name
                Console.Write("Enter Exam Name: ");
                var examName = Console.ReadLine();

                if (string.IsNullOrEmpty(examName))
                {
                    Console.WriteLine("Exam name is required.");
                    return;
                }

                // Department ID
                Console.Write("Enter Department ID: ");
                if (!int.TryParse(Console.ReadLine(), out var departmentId))
                {
                    Console.WriteLine("Invalid Department ID.");
                    return;
                }

                // Exam Date
                Console.Write("Enter Exam Date (yyyy-mm-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out var examDate))
                {
                    Console.WriteLine("Invalid date format.");
                    return;
                }

                var exam = new Exams
                {
                    D_name = examName,
                    Department_id = departmentId,
                    Date = examDate
                };

                await examRepository.AddExam(exam);
                Console.WriteLine("Exam added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public static async Task UpdateExam(ExamRepository examRepository)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== Update Exam =====");

                // Get Exam Code
                Console.Write("Enter Exam Code to update: ");
                if (!int.TryParse(Console.ReadLine(), out var examCode))
                {
                    Console.WriteLine("Invalid Exam Code.");
                    return;
                }

                var exam = await examRepository.GetExamById(examCode);
                if (exam == null)
                {
                    Console.WriteLine($"Exam with code {examCode} not found.");
                    return;
                }

                // Update Exam Details
                Console.Write($"Enter new name for exam (current: {exam.D_name}): ");
                var newName = Console.ReadLine();
                if (!string.IsNullOrEmpty(newName))
                {
                    exam.D_name = newName;
                }

                Console.Write($"Enter new date for exam (current: {exam.Date.ToShortDateString()}): ");
                if (DateTime.TryParse(Console.ReadLine(), out var newDate))
                {
                    exam.Date = newDate;
                }

                await examRepository.UpdateExam(exam);
                Console.WriteLine("Exam updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public static async Task DeleteExam(ExamRepository examRepository)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== Delete Exam =====");

                // Get Exam Code
                Console.Write("Enter Exam Code to delete: ");
                if (!int.TryParse(Console.ReadLine(), out var examCode))
                {
                    Console.WriteLine("Invalid Exam Code.");
                    return;
                }

                var exam = await examRepository.GetExamById(examCode);
                if (exam == null)
                {
                    Console.WriteLine($"Exam with code {examCode} not found.");
                    return;
                }

                // Confirm Deletion
                Console.WriteLine($"Are you sure you want to delete the exam: {exam.D_name}? (y/n): ");
                var confirmDelete = Console.ReadLine()?.ToLower();
                if (confirmDelete != "y")
                {
                    Console.WriteLine("Exam deletion cancelled.");
                    return;
                }

                await examRepository.DeleteExam(examCode);
                Console.WriteLine("Exam deleted successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public static async Task ViewExamsByDate(ExamRepository examRepository)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== View Exams by Date =====");

                // Get Date Range
                Console.Write("Enter Start Date (yyyy-mm-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out var startDate))
                {
                    Console.WriteLine("Invalid date format.");
                    return;
                }

                Console.Write("Enter End Date (yyyy-mm-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out var endDate))
                {
                    Console.WriteLine("Invalid date format.");
                    return;
                }

                var exams = await examRepository.GetExamsByDate(startDate, endDate);
                if (exams.Any())
                {
                    Console.WriteLine("Exams within the date range:");
                    foreach (var exam in exams)
                    {
                        Console.WriteLine($"Exam Code: {exam.Exam_code}, Name: {exam.D_name}, Date: {exam.Date.ToShortDateString()}");
                    }
                }
                else
                {
                    Console.WriteLine("No exams found within the specified date range.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public static async Task ViewExamsByStudent(ExamRepository examRepository)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== View Exams by Student =====");

                // Get Student ID
                Console.Write("Enter Student ID: ");
                if (!int.TryParse(Console.ReadLine(), out var studentId))
                {
                    Console.WriteLine("Invalid Student ID.");
                    return;
                }

                var exams = await examRepository.GetExamsByStudent(studentId);
                if (exams.Any())
                {
                    Console.WriteLine($"Exams taken by Student ID {studentId}:");
                    foreach (var exam in exams)
                    {
                        Console.WriteLine($"Exam Code: {exam.Exam_code}, Name: {exam.D_name}, Date: {exam.Date.ToShortDateString()}");
                    }
                }
                else
                {
                    Console.WriteLine("No exams found for this student.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        //                     SubjectMenu
        public static async Task SubjectMenu(SubjectRepository subjectRepository)
        {
            bool showMenu = true;
            while (showMenu)
            {
                Console.Clear();
                Console.WriteLine("Subject Management Menu:");
                Console.WriteLine("1. View all subjects");
                Console.WriteLine("2. View subject by ID");
                Console.WriteLine("3. Add a new subject");
                Console.WriteLine("4. Update a subject");
                Console.WriteLine("5. Delete a subject");
                Console.WriteLine("6. Get subjects taught by a faculty");
                Console.WriteLine("7. Count total subjects");
                Console.WriteLine("8. Exit");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        await ViewAllSubjects(subjectRepository);
                        break;

                    case "2":
                        await ViewSubjectById(subjectRepository);
                        break;

                    case "3":
                        await AddNewSubject(subjectRepository);
                        break;

                    case "4":
                        await UpdateSubject(subjectRepository);
                        break;

                    case "5":
                        await DeleteSubject(subjectRepository);
                        break;

                    case "6":
                        await GetSubjectsByFaculty(subjectRepository);
                        break;

                    case "7":
                        await CountSubjects(subjectRepository);
                        break;

                    case "8":
                        showMenu = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        public static async Task ViewAllSubjects(SubjectRepository subjectRepository)
        {
            var subjects = await subjectRepository.GetAllSubjects();
            Console.WriteLine("\nSubjects List:");
            foreach (var subject in subjects)
            {
                Console.WriteLine($"ID: {subject.Subject_id}, Name: {subject.Subject_name}, Faculty: {subject.Faculty.Name}");
            }
            Console.WriteLine("\nPress any key to go back to the menu...");
            Console.ReadKey();
        }

        public static async Task ViewSubjectById(SubjectRepository subjectRepositor)
        {
            Console.Write("Enter Subject ID: ");
            if (int.TryParse(Console.ReadLine(), out int subjectId))
            {
                var subject = await subjectRepositor.GetSubjectById(subjectId);
                if (subject != null)
                {
                    Console.WriteLine($"\nSubject ID: {subject.Subject_id}");
                    Console.WriteLine($"Name: {subject.Subject_name}");
                    Console.WriteLine($"Faculty: {subject.Faculty.Name}");
                }
                else
                {
                    Console.WriteLine("Subject not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
            Console.WriteLine("\nPress any key to go back to the menu...");
            Console.ReadKey();
        }

        public static async Task AddNewSubject(SubjectRepository subjectRepositor)
        {
            Console.Write("Enter subject name: ");
            string name = Console.ReadLine();
            Console.Write("Enter faculty ID: ");
            if (int.TryParse(Console.ReadLine(), out int facultyId))
            {
                var newSubject = new Subject
                {
                    Subject_name = name,
                    F_id = facultyId
                };

                await subjectRepositor.AddSubject(newSubject);
                Console.WriteLine("Subject added successfully.");
            }
            else
            {
                Console.WriteLine("Invalid faculty ID.");
            }
            Console.WriteLine("\nPress any key to go back to the menu...");
            Console.ReadKey();
        }

        public static async Task UpdateSubject(SubjectRepository subjectRepositor)
        {
            Console.Write("Enter the ID of the subject to update: ");
            if (int.TryParse(Console.ReadLine(), out int subjectId))
            {
                var subject = await subjectRepositor.GetSubjectById(subjectId);
                if (subject != null)
                {
                    Console.Write("Enter new name for the subject: ");
                    subject.Subject_name = Console.ReadLine();

                    Console.Write("Enter new faculty ID: ");
                    if (int.TryParse(Console.ReadLine(), out int facultyId))
                    {
                        subject.F_id = facultyId;
                        await subjectRepositor.UpdateSubject(subject);
                        Console.WriteLine("Subject updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid faculty ID.");
                    }
                }
                else
                {
                    Console.WriteLine("Subject not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
            Console.WriteLine("\nPress any key to go back to the menu...");
            Console.ReadKey();
        }

        public static async Task DeleteSubject(SubjectRepository subjectRepositor)
        {
            Console.Write("Enter the ID of the subject to delete: ");
            if (int.TryParse(Console.ReadLine(), out int subjectId))
            {
                await subjectRepositor.DeleteSubject(subjectId);
                Console.WriteLine("Subject deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
            Console.WriteLine("\nPress any key to go back to the menu...");
            Console.ReadKey();
        }

        public static async Task GetSubjectsByFaculty(SubjectRepository subjectRepositor)
        {
            Console.Write("Enter faculty ID: ");
            if (int.TryParse(Console.ReadLine(), out int facultyId))
            {
                var subjects = await subjectRepositor.GetSubjectsTaughtByFaculty(facultyId);
                Console.WriteLine("\nSubjects taught by this faculty:");
                foreach (var subject in subjects)
                {
                    Console.WriteLine($"ID: {subject.Subject_id}, Name: {subject.Subject_name}");
                }
            }
            else
            {
                Console.WriteLine("Invalid faculty ID.");
            }
            Console.WriteLine("\nPress any key to go back to the menu...");
            Console.ReadKey();
        }

        public static async Task CountSubjects(SubjectRepository subjectRepository)
        {
            var count = await subjectRepository.CountSubjects();
            Console.WriteLine($"\nTotal number of subjects: {count}");
            Console.WriteLine("\nPress any key to go back to the menu...");
            Console.ReadKey();
        }


    }
}
