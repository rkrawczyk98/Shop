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

        [HttpGet("{orderId}")]
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

        [HttpPut("AddProductToOrder/{orderId}/{productId}")]
        public ActionResult<Order> AddProductToOrder(uint orderId, uint productId)
        {
            if (!_orderService.OrderExists(orderId))
            {
                return NotFound($"Order of given Id = {orderId} does not exist.");
            }

            _orderService.AddProductToOrder(orderId, productId);
            return Accepted($"api/orders/{orderId}/{productId}");
        }
        
        [HttpPut("Update/{orderId}")]
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
            return Accepted($"api/orders/{updatedOrder.OrderId}", updatedOrder);
        }

        [HttpDelete("Delete/{orderId}")]
        public ActionResult DeleteOrder(uint orderId)
        {
            if (!_orderService.OrderExists(orderId))
                return NotFound($"Order of given ID = {orderId} does not exist.");
            
            _orderService.DeleteOrder(orderId);
            return Ok($"api/orders/{orderId}");
        }

        [HttpPut("Complete/{orderId}")]
        public ActionResult CompleteOrder(uint orderId)
        {
            if (!_orderService.OrderExists(orderId))
            {
                return NotFound($"Order of given ID = {orderId} does not exist.");
            }

            var order = _orderService.GetOrder(orderId);
            
            if (order.DateOfCompletion != null)
            {
                return BadRequest($"Order of given ID = {orderId} is already completed.");
            }
            
            _orderService.CompleteOrder(orderId);
            return Accepted($"api/orders/{order.OrderId}");

        }
        
        [HttpPut("Uncomplete/{orderId}")]
        public ActionResult UncompleteOrder(uint orderId)
        {
            if (!_orderService.OrderExists(orderId))
            {
                return NotFound($"Order of given ID = {orderId} does not exist.");
            }

            var order = _orderService.GetOrder(orderId);
            
            if (order.DateOfCompletion == null)
            {
                return BadRequest($"Order of given ID = {orderId} is not completed");
            }

            _orderService.UncompleteOrder(orderId);
            return Accepted($"api/orders/{order.OrderId}");

        }
    }
}