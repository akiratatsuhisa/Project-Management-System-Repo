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

        [Required]
        [Display(Name = "Loại đồ án")]
        public short ProjectTypeId { get; set; }

        [Required]
        [Display(Name = "Chuyên ngành")]
        public short SpecializedFacultyId { get; set; }

        [Required]
        [StringLength(256)]
        [Display(Name = "Tên đồ án")]
        public string Title { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }
    }
}
