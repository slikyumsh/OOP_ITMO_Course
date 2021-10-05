using System;
using System.Collections.Generic;
using System.Linq;

namespace Shops.Models
{
    public class ShopListForBuyer
    {
        private Dictionary<Product, int> _shopListForBuyer;

        public ShopListForBuyer()
        {
            _shopListForBuyer = new Dictionary<Product, int>();
        }

        public Dictionary<Product, int> ShopList => _shopListForBuyer;

        public void Add(Product product, int quantity)
        {
            if (product == null)
                throw new ArgumentException("Null value of product");
            if (quantity <= 0)
                throw new ArgumentException("Quantity <= 0");
            if (_shopListForBuyer.Keys.Contains(product))
                throw new ArgumentException("We have already chosen this product");
            _shopListForBuyer.Add(product, quantity);
        }
    }
}