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

        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public short FacultyId { get; set; }

        public virtual Faculty Faculty { get; set; }

        public string LecturerId { get; set; }

        public virtual Lecturer Lecturer { get; set; }

        [Column(TypeName = "char(10)")]
        [RegularExpression(@"^\d{10}$")]
        [Required]
        public string StudentCode { get; set; }

        public virtual ICollection<ProjectMember> ProjectMembers { get; set; }

        public virtual ICollection<ProjectScheduleReport> ProjectScheduleReports { get; set; }
    }
}
