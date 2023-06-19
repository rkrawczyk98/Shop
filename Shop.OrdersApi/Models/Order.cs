using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Shop.OrdersApi.Models;

public class Order
{
    public uint OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime DateOfPlacing { get; set; } = DateTime.Now;
    public DateTime? DateOfCompletion { get; set; }
    public decimal? TotalPrice { get; set; }
    public OrderProducts OrderProducts { get; set; }
}