using Microsoft.AspNetCore.Mvc;
using OPG.Interfaces;
using OPG.Models;
using OPG.Services;
using System.ComponentModel.DataAnnotations;

namespace OPG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OPGController : ControllerBase
    {
        private IOrderService _orderService;

        public OPGController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetOrder")]
        public Order ExecuteGetOrder([Required] int orderNumber)
        {
            return _orderService.GetOrder(orderNumber);
        }

        [HttpGet("GetBoxsizeForOrder")]
        public string ExecuteGetBoxsizeForOrder([Required] int orderNumber)
        {
            return _orderService.GetMinimalBoxSizeForOrder(orderNumber);
        }

        [HttpPost("AddOrder")]
        public void ExecuteAddOrder()
        {
            Order order = new();
            _orderService.AddOrderTask(order);
        }
    }
}
