using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo_list.Data;
using todo_list.Models;

namespace todo_list.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;
    

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {

        var content = await _context.Notes.Where(n => n.Done == false)
            .OrderBy(n => n.DueDate)
            .Take(10)
            .ToListAsync();
        
        return View(content);
    }
    
    /*//GET set Done
    public async Task<IActionResult> Done(long? id)
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
        
        return View(noteViewModel);
    }
*/
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToogleDone(long id)
    {
        var noteViewModel = await _context.Notes.FindAsync(id);

        if (noteViewModel == null)
        {
            return NotFound();
        }

        noteViewModel.Done = true;
        _context.Update(noteViewModel);
        await _context.SaveChangesAsync();
        
        return RedirectToAction(nameof(Index)); 
        
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    private bool NoteViewModelExists(long id)
    {
        return _context.Notes.Any(e => e.Id == id);
    }
}
