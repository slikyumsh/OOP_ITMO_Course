using System;

namespace Shops.Models
{
    public class Product
    {
        private readonly string _name;

        public Product(string name)
        {
            if (name == null)
                throw new ArgumentException("Invalid product name");
            _name = name;
        }

        public string Name => _name;

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Product))
            {
                return false;
            }

            return _name == ((Product)obj).Name;
        }

        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }
    }
}