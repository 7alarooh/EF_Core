using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutSysCollegeManagement.Models
{
    // Define an enum for Student Status
    public enum StudentStatus
    {
        Active,
        Inactive,
        Graduated,
        Suspended,
        Withdrawn
    }
    public class Student
    {
        // Student ID
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SID { get; set; }

        //First Name
        [Required(ErrorMessage = "First Name is required.")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "First Name must contain only letters.")]
        [StringLength(20, ErrorMessage = "First Name can't be longer than 50 characters.")]
        public string FName { get; set; }

        //Last Name
        [Required(ErrorMessage = "Last Name is required.")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Last Name must contain only letters.")]
        [StringLength(20, ErrorMessage = "Last Name can't be longer than 50 characters.")]
        public string LName { get; set; }

        //Date of BD
        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Student), nameof(ValidateDOB))]
        public DateTime DOB { get; set; }

        //City
        [Required(ErrorMessage = "City is required.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "City must contain only letters and spaces.")]
        [StringLength(50, ErrorMessage = "City can't be longer than 50 characters.")]
        public string City { get; set; }

        //Pin Code
        [Required(ErrorMessage = "PinCode is required.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "PinCode must be exactly 6 digits.")]
        public int  PinCode { get; set; }

        //Student State in College
        [Required(ErrorMessage = "Status is required.")]
        public StudentStatus State { get; set; }= StudentStatus.Active;
        // Navigation property for the many-to-many relationship
        public virtual ICollection<Course> Courses { get; set; }
        // F_id as foreign key referencing Faculty
        [ForeignKey("Faculty")]  
        public int F_id { get; set; }  
        public virtual Faculty Faculty { get; set; }  // Navigation
        //----------------------------------------------------------------//

        public static ValidationResult ValidateDOB(DateTime dob, ValidationContext context)
        {
            if (dob > DateTime.Now || dob < DateTime.Now.AddYears(-120))
            {
                return new ValidationResult("Date of Birth must be a valid date.");
            }
            return ValidationResult.Success;
        }

        public override string ToString()
        {
            return $"Student ID: {SID}, Name: {FName} {LName}, DOB: {DOB.ToShortDateString()}, " +
                   $"City: {City}, PinCode: {PinCode}, State: {State}";
        }
    }
}
