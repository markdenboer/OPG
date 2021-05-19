namespace OPG.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public int Amount { get; set; }
        
        public Order Order { get; set; }
        public int OrderId { get; set; }

    }
}
