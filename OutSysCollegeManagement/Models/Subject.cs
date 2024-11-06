using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutSysCollegeManagement.Models
{
    public class Subject
    {
        //subjet name
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Subject_id { get; set; }
        //subject name
        [Required(ErrorMessage = "Subject name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Subject name must be between 2 and 100 characters.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Subject name can only contain letters and spaces.")]
        public string Subject_name { get; set; }
    }
}
