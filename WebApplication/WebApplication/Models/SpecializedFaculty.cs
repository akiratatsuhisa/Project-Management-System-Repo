using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class SpecializedFaculty
    {
        public short Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public short FacultyId { get; set; }

        public virtual Faculty Faculty { get; set; }
    }
}
