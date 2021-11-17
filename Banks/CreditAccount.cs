using System;

namespace Banks
{
    public class CreditAccount : IAccount
    {
        private double _money;
        private int _validityPeriod;
        private double _creditLimit;
        private Guid _id;
        private double _commission;

        public CreditAccount(double money, int validityPeriod, double creditLimit, Guid id, double commission)
        {
            _validityPeriod = validityPeriod;
            _id = id;
        }

        public Guid Id => _id;
        public void WithdrawMoneyFromAccount(double money)
        {
            if (money <= 0 || money > _money - _creditLimit)
                throw new ArgumentException("Invalid number of money");
            _money -= money;
        }

        public void PutMoneyIntoAccount(double money)
        {
            if (money <= 0)
                throw new ArgumentException("Invalid number of money");
            _money += money;
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
            _money -= _commission;
        }
    }
}