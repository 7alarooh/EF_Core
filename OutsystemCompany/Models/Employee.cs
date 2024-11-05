using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutsystemCompany.Models
{
    public enum Gender
    {
        Male,
        Female
    }
    public class Employee: IValidatableObject
    {
        [Key] 
        public int Ssn {  get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string Fname { get; set; }
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string Minit { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string Lname {get; set; }
        [Required(ErrorMessage = "Birth date is required.")]
        [DataType(DataType.Date)]
        public DateTime Bdate { get; set; }
        [StringLength(100, ErrorMessage = "Address cannot be longer than 100 characters.")]
        public string Address { get; set; }
        [EnumDataType(typeof(Gender))]
        public Gender Sex { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive value.")]
        public double Salary { get; set; }
        [ForeignKey("Supervisor")]
        public int? Super_Ssn  { get; set; }
        public virtual Employee Supervisor { get; set; }
        [ForeignKey("Department")]
        public int Dno { get; set; }
        public virtual Department Department { get; set; }

        [InverseProperty("Supervisor")]
        public virtual ICollection<Employee> Supervisees { get; set; }

        public virtual ICollection<Works_On> WorksOnProjects   {get; set;}


        // Implement IValidatableObject to add complex validation logic
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Ensure birth date is in the past
            if (Bdate >= DateTime.Now)
            {
                yield return new ValidationResult("Birth date must be in the past.", new[] { nameof(Bdate) });
            }
        }
        // Static method for custom validation logic for birth date
        public static ValidationResult ValidateBirthDate(DateTime date, ValidationContext context)
        {
            if (date >= DateTime.Now)
            {
                return new ValidationResult("Birth date must be in the past.");
            }
            return ValidationResult.Success;
        }
    }
}
