namespace OPG.Models
{
    public class Item
    {
        public Item(string name, double height, double width, double length, int amount)
        {
            Name = name;
            Height = height;
            Width = width;
            Length = length;
            Amount = amount;
        }

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
