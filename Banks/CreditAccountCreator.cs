using System;

namespace Banks
{
    public class CreditAccountCreator : IAccountCreator
    {
        private double _money;
        private int _months;
        private double _creditLimit;

        public CreditAccountCreator(double money, int months, double creditLimit)
        {
            if (money <= 0)
                throw new ArgumentException("Invalid number of money");
            if (months <= 0)
                throw new ArgumentException("Invalid number of months");
            if (_creditLimit > 0)
                throw new ArgumentException("Invalid credit limit, it should be negative");
            _money = money;
            _months = months;
            _creditLimit = creditLimit;
        }

        public IAccount Create()
        {
            return new CreditAccount(_money, _months, _creditLimit);
        }
    }
}