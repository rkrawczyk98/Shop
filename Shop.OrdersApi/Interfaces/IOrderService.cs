using Shop.OrdersApi.Models;

namespace Shop.OrdersApi.Interfaces;

public interface IOrderService
{
    Order GetOrder(uint orderId);
    IEnumerable<Order> GetAllOrders();
    void SaveOrder(Order order);
    Order CreateOrder(Order order);
    Order UpdateOrder(Order order);
    bool OrderExists(uint orderId);
    void DeleteOrder(uint orderId);
}