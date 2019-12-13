using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;

namespace WebApplication.ViewComponents
{
    public class ProjectScheduleReportsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ProjectScheduleReportsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(long id)
        {
            return View(await _context.ProjectScheduleReports
                .Where(psr => psr.ProjectScheduleId == id)
                .Include(psr => psr.Student)
                    .ThenInclude(s => s.ApplicationUser)
                .OrderByDescending(psr => psr.CreatedDate)
                .ToListAsync());
        }
    }
}
