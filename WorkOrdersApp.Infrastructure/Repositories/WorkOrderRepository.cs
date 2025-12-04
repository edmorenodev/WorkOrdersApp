using Microsoft.EntityFrameworkCore;
using WorkOrdersApp.Infrastructure.Data;
using WorkOrdersApp.Core.Entities;

public class WorkOrderRepository : IWorkOrderRepository
{
    private readonly AppDbContext _context;

    public WorkOrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WorkOrder>> GetAllAsync() =>
        await _context.WorkOrders.Include(w => w.Area).ToListAsync();

    public async Task<WorkOrder?> GetByIdAsync(int id) =>
        await _context.WorkOrders.Include(w => w.Area)
                                  .FirstOrDefaultAsync(w => w.Id == id);

    public async Task AddAsync(WorkOrder workOrder)
    {
        _context.WorkOrders.Add(workOrder);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(WorkOrder workOrder)
    {
        _context.WorkOrders.Update(workOrder);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.WorkOrders.FindAsync(id);
        if (entity != null)
        {
            _context.WorkOrders.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
