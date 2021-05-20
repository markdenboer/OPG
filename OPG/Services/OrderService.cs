using Hangfire;
using Microsoft.EntityFrameworkCore;
using OPG.DTO;
using OPG.Interfaces;
using OPG.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OPG.Services
{
    public class OrderService : IOrderService
    {
        private OPGDbContext OPGDbContext { get; set; }
        public OrderService(OPGDbContext dbContext)
        {
            OPGDbContext = dbContext;
        }
        public async Task<Order> GetOrder(string orderNumber)
        {
            return await OPGDbContext.Orders.Include(order => order.Items).FirstOrDefaultAsync(order => order.OrderNumber == orderNumber);
        }

        public async Task AddOrderTaskWithJSON(OrderDTO order)
        {
            Order newOrder = new();
            newOrder.OrderNumber = order.OrderNumber;
            newOrder.Items = order.Items.Select(itemDTO => new Item(itemDTO.Name, itemDTO.Height, itemDTO.Width, itemDTO.Length, itemDTO.Amount)).ToList();    
            newOrder.BoxHeight = newOrder.Items.Sum(item => item.Height);
            newOrder.BoxWidth = newOrder.Items.Max(item => item.Width);
            newOrder.BoxLength = newOrder.Items.Max(item => item.Length);

            await OPGDbContext.Orders.AddAsync(newOrder);
            await OPGDbContext.SaveChangesAsync();
        }
    }
}
