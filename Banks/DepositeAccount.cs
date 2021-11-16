using System;

namespace Banks
{
    public class DepositeAccount : IAccount
    {
        private double _money;
        private int _months;
        private double _procent;
        private Guid _id;

        public DepositeAccount(double money, int months, double procent, Guid id)
        {
            _months = months;
            _id = id;
        }

        public Guid Id => _id;

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
    }
}