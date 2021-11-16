using System;

namespace Banks
{
    public class CorrespondentAccountCreator : IAccountCreator
    {
        private readonly Guid _id;
        private double _money;

        public CorrespondentAccountCreator(double money)
        {
            if (money <= 0)
                throw new ArgumentException("Invalid number of money");
            _money = money;
            _id = Guid.NewGuid();
        }

        public IAccount Create()
        {
            return new CorrespondentAccount(_money, _id);
        }
    }
}