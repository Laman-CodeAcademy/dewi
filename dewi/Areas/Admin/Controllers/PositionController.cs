using dewi.Contexts;
using dewi.Models;
using Microsoft.AspNetCore.Mvc;

namespace dewi.Areas.Admin.Controllers;
    [Area("Admin")]

    public class PositionController(AppDbContext _context) : Controller
    {
        public IActionResult Index()
        {
        var positions = _context.Positions.Select(x => new Position()
        {
            Name=x.Name,
            Id=x.Id,
        }).ToList();

            return View(positions);
        }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Position position) {

        if (!ModelState.IsValid) {
            return View(position);
        }

        await _context.Positions.AddAsync(position);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var position = _context.Positions.Find(id); 
        if (position is null)
        {
            return NotFound();
        }

         _context.Positions.Remove(position);
         _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }


    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var position = _context.Positions.Find(id);
        if (position is null)
        {
            return NotFound();
        }

        return View(position);
    }


    [HttpPost]
    public async Task<IActionResult> Update(Position position)
    {
        var existPosition = await _context.Positions.FindAsync(position.Id);
        if(existPosition == null)
        {
            return NotFound();
        }
        existPosition.Name = position.Name;
        await _context.SaveChangesAsync();
        return (RedirectToAction(nameof(Index)));
    }
}
