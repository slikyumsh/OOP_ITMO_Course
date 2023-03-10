using System;
using Banks.Interfaces;

namespace Banks.Entities
{
    public class CorrespondentAccount : IAccount
    {
        private double _money;
        private Guid _id;
        public CorrespondentAccount(double money)
        {
            _money = money;
            _id = Guid.NewGuid();
        }

        public Guid Id => _id;
        public double Money => _money;

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