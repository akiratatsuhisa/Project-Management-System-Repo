using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class ProjectViewModel
    {
        public int Id { get; set; }

        public short ProjectTypeId { get; set; }

        public short SpecializedFacultyId { get; set; }

        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
