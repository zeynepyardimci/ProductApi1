namespace ProductApi.Models
{
    public class Product : IComparable<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double Price { get; set; }
        public Product() // parametresiz constructor
        {
            Name = string.Empty;
        }
        public Product(string name)
        {
            Name = name;
        }

        public int CompareTo(Product? other)
        {
            if (other == null) return 1;
            return Price.CompareTo(other.Price);
        }
        public override string ToString()
        {
            return $"{Name} - {Price:C}";
        }
    }
}
