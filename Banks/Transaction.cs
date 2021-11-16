﻿using System;

namespace Banks
{
    public class Transaction
    {
        private IAccount _sender;
        private IAccount _recipient;
        private double _money;
        private Guid _id;

        public Transaction(IAccount sender, IAccount recipient, double money)
        {
            if (sender == null)
                throw new ArgumentException("Account is null");
            if (recipient == null)
                throw new ArgumentException("Account is null");
            if (money <= 0)
                throw new ArgumentException("Invalid number of money");
            _sender = sender;
            _recipient = recipient;
            _money = money;
            _id = Guid.NewGuid();
        }

        public Transaction ReverseTransaction()
        {
            return new Transaction(_recipient, _sender, _money);
        }
    }
}