using WorkOrdersApp.Core.Entities;

namespace WorkOrdersApp.Core.Interfaces;

public interface IWorkOrderRepository : IGenericRepository<WorkOrder>
{
    Task<IEnumerable<WorkOrder>> GetByStatusAsync(WorkOrderStatus status);
}