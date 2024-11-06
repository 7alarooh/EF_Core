using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutSysCollegeManagement.Models
{
    public class Hostel
    {
        [Key] //Hostel_id 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Hostel_id { get; set; }
        //Name of the hostel

        [Required(ErrorMessage = "Hostel name is required.")]
        [StringLength(100, ErrorMessage = "Hostel name cannot exceed 100 characters.")]
        public string Hostel_name { get; set; }
        //// Number of seats in the hostel

        [Required(ErrorMessage = "Number of seats is required.")]
        [Range(1, 1000, ErrorMessage = "Number of seats must be between 1 and 1000.")]
        public int No_of_seats { get; set; }
    }
}
