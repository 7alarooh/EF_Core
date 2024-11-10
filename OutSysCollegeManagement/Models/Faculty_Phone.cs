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
    
    [PrimaryKey(nameof(SID), nameof(Mobile_no),nameof(Fid))]
    public class Faculty_Phone
    {
        //student id
        [ForeignKey("Student")]  
        public int SID { get; set; }
        public virtual Student Student { get; set; } // Navigation 

        //Faculty id
        [ForeignKey("Faculty")]
        public int Fid { get; set; }
        public virtual Faculty Faculty { get; set; } // Navigation 

        // phone number
        [Required(ErrorMessage = "Mobile number is required.")]
        [StringLength(15, ErrorMessage = "Mobile number cannot exceed 15 characters.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Mobile number must be exactly 8 digits.")]
        public string Mobile_no { get; set; }  // Mobile number
    }
}
