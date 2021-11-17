using System;

namespace Banks
{
    public class DepositeAccount : IAccount
    {
        private double _money;
        private int _validityPeriod;
        private double _percent;
        private Guid _id;

        public DepositeAccount(double money, int validityPeriod, double percent, Guid id)
        {
            _validityPeriod = validityPeriod;
            _id = id;
        }

        public Guid Id => _id;

        public void PutMoneyIntoAccount(double money)
        {
            if (money <= 0)
                throw new ArgumentException("Invalid number of money");
            _money += money;
        }

        public void ChangePercent(double newPercent)
        {
            if (newPercent <= 0)
                throw new ArgumentException("Invalid percent");
            _percent = newPercent;
        }

        public void CanWithdrawMoneyFromAccount(double money)
        {
            if (money > _money)
                throw new ArgumentException("Not enough money");
        }

        public void WithdrawMoneyFromAccount(double money)
        {
            CanWithdrawMoneyFromAccount(money);
            _money -= money;
        }

        public void PayPercent()
        {
            PutMoneyIntoAccount(_money * _percent / _validityPeriod);
        }
    }
}