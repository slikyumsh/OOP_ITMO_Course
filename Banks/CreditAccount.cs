using System;

namespace Banks
{
    public class CreditAccount : IAccount
    {
        private double _money;
        private int _months;
        private double _creditLimit;

        public CreditAccount(double money, int months, double creditLimit)
        {
            _months = months;
        }

        public void WithdrawMoneyFromAccount(int money)
        {
            if (money <= 0 || money > _money - _creditLimit)
                throw new ArgumentException("Invalid number of money");
            _money -= money;
        }

        public void PutMoneyIntoAccount(int money)
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
    }
}