using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Faculty
    {
        public short Id { get; set; }

        [StringLength(256)]
        [Required]
        public string Name { get; set; }
    }
}
