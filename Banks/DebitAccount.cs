using System;

namespace Banks
{
    public class DebitAccount : IAccount
    {
        private double _money;
        private int _months;
        private double _procent;

        public DebitAccount(double money, int months, double procent)
        {
            _months = months;
        }

        public int Months => _months;

        public void WithdrawMoneyFromAccount(int money)
        {
            if (money <= 0 || money > _money)
                throw new ArgumentException("Invalid number of money");
            _money -= money;
        }

        public void PutMoneyIntoAccount(int money)
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
    }
}