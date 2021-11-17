using System;

namespace Banks
{
    public class DebitAccount : IAccount
    {
        private double _money;
        private int _validityPeriod;
        private double _procent;
        private Guid _id;

        public DebitAccount(double money, int validityPeriod, double procent, Guid id)
        {
            _validityPeriod = validityPeriod;
            _id = id;
        }

        public int ValidityPeriod => _validityPeriod;
        public Guid Id => _id;

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

        public void ChangeProcent(double newProcent)
        {
            if (newProcent <= 0)
                throw new ArgumentException("Invalid procent");
            _procent = newProcent;
        }

        public void PayProcent()
        {
            PutMoneyIntoAccount(_money * _procent / _validityPeriod);
        }
    }
}