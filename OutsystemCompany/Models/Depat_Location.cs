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
    [PrimaryKey(nameof(Dnumber), nameof(Dlocation))]
    public class DeptLocation
    {
        [ForeignKey("Department")]
        public int Dnumber { get; set; }

        public virtual Department Department { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(100, ErrorMessage = "Location cannot be longer than 100 characters.")]
        public string Dlocation { get; set; }
    }
}
