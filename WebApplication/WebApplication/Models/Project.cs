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

        [Display(Name = "Giảng viên")]
        public virtual Lecturer Lecturer { get; set; }

        public short ProjectTypeId { get; set; }

        [Display(Name = "Loại đồ án")]
        public virtual ProjectType ProjectType { get; set; }

        [Display(Name = "Tên đồ án")]
        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Trạng thái")]
        public byte Status { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<ProjectMember> ProjectMembers { get; set; }

        public virtual ICollection<ProjectSchedule> ProjectSchedules { get; set; }

    }
}