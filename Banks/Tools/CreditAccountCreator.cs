using System;
using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Tools
{
    public class CreditAccountCreator : IAccountCreator
    {
        private readonly Guid _id;
        private double _money;
        private int _validityPeriod;
        private double _creditLimit;
        private double _commission;

        public CreditAccountCreator(double money, int validityPeriod, double creditLimit, double commission)
        {
            if (money <= 0)
                throw new ArgumentException("Invalid number of money");
            if (validityPeriod <= 0)
                throw new ArgumentException("Invalid number of months");
            if (_creditLimit > 0)
                throw new ArgumentException("Invalid credit limit, it should be negative");
            if (_commission < 0)
                throw new ArgumentException("Invalid commission, it should not be negative");
            _money = money;
            _validityPeriod = validityPeriod;
            _creditLimit = creditLimit;
            _commission = commission;
            _id = Guid.NewGuid();
        }

        public IAccount Create()
        {
            return new CreditAccount(_money, _validityPeriod, _creditLimit, _id, _commission);
        }
    }
}