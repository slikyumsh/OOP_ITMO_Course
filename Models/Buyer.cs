using System;
using System.Collections.Generic;

namespace Shops.Models
{
    public class Buyer
    {
        private static int _id = 0;
        private int _money;
        public Buyer(int money)
        {
            if (money < 0)
                throw new ArgumentException("Negative money");
            _money = money;
            _id++;
        }

        public int Id => _id;
        public void Buy(int sum)
        {
            if (_money < sum)
                throw new ArgumentException("Not enough money");
            _money -= sum;
        }
    }
}