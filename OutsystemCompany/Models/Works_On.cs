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
    [PrimaryKey(nameof(Essn), nameof(Pno))]
    public class Works_On
    {
        [ForeignKey("Employee")]
        [Required(ErrorMessage = "Employee SSN is required.")]
        public int Essn { get; set; }
        public virtual Employee Employee { get; set; }

        [ForeignKey("Project")]
        [Required(ErrorMessage = "Project number is required.")]
        public int Pno { get; set; }
        public virtual Project Project { get; set; }

        [Range(0, 168, ErrorMessage = "Hours must be between 0 and 168.")]
        public decimal Hours { get; set; } 
    }
}
