using System;

namespace Banks
{
    public class DepositeAccount : IAccount
    {
        private const double PercentageToFractionConversionFactor = 0.01;
        private int _validityPeriod;
        private double _percent;

        public DepositeAccount(double money, int validityPeriod, double percent, Guid id)
        {
            Money = money;
            _percent = percent;
            _validityPeriod = validityPeriod;
            Id = id;
        }

        public Guid Id { get; }
        public double Money { get; private set; }

        public void PutMoneyIntoAccount(double money)
        {
            if (money <= 0)
                throw new ArgumentException("Invalid number of money");
            Money += money;
        }

        public void ChangePercent(double newPercent)
        {
            if (newPercent <= 0)
                throw new ArgumentException("Invalid percent");
            _percent = newPercent;
        }

        public void CanWithdrawMoneyFromAccount(double money)
        {
            if (money > Money)
                throw new ArgumentException("Not enough money");
        }

        public void WithdrawMoneyFromAccount(double money)
        {
            CanWithdrawMoneyFromAccount(money);
            Money -= money;
        }

        public void PayPercent()
        {
            PutMoneyIntoAccount(Money * _percent * PercentageToFractionConversionFactor / _validityPeriod);
        }
    }
}