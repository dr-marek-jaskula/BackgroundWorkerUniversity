namespace BackgroundWorker;

public class OrderInfo
{
    public Guid OrderId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public int QuantityOrderded { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
}

