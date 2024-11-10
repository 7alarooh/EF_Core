using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutSysCollegeManagement.Models
{
    public  class Course
    {
        //course id 
        [Key]
        public int Course_id { get; set; }
        // course name
        [Required(ErrorMessage = "Course name is required.")]
        [StringLength(100, ErrorMessage = "Course name can't be longer than 100 characters.")]
        public string Course_name { get; set; }
        //Duration 
        [Required(ErrorMessage = "Duration is required.")]
        public int Duration { get; set; }
        //Date of Beginning
        [Required(ErrorMessage = "Date of Beginning is required.")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        // Foreign key for Department
        [Required]
        [ForeignKey("Department")]
        public int Department_id { get; set; }

        // Navigation property for Department
        public virtual Department Department { get; set; }
        //Navigation
        public virtual ICollection<Student> Students { get; set; }
    }
}
