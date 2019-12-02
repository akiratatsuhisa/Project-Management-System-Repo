using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Lecturer
    {
        [ForeignKey("ApplicationUser")]
        public string LecturerId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public short FacultyId { get; set; }

        public virtual Faculty Faculty { get; set; }

        public bool IsManager { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
