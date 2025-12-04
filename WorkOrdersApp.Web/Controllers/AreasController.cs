using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkOrdersApp.Infrastructure.Data;
using WorkOrdersApp.Core.Entities;

namespace WorkOrdersApp.Web.Controllers;

public class AreasController : Controller
{
    private readonly AppDbContext _context;

    public AreasController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var areas = await _context.Areas.ToListAsync();
        // Use explicit view path to avoid runtime discovery issues on some setups
        return View("~/Views/Areas/Index.cshtml", areas);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Area area)
    {
        if (!ModelState.IsValid) return View(area);
        _context.Areas.Add(area);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var area = await _context.Areas.FindAsync(id);
        if (area == null) return NotFound();
        return View(area);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Area area)
    {
        if (!ModelState.IsValid) return View(area);
        _context.Areas.Update(area);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.Areas.FindAsync(id);
        if (entity != null)
        {
            _context.Areas.Remove(entity);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var area = await _context.Areas.FindAsync(id);
        if (area == null) return NotFound();
        return View(area);
    }
}
