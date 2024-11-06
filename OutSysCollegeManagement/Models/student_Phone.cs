using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OutSysCollegeManagement.Models
{
    [PrimaryKey(nameof(SID), nameof(Phone_no))]
    public class student_Phone
    {
        //student id
        [ForeignKey("Student")]
        public int SID { get; set; }
        // Navigation property for the Student reference
        public virtual Student Student { get; set; }
        // phone number
        [Required]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Phone number must be exactly 8 digits.")]
        public string Phone_no { get; set; }
    }
}
