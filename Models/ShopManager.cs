using System;
using System.Collections.Generic;
using System.Linq;

namespace Shops.Models
{
    public class ShopManager
    {
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
            if (_listOfShops.Contains(shop))
                throw new ArgumentException("We have already have this shop at _listOfShops");
            _listOfShops.Add(shop);
            return shop;
        }

        public Product FindProduct(Product product, Shop shop)
        {
            if (product == null)
                throw new ArgumentException("Invalid value of product");
            if (shop == null)
                throw new ArgumentException("Invalid value of shop");
            Shop desiredShop = FindShop(shop.Id);
            if (desiredShop == null)
                throw new ArgumentException("There are not any shop with this name");
            Product desiredProduct = shop.FindProduct(product.Name);
            return desiredProduct;
        }

        public Shop FindShopWithMinPrice(ShopListForBuyer shopList)
        {
            int minSum = int.MaxValue;
            Shop potentialAnswer = null;
            foreach (var shop in _listOfShops)
            {
                int currentSum = 0;
                foreach (var pair in shopList.ShopList)
                {
                    if (!shop.IsEnoughProduct(pair.Key, pair.Value))
                    {
                        continue;
                    }

                    int cost = shop.GetProductCost(pair.Key);
                    currentSum += cost * pair.Value;
                }

                if (minSum > currentSum)
                {
                    minSum = currentSum;
                    potentialAnswer = shop;
                }
            }

            return potentialAnswer;
        }

        public int GetPriceForBuyAtThisShop(ShopListForBuyer shopList, Shop shop)
        {
            int sum = 0;
            foreach (var pair in shopList.ShopList)
            {
                if (!shop.IsEnoughProduct(pair.Key, pair.Value))
                    throw new ArgumentException("Can't buy ShopList at this shop");
                sum += shop.GetProductCost(pair.Key) * pair.Value;
            }

            return sum;
        }

        public Buyer AddBuyer(Buyer person)
        {
            if (FindUser(person) != null)
                throw new ArgumentException("We already have this buyer");
            _users.Add(person);
            return person;
        }

        public void Buy(ShopListForBuyer shopList, Shop shop, Buyer person)
        {
            int result = GetPriceForBuyAtThisShop(shopList, shop);
            person.Buy(result);
            shop.Sell(result);
            foreach (var productFromShoplist in shopList.ShopList)
            {
                shop.Remove(productFromShoplist.Key, productFromShoplist.Value);
            }
        }

        private Buyer FindUser(Buyer person)
        {
            Buyer desiredPerson = _users.SingleOrDefault(desiredPerson => desiredPerson.Id == person.Id);
            return desiredPerson;
        }
    }
}