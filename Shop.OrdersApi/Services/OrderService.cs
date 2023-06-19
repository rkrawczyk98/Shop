using Shop.OrdersApi.Interfaces;
using Shop.OrdersApi.Models;

namespace Shop.OrdersApi.Services;

public class OrderService : IOrderService
{
    public Order GetOrder(uint orderId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Order> GetAllOrders()
    {
        throw new NotImplementedException();
    }

    public void SaveOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public Order CreateOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public Order UpdateOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public bool OrderExists(uint orderId)
    {
        throw new NotImplementedException();
    }

    public void DeleteOrder(uint orderId)
    {
        throw new NotImplementedException();
    }
}