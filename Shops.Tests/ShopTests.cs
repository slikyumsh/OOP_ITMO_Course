using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shops;
using Shops.Models;

namespace Shops.Tests
{
    public class ShopTests
    {
        private ShopManager manager;
        [SetUp]
        public void Setup()
        {
            manager = new ShopManager();
        }
        
        [Test]
        public void SuccesTryFindProduct()
        {
            Shop shop = new Shop("Volvo");
            manager.Add(shop);
            Product product = new Product("XC70");
            shop.Add(product, new ProductInfo(10, 1001));
            Product result = shop.FindProduct(product);
            Assert.AreEqual(product,result );
        }
        
        [Test]
        public void FailedTryFindProduct()
        {
            Shop shop = new Shop("Volvo");
            manager.Add(shop);
            Product product = new Product("XC70");
            Product fakeProduct = new Product("XC90");
            shop.Add(product, new ProductInfo(10, 1001));
            Product result = shop.FindProduct(fakeProduct);
            Assert.AreEqual(null,result);
        }
        
         [Test]
        public void AddProduct_ProductAddedAndItCanBeBought()
        {
            Shop shop = new Shop("Volvo");
            manager.Add(shop);
            Product product = new Product("XC70");
            shop.Add(product, new ProductInfo(1001, 10));
            ShopListForBuyer shopList = new ShopListForBuyer();
            shopList.Add(product, 2);
            Buyer person = new Buyer(10000);
            manager.Buy(shopList, shop, person);
            Assert.AreEqual(2002, shop.Earning);
        }

         [Test]
        public void AddProduct_SetProductCost()
        {
            ShopManager manager = new ShopManager();
            Shop shop = new Shop("Audi");
            manager.Add(shop);
            Product product = new Product("R8");
            shop.Add(product, new ProductInfo(10010, 100));
            int nowCost = shop.GetProductCost(product);
            Assert.AreEqual(10010, nowCost);
        } 
        
        [Test]
        public void AddProduct_SetProductCost_ChangeCost()
        {
            ShopManager manager = new ShopManager();
            Shop shop = new Shop("Audi");
            manager.Add(shop);
            Product product = new Product("R8");
            shop.Add(product, new ProductInfo(10, 10010));
            shop.ChangeCost(product, 20000);
            int nowCost = shop.GetProductCost(product);
            Assert.AreEqual(20000, nowCost);
        } 
        
        [Test]
        public void FindShopWithMinPrice()
        {
            ShopManager manager = new ShopManager();
            Shop shop1 = new Shop("AudiMoscow");
            Shop shop2 = new Shop("AudiSPb");
            Shop shop3 = new Shop("AudiRostov");
            manager.Add(shop1);
            manager.Add(shop2);
            manager.Add(shop3);
            Product product = new Product("R8");
            shop1.Add(product, new ProductInfo(2, 10000));
            shop2.Add(product, new ProductInfo(3, 5000));
            shop3.Add(product, new ProductInfo(1, 1000));
            ShopListForBuyer shopList = new ShopListForBuyer();
            shopList.Add(product, 1);
            Shop shop = manager.FindShopWithMinPrice(shopList);
            Assert.AreEqual(shop3.Id, shop.Id);
        }
        
        [Test]
        public void CorrectBuyShopList()
        {
            ShopManager manager = new ShopManager();
            Shop shop = new Shop("AudiMoscow");
            
            Product product1 = new Product("R8");
            Product product2 = new Product("Rsi");
            Product product3 = new Product("Q7");
            shop.Add(product1, new ProductInfo(1000, 2));
            shop.Add(product2, new ProductInfo(1500, 3));
            shop.Add(product3, new ProductInfo(8000, 1));
            ShopListForBuyer shopList = new ShopListForBuyer();
            shopList.Add(product1, 1);
            shopList.Add(product3, 1);
            Buyer person = new Buyer(1000000);
            int money = manager.GetPriceForBuyAtThisShop(shopList, shop);
            person.Buy(money);
            shop.Sell(money);
            Assert.AreEqual(9000, shop.Earning);
        }
    }
}