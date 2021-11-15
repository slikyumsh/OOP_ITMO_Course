using System;
using System.Collections.Generic;
using System.Linq;

namespace Banks
{
    public class CenterBank
    {
        private List<Bank> _banks;

        public CenterBank()
        {
            _banks = new List<Bank>();
        }

        public void AddBank(Bank bank)
        {
            Bank desiredBank = _banks.SingleOrDefault(desiredBank => desiredBank.Id == bank.Id);
            if (desiredBank != null)
                throw new ArgumentException("We already have this bank");
            _banks.Add(bank);
        }
    }
}