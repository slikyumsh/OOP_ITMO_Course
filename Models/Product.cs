using System;

namespace Shops.Models
{
    public class Product : IEquatable<Product>
    {
        private string _name;

        public Product(string name)
        {
            if (name == null)
                throw new ArgumentException("Invalid product name");
            _name = name;
        }

        public string Name => _name;

        public static bool operator ==(Product product1, Product product2)
        {
            if (product1 is not null)
                return product1.Equals(product2);
            if (product2 is null)
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(Product a, Product b) => !(a == b);

        public bool Equals(Product objProduct)
        {
            if (objProduct == null)
                return false;

            return objProduct.Name == _name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_name);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}