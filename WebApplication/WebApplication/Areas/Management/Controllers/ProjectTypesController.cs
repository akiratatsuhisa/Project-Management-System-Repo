using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize(Roles ="Employee")]
    public class ProjectTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Management/ProjectTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProjectTypes.ToListAsync());
        }

        // GET: Management/ProjectTypes/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectType = await _context.ProjectTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectType == null)
            {
                return NotFound();
            }

            return View(projectType);
        }

        // GET: Management/ProjectTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Management/ProjectTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IsDisabled")] ProjectType projectType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(projectType);
        }

        // GET: Management/ProjectTypes/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectType = await _context.ProjectTypes.FindAsync(id);
            if (projectType == null)
            {
                return NotFound();
            }
            return View(projectType);
        }

        // POST: Management/ProjectTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,Name,IsDisabled")] ProjectType projectType)
        {
            if (id != projectType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectTypeExists(projectType.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(projectType);
        }

        // GET: Management/ProjectTypes/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectType = await _context.ProjectTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectType == null)
            {
                return NotFound();
            }

            return View(projectType);
        }

        // POST: Management/ProjectTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var projectType = await _context.ProjectTypes.FindAsync(id);
            _context.ProjectTypes.Remove(projectType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectTypeExists(short id)
        {
            return _context.ProjectTypes.Any(e => e.Id == id);
        }
    }
}
