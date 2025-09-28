using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechnicalTask.BusinessLogic.DTOs.OrderDtos;
using TechnicalTask.BusinessLogic.Interfaces.IServices;
using TechnicalTask.BusinessLogic.Services;

namespace TechnicalTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService orderService;


        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            var result = orderService.GetAll();
            return result.IsFail ? BadRequest(result) : Ok(result);
        }

        [HttpGet("GetOrderById/{id}")]
        public IActionResult GetOrderById(int id)
        {
            var result = orderService.GetById(id);
            return result.IsFail ? BadRequest(result) : Ok(result);
        }
        [HttpPost("AddOrder")]
        public IActionResult AddOrder([FromBody] CreateOrderDto orderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (!int.TryParse(HttpContext.User.FindFirst("UserId")?.Value, out int customerId))
                return BadRequest("Invalid Customer");
            orderDto.CustomerId = customerId;
            var result = orderService.Add(orderDto);
            return result.IsFail ? BadRequest(result) : Ok(result);
        }


        [HttpGet("GetOrdersByCustomerId")]
        public IActionResult GetOrdersByCustomerId()
        {
            if (!int.TryParse(HttpContext.User.FindFirst("UserId")?.Value, out int customerId))
                return BadRequest("Invalid Customer");
            var result = orderService.GetOrdersByCustomerId(customerId);
            return result.IsFail ? BadRequest(result) : Ok(result);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (!int.TryParse(HttpContext.User.FindFirst("UserId")?.Value, out int customerId))
                return BadRequest("Invalid Customer");
            var result = orderService.Delete(id, customerId);
            return result.Result.IsFail ? BadRequest(result.Result) : Ok(result.Result);
        }

    }
}
