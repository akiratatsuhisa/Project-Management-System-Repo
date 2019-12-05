using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        public string LecturerId { get; set; }

        public virtual Lecturer Lecturer { get; set; }

        public short ProjectTypeId { get; set; }

        public virtual ProjectType ProjectType { get; set; }

        public short SpecializedFacultyId { get; set; }

        public virtual SpecializedFaculty SpecializedFaculty { get; set; }

        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        public string Description { get; set; }

        public byte Status { get; set; }

        [Range(1,2)]
        public byte Semester { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<ProjectMember> ProjectMembers { get; set; }

        public virtual ICollection<ProjectSchedule> ProjectSchedules { get; set; }

    }
}