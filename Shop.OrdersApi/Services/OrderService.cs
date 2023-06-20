using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http.HttpResults;
using Shop.OrdersApi.Data;
using Shop.OrdersApi.Interfaces;
using Shop.OrdersApi.Models;

namespace Shop.OrdersApi.Services;

public class OrderService : IOrderService
{

    private readonly AppDbContext _appDbContext;

    public OrderService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public void CompleteOrder(uint orderId)
    {
        var order = GetOrder(orderId);
        order.DateOfCompletion = DateTime.Now;
        _appDbContext.Update(order);
        _appDbContext.SaveChanges();
    }

    public void UncompleteOrder(uint orderId)
    {
        var order = GetOrder(orderId);
        order.DateOfCompletion = null;
        _appDbContext.Update(order);
        _appDbContext.SaveChanges();
    }

    public Order GetOrder(uint orderId)
    {
        if (!OrderExists(orderId))
        {
            throw new Exception($"Order of given Id = {orderId} does not exist!");
        }
        return _appDbContext.Orders.FirstOrDefault(o => orderId == o.OrderId);
    }

    public IEnumerable<Order> GetAllOrders() => _appDbContext.Orders;

    public Order CreateOrder(Order order)
    {
        if (OrderExists(order.OrderId))
        {
            throw new Exception($"Order of given Id = {order.OrderId} already exists!");
        }
            
        string pattern = @"^\d+(,\d+)*$"; // input = "2,3,4,1,5,2,3,4"
        
        if (!(order.OrderProducts == "") && !(Regex.IsMatch(order.OrderProducts, pattern)))
        {
            throw new Exception($"OrderProducts does not match regular expression formula e.g. \"3,4,1,2,5\" ");
        }
        _appDbContext.Add(order);
        _appDbContext.SaveChanges();
        return order;
    }

    public void AddProductToOrder(uint orderId, uint productId)
    {
        if (!OrderExists(orderId))
        {
            throw new Exception($"Order of given Id = {orderId} does not exist!");
        }
        var order = GetOrder(orderId);
        if (order.OrderProducts == "" || order.OrderProducts is null)
            order.OrderProducts = productId.ToString();
        else
            order.OrderProducts = order.OrderProducts?.Trim() + "," + productId.ToString();
        
        
        _appDbContext.Update(order);
        _appDbContext.SaveChanges();
    }

    public Order UpdateOrder(Order order)
    {
        if (!OrderExists(order.OrderId))
        {
            throw new Exception($"Order of given Id = {order.OrderId} does not exist!");
        }
        var existingOrder = GetOrder(order.OrderId);
        if (existingOrder.DateOfCompletion is not null)
        {
            throw new Exception("Cannot edit completed order.");
        }

        if (existingOrder == null)
        {
            throw new Exception("Order of that Id is null");
        }

        existingOrder.OrderProducts = order.OrderProducts;
        existingOrder.DateOfCompletion = existingOrder.DateOfCompletion;
        _appDbContext.Update(existingOrder);
        _appDbContext.SaveChanges();
        return existingOrder;
    }

    public bool OrderExists(uint orderId)
    {
        return _appDbContext.Orders.Any(o => o.OrderId == orderId);
    }

    public void DeleteOrder(uint orderId)
    {
        if (!OrderExists(orderId))
        {
            throw new Exception($"Order of given Id = {orderId} does not exist");
        }

        _appDbContext.Orders.Remove(GetOrder(orderId));
        _appDbContext.SaveChanges();
    }
}