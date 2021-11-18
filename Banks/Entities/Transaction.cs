using System;

namespace Banks
{
    public class Transaction
    {
        private readonly IAccount _sender;
        private readonly IAccount _recipient;
        private readonly double _money;
        private readonly Guid _id;

        public Transaction(IAccount sender, IAccount recipient, double money)
        {
            if (sender == null)
                throw new ArgumentException("Account is null");
            if (recipient == null)
                throw new ArgumentException("Account is null");
            if (recipient.Id == sender.Id)
                throw new ArgumentException("The same account");
            if (money <= 0)
                throw new ArgumentException("Invalid number of money");
            _sender = sender;
            _recipient = recipient;
            _money = money;
            _id = Guid.NewGuid();
        }

        public IAccount Sender => _sender;
        public IAccount Recipient => _recipient;
        public double Money => _money;

        public Transaction ReverseTransaction()
        {
            return new Transaction(_recipient, _sender, _money);
        }

        public void TransferMoney()
        {
            _sender.WithdrawMoneyFromAccount(_money);
            _recipient.PutMoneyIntoAccount(_money);
        }
    }
}