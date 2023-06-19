namespace Shop.OrdersApi.Models;

public class OrderProducts
{
    public uint OrderId { get; set; }
    public ICollection<Order> Order { get; set; }
    public uint ProductId { get; set; }
}