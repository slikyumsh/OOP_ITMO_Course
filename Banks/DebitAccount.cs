using System;

namespace Banks
{
    public class DebitAccount : IAccount
    {
        public const double PercentageToFractionConversionFactor = 0.01;
        private double _money;
        private int _validityPeriod;
        private double _percent;
        private Guid _id;

        public DebitAccount(double money, int validityPeriod, double percent, Guid id)
        {
            _money = money;
            _validityPeriod = validityPeriod;
            _id = id;
            _percent = percent;
        }

        public int ValidityPeriod => _validityPeriod;
        public double Percent => _percent;
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

        public void ChangePercent(double newPercent)
        {
            if (newPercent <= 0)
                throw new ArgumentException("Invalid percent");
            _percent = newPercent;
        }

        public void PayPercent()
        {
            PutMoneyIntoAccount(_money * _percent * PercentageToFractionConversionFactor / _validityPeriod);
        }
    }
}