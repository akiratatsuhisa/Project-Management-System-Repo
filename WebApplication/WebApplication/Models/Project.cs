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

        public ProjectStatusViewModel StatusViewModel
        {
            get => Status switch
            {
                0 => new ProjectStatusViewModel { Name = "Đang tiến hành", TextClassName = "text-primary" },

                2 => new ProjectStatusViewModel { Name = "Không được tiếp tục", TextClassName = "text-warning" },

                4 => new ProjectStatusViewModel { Name = "Đợi chấm", TextClassName = "text-info" },

                6 => new ProjectStatusViewModel { Name = "Đạt", TextClassName = "text-success" },

                8 => new ProjectStatusViewModel { Name = "Rớt", TextClassName = "text-danger" },

                10 => new ProjectStatusViewModel { Name = "Bị hủy", TextClassName = "text-danger" },

                _ => new ProjectStatusViewModel { Name = "Không có trạng thái" }
            };
        }
    }
}