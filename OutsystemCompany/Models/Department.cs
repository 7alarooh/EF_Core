using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutsystemCompany.Models
{
    public class Department : IValidatableObject
    {
        [Key]
        public int Dnumber { get; set; }

        [Required(ErrorMessage = "Department name is required.")]
        [StringLength(100, ErrorMessage = "Department name cannot be longer than 100 characters.")]
        public string Dname { get; set; }

        [ForeignKey("Manager")]
        public int MgrSsn { get; set; }

        public virtual Employee Manager { get; set; }

        [Required(ErrorMessage = "Manager start date is required.")]
        public DateTime MgrStartDate { get; set; }


        [InverseProperty("Department")]
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();


        // Implement IValidatableObject to add complex validation logic
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        { if (MgrSsn <= 0)
            {
                yield return new ValidationResult("Manager's SSN must be a positive value.", new[] { nameof(MgrSsn) });
            }
          if (MgrStartDate > DateTime.Now)
            {
                yield return new ValidationResult("Manager start date cannot be in the future.", new[] { nameof(MgrStartDate) });
            }
        }
    }
}
