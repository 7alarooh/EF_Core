using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutsystemCompany.Models
{
    [PrimaryKey(nameof(Essn), nameof(DependentName))]
    public class Dependent
    {
        [ForeignKey("Employee")]
        [Required(ErrorMessage = "Employee SSN is required.")]
        public int Essn { get; set; }
        public virtual Employee Employee { get; set; }

        [Required(ErrorMessage = "Dependent name is required.")]
        [StringLength(100, ErrorMessage = "Dependent name cannot be longer than 100 characters.")]
        public string DependentName { get; set; }

        [StringLength(10, ErrorMessage = "Sex must be specified as 'Male' or 'Female'.")]
        public string Sex { get; set; }

        [Required(ErrorMessage = "Birthdate is required.")]
        [DataType(DataType.Date)]
        public DateTime Bdate { get; set; }

        [StringLength(50, ErrorMessage = "Relationship cannot be longer than 50 characters.")]
        public string Relationship { get; set; }
    }
}
