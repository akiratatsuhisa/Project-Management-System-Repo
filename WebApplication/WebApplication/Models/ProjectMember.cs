using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class ProjectMember
    {
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        public string StudentId { get; set; }

        public virtual Student Student { get; set; }

        [Required]
        [Range(0,10)]
        public float Grade { get; set; }
    }
}
