using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class ProjectType
    {
        public short Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public bool IsDisabled { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
