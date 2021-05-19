using Microsoft.EntityFrameworkCore;
using OPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPG.Services
{
    public interface IOrderService
    {
        public void AddOrderTask(Order order);
        public Order GetOrder(int orderNr);
        public string GetMinimalBoxSizeForOrder(int orderNr);
    }

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

        public string GetMinimalBoxSizeForOrder(int orderNr)
        {
            //Everything stacked on top of eachother
            ICollection<Item> orderItems = GetOrder(orderNr).Items;
            double totalHeight = orderItems.Sum(item => item.Height);
            Item biggestLength = orderItems.OrderByDescending(item => item.Length).FirstOrDefault();
            Item biggestWidth = orderItems.OrderByDescending(item => item.Width).FirstOrDefault();

            string resultString = "Minimal values for box:";
            resultString += "\nHeight: " + totalHeight.ToString() + "cm";
            resultString += "\nLength: " + biggestLength.Length.ToString() + "cm";
            resultString += "\nWidth: " + biggestWidth.Width.ToString() + "cm";
            return resultString;
        }

        public async Task<Order> AddOrder(Order order)
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
            return order;
        }

        public void AddOrderTask(Order order)
        {
            Task.Run(() => AddOrder(order));
        }

        private double CalculateBoxHeight(ICollection<Item> orderItems)
        {
            return orderItems.Sum(item => item.Height);
        }

        private double CalculateBoxWidth(ICollection<Item> orderItems)
        {
            return orderItems.Max(item => item.Width);
        }

        private double CalculateBoxLength(ICollection<Item> orderItems)
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
