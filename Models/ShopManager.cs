using System;
using System.Collections.Generic;
using System.Linq;

namespace Shops.Models
{
    public class ShopManager
    {
        private const int MaxValue = 1000000000;
        private List<Shop> _listOfShops;
        private List<Buyer> _users;

        public ShopManager()
        {
            _listOfShops = new List<Shop>();
            _users = new List<Buyer>();
        }

        public List<Shop> ListShops => _listOfShops;

        public Shop FindShop(int id)
        {
            Shop desiredShop = _listOfShops.SingleOrDefault(desiredShop => desiredShop.Id == id);
            return desiredShop;
        }

        public Shop Add(Shop shop)
        {
            if (!_listOfShops.Contains(shop))
            {
                _listOfShops.Add(shop);
                return shop;
            }

            throw new Exception("We have already have this shop at _listOfShops");
        }

        public Shop FindShop(Shop shop)
        {
            Shop desiredShop = FindShop(shop.Id);
            return desiredShop;
        }

        public Product FindProduct(Product product, Shop shop)
        {
            Shop desiredShop = FindShop(shop);
            if (desiredShop == null)
                return null;
            Product desiredProduct = shop.FindProduct(product, 1);
            return desiredProduct;
        }

        public Shop FindShopWithMinPrice(Dictionary<Product, int> shopList)
        {
            int minSum = MaxValue;
            Shop potentialAnswer = null;
            foreach (var shop in _listOfShops)
            {
                int cnt = 0;
                int currentSum = 0;
                foreach (var pair in shopList)
                {
                    if (shop.IsEnoughProduct(pair.Key, pair.Value))
                    {
                        cnt++;
                        int cost = shop.ProductCost(pair.Key);
                        currentSum += cost * pair.Value;
                    }
                }

                if (cnt == shopList.Count)
                {
                    if (minSum > currentSum)
                    {
                        minSum = currentSum;
                        potentialAnswer = shop;
                    }
                }
            }

            return potentialAnswer;
        }

        public int PriceForBuyAtThisShop(Dictionary<Product, int> shopList, Shop shop)
        {
            int sum = 0;
            foreach (var pair in shopList)
            {
                if (!shop.IsEnoughProduct(pair.Key, pair.Value))
                    return -1;
                sum += shop.ProductCost(pair.Key) * pair.Value;
            }

            return sum;
        }

        public Buyer AddBuyer(Buyer person)
        {
            if (FindUser(person) == null)
                _users.Add(person);
            return FindUser(person);
        }

        public void Buy(Dictionary<Product, int> shopList, Shop shop, Buyer person)
        {
            int result = PriceForBuyAtThisShop(shopList, shop);
            if (result == -1)
                throw new Exception("Can't buy these products at this shop");
            person.Buy(result);
            shop.Sell(result);
        }

        private Buyer FindUser(Buyer person)
        {
            Buyer desiredPerson = _users.SingleOrDefault(desiredPerson => desiredPerson.Id == person.Id);
            return desiredPerson;
        }
    }
}