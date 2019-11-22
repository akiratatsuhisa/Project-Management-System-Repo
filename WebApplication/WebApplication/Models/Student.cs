using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Student
    {
        [ForeignKey("ApplicationUser")]
        public string StudentId { get; set; }

        [Required]
        public short FacultyId { get; set; }

        [Required]
        public string LecturerId { get; set; }

        [Column(TypeName ="char(10)")]
        [Required]
        public string StudentCode { get; set; } 

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual Faculty Faculty { get; set; }

        public virtual Lecturer Lecturer { get; set; }
    }
}
