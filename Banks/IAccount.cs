using System;

namespace Banks
{
    public interface IAccount
    {
        private const double DaysPerMonth = 30;
        public Guid Id => Id;
        public void PutMoneyIntoAccount(double money);
        public void WithdrawMoneyFromAccount(double money);
    }
}