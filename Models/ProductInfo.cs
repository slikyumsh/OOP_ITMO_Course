using System;

namespace Shops.Models
{
    public class ProductInfo
    {
        private int _cost;
        private int _count;

        public ProductInfo(int cost, int count)
        {
            _cost = cost;
            _count = count;
        }

        public int Cost => _cost;
        public int Count => _count;

        public void IncChangeCount(int count)
        {
            _count += count;
        }

        public void DecChangeCount(int count)
        {
            if (count <= _count)
            {
                _count -= count;
            }
            else
            {
                throw new Exception("Can't minus count of product");
            }
        }

        public void ChangeCost(int cost)
        {
            if (cost >= 0)
            {
                _cost = cost;
            }
            else
            {
                throw new Exception("Incorrect cost of product");
            }
        }
    }
}