﻿using System;

namespace Banks
{
    public class DepositeAccountCreator : IAccountCreator
    {
        private double _money;
        private int _months;
        private double _procent;

        public DepositeAccountCreator(double money, int months, double procent)
        {
            if (money <= 0)
                throw new ArgumentException("Invalid number of money");
            if (months <= 0)
                throw new ArgumentException("Invalid number of months");
            _money = money;
            _months = months;
            if (_money < 50000) _procent = 3;
            if (_money >= 50000 && _money < 100000) _procent = 3.5;
            if (_money >= 100000) _procent = 4;
        }

        public IAccount Create()
        {
            return new DepositeAccount(_money, _months, _procent);
        }
    }
}