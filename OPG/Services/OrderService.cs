using Hangfire;
using Microsoft.EntityFrameworkCore;
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

        public void AddOrderTask(Order order)
        {
            //BackgroundJob.Enqueue(() => AddOrder(order));
            Task.Run(async () => await AddOrder(order));
        }
        public async Task AddOrder(Order order)
        {     
            for (int i = 0; i < 5; i++)
            {
                Item item = GenerateItem();
                order.Items.Add(item);
            }
            order.BoxHeight = CalculateBoxHeight(order.Items);
            order.BoxWidth = CalculateBoxWidth(order.Items);
            order.BoxLength = CalculateBoxLength(order.Items);

            await OPGDbContext.Orders.AddAsync(order);
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

        private Item GenerateItem()
        {
            Random rng = new();
            Item item = new()
            {
                Name = "Item" + rng.Next(1, 777).ToString(),
                Height = rng.Next(1, 20),
                Width = rng.Next(1, 20),
                Length = rng.Next(1, 20),
                Amount = 1
            };
            return item;
        }
    }
}
