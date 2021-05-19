using Microsoft.AspNetCore.Mvc;
using OPG.Interfaces;
using OPG.Models;
using OPG.Services;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

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

        [HttpPost("AddOrder")]
        public async Task ExecuteAddOrder()
        {
            Order order = new();
            await _orderService.AddOrder(order);
        }
    }
}
