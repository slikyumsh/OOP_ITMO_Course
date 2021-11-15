using System;

namespace Banks
{
    public class DebitAccountCreator : IAccountCreator
    {
        private double _money;
        private int _months;
        private double _procent;

        public DebitAccountCreator(double money, int months, double procent)
        {
            if (money <= 0)
                throw new ArgumentException("Invalid number of money");
            if (months <= 0)
                throw new ArgumentException("Invalid number of months");
            if (procent <= 0)
                throw new ArgumentException("Invalid procent");
            _money = money;
            _months = months;
            _procent = procent;
        }

        public IAccount Create()
        {
            return new DebitAccount(_money, _months, _procent);
        }
    }
}