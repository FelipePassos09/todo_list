using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using todo_list.Data;
using todo_list.Models;
using todo_list.Models.Enums;

namespace todo_list.Controllers
{
    public class Notes : Controller
    {
        private readonly AppDbContext _context;

        public Notes(AppDbContext context)
        {
            _context = context;
        }

        // GET: Notes
        public async Task<IActionResult> Index()
        {
            var content = await _context.Notes.Where(data => data.Done == false).ToListAsync();
            
            return View(content);
        }

        // GET: Notes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noteViewModel = await _context.Notes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (noteViewModel == null)
            {
                return NotFound();
            }

            return View(noteViewModel);
        }

        // GET: Notes/Create
        public IActionResult Create()
        {
            ViewBag.Priorities = new SelectList(Enum.GetValues(typeof(Priorities)).Cast<Priorities>().Select(p => new { Value = p, Text = p.ToString() }), "Value", "Text");
            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Subject,Content,Done,Priorities,DueDate,CreatedAt")] NoteViewModel noteViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(noteViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.Priorities = new SelectList(Enum.GetValues(typeof(Priorities)).Cast<Priorities>().Select(p => new { Value = p, Text = p.ToString() }), "Value", "Text");
            return View(noteViewModel);
        }

        // GET: Notes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noteViewModel = await _context.Notes.FindAsync(id);
            if (noteViewModel == null)
            {
                return NotFound();
            }
            
            ViewBag.Priorities = new SelectList(Enum.GetValues(typeof(Priorities)).Cast<Priorities>().Select(p => new { Value = p, Text = p.ToString() }), "Value", "Text");
            return View(noteViewModel);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Subject,Content,Done,Priorities,DueDate,CreatedAt")] NoteViewModel noteViewModel)
        {
            if (id != noteViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(noteViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteViewModelExists(noteViewModel.Id))
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
            ViewBag.Priorities = new SelectList(Enum.GetValues(typeof(Priorities)).Cast<Priorities>().Select(p => new { Value = p, Text = p.ToString() }), "Value", "Text");
            return View(noteViewModel);
        }

        // GET: Notes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noteViewModel = await _context.Notes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (noteViewModel == null)
            {
                return NotFound();
            }

            return View(noteViewModel);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var noteViewModel = await _context.Notes.FindAsync(id);
            if (noteViewModel != null)
            {
                _context.Notes.Remove(noteViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoteViewModelExists(long id)
        {
            return _context.Notes.Any(e => e.Id == id);
        }
    }
}
