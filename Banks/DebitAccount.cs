using System;

namespace Banks
{
    public class DebitAccount : IAccount
    {
        public const double PercentageToFractionConversionFactor = 0.01;
        private int _validityPeriod;
        private double _percent;

        public DebitAccount(double money, int validityPeriod, double percent, Guid id)
        {
            Money = money;
            _validityPeriod = validityPeriod;
            Id = id;
            _percent = percent;
        }

        public int ValidityPeriod => _validityPeriod;
        public double Percent => _percent;
        public Guid Id { get; }
        public double Money { get; private set; }

        public void WithdrawMoneyFromAccount(double money)
        {
            if (money <= 0 || money > Money)
                throw new ArgumentException("Invalid number of money");
            Money -= money;
        }

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

        public void PayPercent()
        {
            PutMoneyIntoAccount(Money * _percent * PercentageToFractionConversionFactor / _validityPeriod);
        }
    }
}