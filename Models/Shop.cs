using System;
using System.Collections.Generic;
using System.Linq;

namespace Shops.Models
{
    public class Shop
    {
        private static int _shopsNumber = 0;
        private int _id;
        private string _name;
        private Dictionary<Product, ProductInfo> _productList;
        private int _earnings;

        public Shop(string name)
        {
            _name = name;
            _id = _shopsNumber;
            _shopsNumber++;
            _productList = new Dictionary<Product, ProductInfo>();
            _earnings = 0;
        }

        public string Name => _name;
        public int Id => _id;
        public Dictionary<Product, ProductInfo> ProductList => _productList;
        public int Earning => _earnings;

        public Product Add(Product product, int quantity, int shopCost)
        {
            if (_productList.Keys.Contains(product))
            {
                _productList[product].IncChangeCount(quantity);
                _productList[product].ChangeCost(shopCost);
            }
            else
            {
                _productList.Add(product, new ProductInfo(shopCost, quantity));
            }

            return product;
        }

        public void Remove(Product product, int quantity)
        {
            if (_productList.Keys.Contains(product))
            {
                if (_productList[product].Count >= quantity)
                {
                    _productList[product].DecChangeCount(quantity);
                }
                else
                {
                    throw new Exception("Not enough products");
                }
            }
            else
            {
                throw new Exception("Haven't product with this name at this shop");
            }
        }

        public Product FindProduct(Product product, int quantity)
        {
            if (_productList.Keys.Contains(product))
                return product;
            else
                return null;
        }

        public Product FindProduct(Product product)
        {
            if (_productList.Keys.Contains(product))
                return product;
            return null;
        }

        public int ProductCost(Product product)
        {
            Product desiredProduct = FindProduct(product);
            if (desiredProduct != null)
                return _productList[product].Cost;
            return 0;
        }

        public bool IsEnoughProduct(Product product, int quantity)
        {
            if (_productList.Keys.Contains(product))
            {
                return quantity <= _productList[product].Count;
            }

            return false;
        }

        public void ChangeCost(Product product, int newCost)
        {
            if (!_productList.Keys.Contains(product))
                throw new Exception("There is no product with this name");
            _productList[product].ChangeCost(newCost);
        }

        public void Sell(int sum)
        {
            _earnings += sum;
        }
    }
}