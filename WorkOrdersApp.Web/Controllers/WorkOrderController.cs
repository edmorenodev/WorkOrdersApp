using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkOrdersApp.Core.Entities;
using WorkOrdersApp.Infrastructure.Data;

public class WorkOrdersController : Controller
{
    private readonly IWorkOrderRepository _repo;
    private readonly AppDbContext _context;

    public WorkOrdersController(IWorkOrderRepository repo, AppDbContext context)
    {
        _repo = repo;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await _repo.GetAllAsync();
        return View(orders);
    }

    public async Task<IActionResult> Create()
    {
        var areas = await _context.Areas.ToListAsync();
        ViewBag.Areas = new SelectList(areas, "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(WorkOrder order)
    {
        if (!ModelState.IsValid)
        {
            var areas = await _context.Areas.ToListAsync();
            ViewBag.Areas = new SelectList(areas, "Id", "Name");
            return View(order);
        }

        await _repo.AddAsync(order);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var order = await _repo.GetByIdAsync(id);
        if (order == null) return NotFound();
        var areas = await _context.Areas.ToListAsync();
        ViewBag.Areas = new SelectList(areas, "Id", "Name", order.AreaId);
        return View(order);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(WorkOrder order)
    {
        if (!ModelState.IsValid)
        {
            var areas = await _context.Areas.ToListAsync();
            ViewBag.Areas = new SelectList(areas, "Id", "Name", order.AreaId);
            return View(order);
        }

        await _repo.UpdateAsync(order);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _repo.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
