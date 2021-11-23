using System;
using Banks.Interfaces;

namespace Banks.Entities
{
    public class CreditAccount : IAccount
    {
        private const double PercentageToFractionConversionFactor = 0.01;
        private int _validityPeriod;
        private double _creditLimit;
        private double _commission;

        public CreditAccount(double money, int validityPeriod, double creditLimit, Guid id, double commission)
        {
            _commission = commission;
            Money = money;
            _creditLimit = creditLimit;
            _validityPeriod = validityPeriod;
            Id = id;
        }

        public Guid Id { get; }
        public double Money { get; private set; }
        public double Commission => _commission;
        public void WithdrawMoneyFromAccount(double money)
        {
            if (money <= 0 || money > Money - _creditLimit)
                throw new ArgumentException("Invalid number of money");
            Money -= money;
        }

        public void PutMoneyIntoAccount(double money)
        {
            if (money <= 0)
                throw new ArgumentException("Invalid number of money");
            Money += money;
        }

        public void ChangeCreditLimit(double newCreditLimit)
        {
            if (newCreditLimit >= 0)
                throw new ArgumentException("Invalid newCreditLimit");
            _creditLimit = newCreditLimit;
        }

        public void ChangeCommission(double newCommission)
        {
            if (newCommission < 0)
                throw new ArgumentException("Invalid commission");
            _commission = newCommission;
        }

        public void CommissionWriteOff()
        {
            Money -= _commission * Money * PercentageToFractionConversionFactor;
        }
    }
}