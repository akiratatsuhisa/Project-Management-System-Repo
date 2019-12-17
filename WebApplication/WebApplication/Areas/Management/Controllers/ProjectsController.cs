using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize(Roles = "Lecturer")]
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProjectsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var lecturerId = getCurrentLecturerId();
            ViewBag.Lecturer = await _context.Lecturers
                .Include(l => l.ApplicationUser)
                .FirstAsync(l => l.LecturerId == lecturerId);
            return View(_context.Projects
                .Where(p => p.LecturerId == lecturerId)
                .Include(p => p.Lecturer).ThenInclude(l => l.ApplicationUser)
                .Include(p => p.ProjectType));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturerId = getCurrentLecturerId();
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == id && p.LecturerId == lecturerId);

            if (project == null)
            {
                return BadRequest();
            }

            var projectId = project.Id;

            ViewBag.ProjectMembers = _context.ProjectMembers
                .Where(pm => pm.ProjectId == projectId)
                .Include(pm => pm.Student).ThenInclude(s => s.ApplicationUser);

            //Test
            if (_context.ProjectSchedules.Where(ps => ps.ProjectId == projectId).Count() == 0)
            {
                var dateTimeNow = DateTime.Now.AddMonths(-1);
                for (var i = 1; i < 11; i++)
                {
                    dateTimeNow = dateTimeNow.AddDays(7);
                    _context.ProjectSchedules.Add(new ProjectSchedule
                    {
                        ProjectId = projectId,
                        Name = $"Nhiệm vụ tuần {i}",
                        ExpiredDate = dateTimeNow
                    });
                }
                await _context.SaveChangesAsync();
            }

            ViewBag.ProjectSchedules = _context.ProjectSchedules
                .Where(ps => ps.ProjectId == projectId)
                .OrderBy(ps => ps.ExpiredDate)
                .ToList();

            return View(await _context.Projects
                    .Include(p => p.Lecturer).ThenInclude(l => l.ApplicationUser)
                    .Include(p => p.ProjectType)
                    .FirstAsync(p => p.Id == projectId));
        }

        public async Task<IActionResult> EditProjectSchedule(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var result = await _context.ProjectSchedules.FindAsync(id);
            await _context.Entry(result).Reference("Project").LoadAsync();
            if (result.Project.LecturerId == getCurrentLecturerId())
            {
                return View(result);
            }
            return BadRequest();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> EditProjectSchedule(ProjectSchedule projectSchedule)
        {
            var result = await _context.ProjectSchedules.FindAsync(projectSchedule.Id);
            await _context.Entry(result).Reference("Project").LoadAsync();
            if (result.Project.LecturerId == getCurrentLecturerId())
            {
                result.Name = projectSchedule.Name;
                result.Description = projectSchedule.Description;
                result.ExpiredDate = projectSchedule.ExpiredDate;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = result.ProjectId });
            }
            return BadRequest();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> EvaluateProjectSchedule(ProjectSchedule projectSchedule)
        {
            var result = await _context.ProjectSchedules.FindAsync(projectSchedule.Id);
            await _context.Entry(result).Reference("Project").LoadAsync();
            if (result.Project.LecturerId == getCurrentLecturerId())
            {
                result.Rating = projectSchedule.Rating;
                result.Comment = projectSchedule.Comment;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = result.ProjectId });
            }
            return BadRequest();
        }

        private string getCurrentLecturerId() => _userManager.GetUserId(User);
    }
}
