using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutsystemCompany.Models
{
    public class Project
    {
        [Key]
        public int Pnumber { get; set; }

        [Required(ErrorMessage = "Project name is required.")]
        [StringLength(100, ErrorMessage = "Project name cannot be longer than 100 characters.")]
        public string Pname { get; set; }

        [StringLength(200, ErrorMessage = "Project location cannot be longer than 200 characters.")]
        public string Plocation { get; set; }

        [ForeignKey("Department")]
        public int Dnum { get; set; }
        public virtual Department Department { get; set; }

    }
}
