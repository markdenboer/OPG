using Microsoft.AspNetCore.Mvc;
using OPG.DTO;
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

        [HttpPost("AddOrderFromJson")]
        public async Task ExecuteAddOrder([FromBody] OrderDTO orderDTO)
        {
            _orderService.AddOrderTask(orderDTO);
        }
    }
}