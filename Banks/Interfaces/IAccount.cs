using System;

namespace Banks.Interfaces
{
    public interface IAccount
    {
        public Guid Id { get; }
        public double Money { get; }
        public void PutMoneyIntoAccount(double money);
        public void WithdrawMoneyFromAccount(double money);
    }
}