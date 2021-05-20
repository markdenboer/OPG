using System.Collections.Generic;

namespace OPG.DTO
{
    public class OrderDTO
    {
        public string OrderNumber { get; set; }
        public ICollection<ItemDTO> Items { get; set; }
    }
}
