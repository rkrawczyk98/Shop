using Microsoft.AspNetCore.Mvc;
using Shop.OrdersApi.Interfaces;
using Shop.OrdersApi.Models;

namespace Shop.OrdersApi.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("CreateOrder")]
        public ActionResult<Order> CreateOrder(Order order)
        {
            if (_orderService.OrderExists(order.OrderId))
            {
                return BadRequest("Order of given Id already exists.");
            }

            var createdOrder = _orderService.CreateOrder(order);
            return Created($"api/orders/{createdOrder.OrderId}", createdOrder);
        }

        [HttpGet("{OrderId}")]
        public ActionResult<Order> GetOrder(uint orderId)
        {
            if (!_orderService.OrderExists(orderId))
            {
                return NotFound("Order with that ID does not exist");
            }
            var order = _orderService.GetOrder(orderId);
            return Ok(order);
        }

        [HttpGet("GetAllOrders")]
        public ActionResult<IEnumerable<Order>> GetAllOrders() => Ok(_orderService.GetAllOrders());

        [HttpPut("Update/{OrderId}")]
        public ActionResult<Order> UpdateOrder(uint orderId, Order order)
        {
            if (orderId != order.OrderId)
            {
                return BadRequest($"orderId parameter does not match order\n{orderId} != {order.OrderId}");
            }

            if (!_orderService.OrderExists(orderId))
            {
                return NotFound($"Order of given Id = {orderId} does not exist.");
            }

            var updatedOrder = _orderService.UpdateOrder(order);
            return Accepted($"api/orders/{order.OrderId}", order);
        }

        [HttpDelete("Delete/{OrderId}")]
        public ActionResult DeleteOrder(uint orderId)
        {
            if (!_orderService.OrderExists(orderId))
                return NotFound($"Order of given ID = {orderId} does not exist.");
            
            _orderService.DeleteOrder(orderId);

            return Ok($"api/orders/{orderId}");
        }
    }
}