using Hangfire;
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
        private readonly IOrderService _orderService;

        public OPGController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetOrder")]
        public async Task<Order> ExecuteGetOrder([Required] string orderNumber)
        {
            return await _orderService.GetOrder(orderNumber);
        }

        [HttpPost("AddOrderFromJson")]
        public void ExecuteAddOrder([FromBody] OrderDTO orderDTO)
        {
            BackgroundJob.Enqueue(() => _orderService.AddOrderTaskWithJSON(orderDTO));
        }
    }
}