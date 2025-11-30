namespace WorkOrdersApp.Core.Entities;

public class WorkOrder
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public WorkOrderStatus Status { get; set; } = WorkOrderStatus.Pending;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }

    // Relación 1-N (1 área, muchas órdenes)
    public int AreaId { get; set; }
    public Area? Area { get; set; }
}
