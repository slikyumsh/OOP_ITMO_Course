using System;

namespace Banks
{
    public interface IAccount
    {
        public Guid Id => Id;
        public void PutMoneyIntoAccount(double money);
        public void WithdrawMoneyFromAccount(double money);
    }
}