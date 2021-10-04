namespace Shops.Models
{
    public class Product
    {
        private string _name;

        public Product(string name)
        {
            _name = name;
        }

        public string Name => _name;
    }
}