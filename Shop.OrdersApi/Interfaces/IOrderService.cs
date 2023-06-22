using Shop.OrdersApi.Models;

namespace Shop.OrdersApi.Interfaces;

public interface IOrderService
{
    Order CreateOrder(Order order);
    void AddProductToOrder(uint orderId, uint productId);
    void CompleteOrder(uint orderId);
    void UncompleteOrder(uint orderId);
    Order GetOrder(uint orderId);
    IEnumerable<Order> GetAllOrders();
    Order UpdateOrder(Order order);
    void DeleteOrder(uint orderId);
    bool OrderExists(uint orderId);
}