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
        public int Earning => _earnings;

        public Product Add(Product product, ProductInfo productInfo)
        {
            if (_productList.Keys.Contains(product))
            {
                _productList[product].IncrementCount(productInfo.Count);
                _productList[product].ChangeCost(productInfo.Cost);
            }
            else
            {
                _productList.Add(product, productInfo);
            }

            return product;
        }

        public void Remove(Product product, int quantity)
        {
            if (!_productList.Keys.Contains(product))
                throw new ArgumentException("Haven't product with this name at this shop");
            if (_productList[product].Count < quantity)
                throw new ArgumentException("Not enough products");
            _productList[product].DecrementCount(quantity);
        }

        public Product FindProduct(string name)
        {
            Product product = new Product(name);
            if (_productList.Keys.Contains(product))
                return product;
            return null;
        }

        public Product FindProduct(Product product)
        {
            if (_productList.Keys.Contains(product))
                return product;
            return null;
        }

        public int GetProductCost(Product product)
        {
            Product desiredProduct = FindProduct(product.Name);
            if (desiredProduct == null)
                throw new ArgumentException("Invalid argument");
            return _productList[product].Cost;
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
                throw new ArgumentException("There is no product with this name");
            _productList[product].ChangeCost(newCost);
        }

        public void Sell(int sum)
        {
            if (sum <= 0)
                throw new ArgumentException("Negative or zero sum");
            _earnings += sum;
        }
    }
}