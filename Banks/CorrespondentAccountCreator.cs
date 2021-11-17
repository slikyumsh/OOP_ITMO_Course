using System;

namespace Banks
{
    public class CorrespondentAccountCreator : IAccountCreator
    {
        private double _money;

        public CorrespondentAccountCreator(double money)
        {
            if (money <= 0)
                throw new ArgumentException("Invalid number of money");
            _money = money;
        }

        public IAccount Create()
        {
            return new CorrespondentAccount(_money);
        }
    }
}