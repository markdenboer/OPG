using Hangfire;
using Microsoft.EntityFrameworkCore;
using OPG.DTO;
using OPG.Interfaces;
using OPG.Models;
using System;
using System.Collections.Generic;
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
        public Order GetOrder(int orderNr)
        {
            return OPGDbContext.Orders.Include(order => order.Items).FirstOrDefault(order => order.Id == orderNr);
        }

        public void AddOrderTask(OrderDTO order)
        {
            BackgroundJob.Enqueue(() => AddOrderTaskWithJSON(order));
        }

        public async Task AddOrderTaskWithJSON(OrderDTO order)
        {
            Order newOrder = new();
            newOrder.OrderNumber = order.OrderNumber;
            newOrder.Items = order.Items.Select(itemDTO => new Item(itemDTO.Name, itemDTO.Height, itemDTO.Width, itemDTO.Length, itemDTO.Amount)).ToList();    
            newOrder.BoxHeight = CalculateBoxHeight(newOrder.Items);
            newOrder.BoxWidth = CalculateBoxWidth(newOrder.Items);
            newOrder.BoxLength = CalculateBoxLength(newOrder.Items);

            await OPGDbContext.Orders.AddAsync(newOrder);
            await OPGDbContext.SaveChangesAsync();
        }

        private static double CalculateBoxHeight(ICollection<Item> orderItems)
        {
            return orderItems.Sum(item => item.Height);
        }

        private static double CalculateBoxWidth(ICollection<Item> orderItems)
        {
            return orderItems.Max(item => item.Width);
        }

        private static double CalculateBoxLength(ICollection<Item> orderItems)
        {
            return orderItems.Max(item => item.Length);
        }
    }
}
