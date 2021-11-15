using System;

namespace Banks
{
    public class DepositeAccount : IAccount
    {
        private double _money;
        private int _months;
        private double _procent;

        public DepositeAccount(double money, int months, double procent)
        {
            _months = months;
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

        public void WithdrawMoneyFromAccount(int money, int time)
        {
            if (time <= 0 || time > _months)
                throw new ArgumentException("Invalid time");
            if (time < _months)
                throw new ArgumentException("We can't give you your money: deposit term is not expiring");
            _money = 0;
        }
    }
}