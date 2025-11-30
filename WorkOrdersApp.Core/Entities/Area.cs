namespace WorkOrdersApp.Core.Entities;

public class Area
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Relación inversa
    public List<WorkOrder> WorkOrders { get; set; } = new();
}