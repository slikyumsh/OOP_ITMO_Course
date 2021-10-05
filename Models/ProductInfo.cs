using System;

namespace Shops.Models
{
    public class ProductInfo
    {
        private int _cost;
        private int _count;

        public ProductInfo(int cost, int count)
        {
            if (cost <= 0)
                throw new ArgumentException("Negative/zero cost of product");
            if (count <= 0)
                throw new ArgumentException("Negative/zero count of product");
            _cost = cost;
            _count = count;
        }

        public int Cost => _cost;
        public int Count => _count;

        public void IncrementCount(int count)
        {
            if (count <= 0)
                throw new ArgumentException("Invalid argument in IncrementCount");
            _count += count;
        }

        public void DecrementCount(int count)
        {
            if (count <= 0)
                throw new ArgumentException("Invalid argument in DecrementCount");
            if (count > _count)
                throw new ArgumentException("Can't decrement count of product : not enough products");
            _count -= count;
        }

        public void ChangeCost(int cost)
        {
            if (cost <= 0)
                throw new Exception("Invalid cost of product");
            _cost = cost;
        }
    }
}