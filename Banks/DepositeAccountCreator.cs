using System;

namespace Banks
{
    public class DepositeAccountCreator : IAccountCreator
    {
        private readonly Guid _id;
        private double _money;
        private int _validityPeriod;
        private double _percent;

        public DepositeAccountCreator(double money, int validityPeriod, double percent)
        {
            if (money <= 0)
                throw new ArgumentException("Invalid number of money");
            if (validityPeriod <= 0)
                throw new ArgumentException("Invalid number of months");
            _money = money;
            _validityPeriod = validityPeriod;
            if (_money < 50000) _percent = 3;
            if (_money >= 50000 && _money < 100000) _percent = 3.5;
            if (_money >= 100000) _percent = 4;
            _id = Guid.NewGuid();
        }

        public IAccount Create()
        {
            return new DepositeAccount(_money, _validityPeriod, _percent, _id);
        }
    }
}