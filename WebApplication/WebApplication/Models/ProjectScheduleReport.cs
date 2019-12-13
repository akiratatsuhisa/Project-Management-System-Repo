using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class ProjectScheduleReport
    {
        public long Id { get; set; }

        public long ProjectScheduleId { get; set; }

        public virtual ProjectSchedule ProjectSchedule { get; set; }

        [Required]
        public string StudentId { get; set; }

        public virtual Student Student { get; set; }

        public string ReportUrl { get; set; }

        public string Note { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}