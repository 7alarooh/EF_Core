using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutSysCollegeManagement.Models
{
    public class Faculty
    {
        // Faculty ID 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Fid { get; set; }
        // Faculty name
        [Required(ErrorMessage = "Faculty name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Faculty name must be between 3 and 100 characters.")]
        public string Name { get; set; }
        // Department 
        [ForeignKey("Department")]  
        public int? Department_id { get; set; }  
        public Department Department { get; set; }  // Navigation 
        // Faculty salary
        [Required(ErrorMessage = "Salary is required.")]
        [Range(100, 10000, ErrorMessage = "Salary must be between 100 and 10,000.")]
        public decimal Salary { get; set; }  
    }
}
