using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OPG.Models
{
    public class Order
    {
        public int Id { get; set; }
        public ICollection<Item> Items { get; set; }
        public double BoxWidth { get; set; }
        public double BoxHeight { get; set; }
        public double BoxLength { get; set; }

        public Order()
        {
            Items = new Collection<Item>();
        }
    }
}
