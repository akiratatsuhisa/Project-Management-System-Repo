using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class ProjectSchedule
    {
        public long Id { get; set; }

        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        public string Note { get; set; }

        [Required]
        [Range(0,10)]
        public float Rating { get; set; }

        public DateTime ExpiredDate { get; set; }

        public virtual ICollection<ProjectScheduleReport> ProjectScheduleReports { get; set; }
    }
}
