using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutSysCollegeManagement.Models
{
    public class Exams
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Exam_code { get; set; }
        //Exam name
        [Required(ErrorMessage = "Exam name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Exam name must be between 2 and 100 characters.")]
        public string D_name { get; set; }
        //Room 
        [Required(ErrorMessage = "Room is required.")]
        [StringLength(10, ErrorMessage = "Room must be a maximum of 10 characters.")]
        public string Room { get; set; }
        //date of exam
        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        // time of exam
        [Required(ErrorMessage = "Time is required.")]
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }
        //department section
        [ForeignKey("Department")]
        public int Department_id { get; set; }

        // Navigation 
        public virtual Department Department { get; set; }
    }
}
