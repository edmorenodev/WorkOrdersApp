using Microsoft.AspNetCore.Mvc;
using WorkOrdersApp.Core.Entities;
using WorkOrdersApp.Infrastructure.Data;

public class WorkOrdersController : Controller
{
    private readonly IWorkOrderRepository _repo;

    public WorkOrdersController(IWorkOrderRepository repo)
    {
        _repo = repo;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await _repo.GetAllAsync();
        return View(orders);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(WorkOrder order)
    {
        if (!ModelState.IsValid) return View(order);

        await _repo.AddAsync(order);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var order = await _repo.GetByIdAsync(id);
        if (order == null) return NotFound();
        return View(order);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(WorkOrder order)
    {
        if (!ModelState.IsValid) return View(order);
        await _repo.UpdateAsync(order);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _repo.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
