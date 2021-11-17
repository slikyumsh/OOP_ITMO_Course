using System;

namespace Banks
{
    public class DebitAccountCreator : IAccountCreator
    {
        private readonly Guid _id;
        private double _money;
        private int _validityPeriod;
        private double _procent;

        public DebitAccountCreator(double money, int validityPeriod, double procent)
        {
            if (money <= 0)
                throw new ArgumentException("Invalid number of money");
            if (validityPeriod <= 0)
                throw new ArgumentException("Invalid number of months");
            if (procent <= 0)
                throw new ArgumentException("Invalid procent");
            _money = money;
            _validityPeriod = validityPeriod;
            _procent = procent;
            _id = Guid.NewGuid();
        }

        public IAccount Create()
        {
            return new DebitAccount(_money, _validityPeriod, _procent, _id);
        }
    }
}