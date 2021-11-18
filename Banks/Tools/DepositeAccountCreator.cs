using System;

namespace Banks
{
    public class DepositeAccountCreator : IAccountCreator
    {
        private const double AmountMoneyIsFirstBorder = 50000;
        private const double AmountMoneyIsSecondBorder = 100000;
        private const double PercentsAtFirstBorder = 3;
        private const double PercentsAtSecondBorder = 3.5;
        private const double PercentsAtThirdBorder = 4;
        private readonly Guid _id;
        private double _money;
        private int _validityPeriod;
        private double _percent;

        public DepositeAccountCreator(double money, int validityPeriod)
        {
            if (money <= 0)
                throw new ArgumentException("Invalid number of money");
            if (validityPeriod <= 0)
                throw new ArgumentException("Invalid number of months");
            _money = money;
            _validityPeriod = validityPeriod;
            if (_money < AmountMoneyIsFirstBorder) _percent = PercentsAtFirstBorder;
            if (_money >= AmountMoneyIsFirstBorder && _money < AmountMoneyIsSecondBorder) _percent = PercentsAtSecondBorder;
            if (_money >= AmountMoneyIsSecondBorder) _percent = PercentsAtThirdBorder;
            _id = Guid.NewGuid();
        }

        public IAccount Create()
        {
            return new DepositeAccount(_money, _validityPeriod, _percent, _id);
        }
    }
}