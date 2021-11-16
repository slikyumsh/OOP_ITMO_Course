using System;
using System.Collections.Generic;
using System.Linq;

namespace Banks
{
    public class CenterBank
    {
        private List<Bank> _banks;
        private List<Client> _clients;

        public CenterBank()
        {
            _banks = new List<Bank>();
            _clients = new List<Client>();
        }

        public void AddBank(Bank bank)
        {
            Bank desiredBank = _banks.SingleOrDefault(desiredBank => desiredBank.Id == bank.Id);
            if (desiredBank != null)
                throw new ArgumentException("We already have this bank");
            _banks.Add(bank);
        }

        public void AddClient(Client client)
        {
            Client desiredClient = _clients.SingleOrDefault(desiredClient => desiredClient.Id == client.Id);
            if (desiredClient != null)
                throw new ArgumentException("We already have this bank");
            _clients.Add(client);
        }
    }
}