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

namespace WebApplication.Controllers
{
    [Authorize(Roles = "Student")]
    public class ProjectsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //// GET: Projects
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Projects.Include(p => p.Lecturer).Include(p => p.ProjectType).Include(p => p.SpecializedFaculty);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        //// GET: Projects/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var project = await _context.Projects
        //        .Include(p => p.Lecturer)
        //        .Include(p => p.ProjectType)
        //        .Include(p => p.SpecializedFaculty)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (project == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(project);
        //}

        // GET: Projects/Create
        private Faculty GetStudentFaculty()
        {
            var id = _context.Students.Find(_userManager.GetUserId(User)).FacultyId;
            return _context.Faculties.Find(id);
        }

        public IActionResult Create()
        {
            var faculty = GetStudentFaculty();
            ViewData["ProjectTypeId"] = new SelectList(_context.ProjectTypes, "Id", "Name");
            ViewData["FacultyName"] = faculty.Name;
            ViewData["SpecializedFacultyId"] = new SelectList(_context.SpecializedFaculties
                .Where(sf => sf.FacultyId == faculty.Id), "Id", "Name");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LecturerId,ProjectTypeId,SpecializedFacultyId,Title,Description,Status,Semester,CreatedDate")] ProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                var lecturerId = _context.Students.Find(_userManager.GetUserId(User)).LecturerId;
                var dateTimeNow = DateTime.Now;
                _context.Add(new Project
                {
                    LecturerId = lecturerId,
                    ProjectTypeId = project.ProjectTypeId,
                    SpecializedFacultyId = project.SpecializedFacultyId,
                    Title = project.Title,
                    Description = project.Description,
                    Status = 0,
                    CreatedDate = dateTimeNow,
                    Semester = 1
                });
                await _context.SaveChangesAsync();
                return View("CreatedSuccess",project);
            }
            var faculty = GetStudentFaculty();
            ViewData["ProjectTypeId"] = new SelectList(_context.ProjectTypes, "Id", "Name", project.ProjectTypeId);
            ViewData["FacultyId"] = faculty.Name;
            ViewData["SpecializedFacultyId"] = new SelectList(_context.SpecializedFaculties
                .Where(sf => sf.FacultyId == faculty.Id), "Id", "Name", project.SpecializedFacultyId);
            return View(project);
        }
    }
}
