using System;

namespace Banks
{
    public class DebitAccountCreator : IAccountCreator
    {
        private readonly Guid _id;
        private double _money;
        private int _validityPeriod;
        private double _percent;

        public DebitAccountCreator(double money, int validityPeriod, double percent)
        {
            if (money <= 0)
                throw new ArgumentException("Invalid number of money");
            if (validityPeriod <= 0)
                throw new ArgumentException("Invalid number of months");
            if (percent <= 0)
                throw new ArgumentException("Invalid percent");
            _money = money;
            _validityPeriod = validityPeriod;
            _percent = percent;
            _id = Guid.NewGuid();
        }

        public IAccount Create()
        {
            return new DebitAccount(_money, _validityPeriod, _percent, _id);
        }
    }
}