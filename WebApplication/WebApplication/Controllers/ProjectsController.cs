using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Authorize(Roles = "Student")]
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProjectsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var studentId = _userManager.GetUserId(User);
            return View(_context.ProjectMembers.Where(pm => pm.StudentId == studentId).Select(pm => pm.Project));
        }

        public async Task<IActionResult> Details(int id)
        {
            var studentId = _userManager.GetUserId(User);
            var result = await _context.ProjectMembers.FirstOrDefaultAsync(pm => pm.StudentId == studentId && pm.ProjectId == id);
            if (result == null)
            {
                return NotFound();
            }
            var projectId = result.ProjectId;
            ViewBag.ProjectMembers = _context.ProjectMembers.Where(pm => pm.ProjectId == projectId).Include(pm => pm.Student).ThenInclude(s => s.ApplicationUser);
            //if (_context.ProjectSchedules.Where(ps => ps.ProjectId == projectId).Count() == 0)
            //{
            //    var dateTimeNow = DateTime.Now;
            //    for (var i = 1; i < 11; i++)
            //    {
            //        _context.ProjectSchedules.Add(new ProjectSchedule { ProjectId = projectId, Name = $"Nhiệm vụ tuần {i}", ExpiredDate = dateTimeNow.AddDays(7) });
            //    }
            //    await _context.SaveChangesAsync();
            //}
            ViewBag.ProjectSchedules = _context.ProjectSchedules.Where(ps => ps.ProjectId == projectId).OrderBy(ps => ps.ExpiredDate).ToList();
            return View(await _context.Projects
                    .Include(p => p.Lecturer).ThenInclude(l => l.ApplicationUser)
                    .Include(p => p.ProjectType)
                    .FirstAsync(p => p.Id == projectId));
        }
    }
}