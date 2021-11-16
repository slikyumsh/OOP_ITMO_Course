﻿using System;

namespace Banks
{
    public class CorrespondentAccount : IAccount
    {
        private double _money;
        private Guid _id;
        public CorrespondentAccount(double money, Guid id)
        {
            _id = id;
        }

        public Guid Id => _id;

        public void WithdrawMoneyFromAccount(double money)
        {
            if (money <= 0 || money > _money)
                throw new ArgumentException("Invalid number of money");
            _money -= money;
        }

        public void PutMoneyIntoAccount(double money)
        {
            if (money <= 0)
                throw new ArgumentException("Invalid number of money");
            _money += money;
        }
    }
}