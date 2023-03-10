using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Interfaces;

namespace Banks.Entities
{
    public class Client : ISubscriber
    {
        private int _id;
        private string _name;
        private string _surname;
        private string _address;
        private string _passport;
        private PhoneNumber _phone;
        private List<IAccount> _accounts;
        private ILogger _logger;

        public Client(string name, string surname, string address, string passport, PhoneNumber phone, int id, ILogger logger)
        {
            _name = name;
            _surname = surname;
            _address = address;
            _passport = passport;
            _phone = phone;
            _id = id;
            _accounts = new List<IAccount>();
            _logger = logger;
        }

        public int Id => _id;
        public string Address => _address;
        public string Passport => _passport;
        public IReadOnlyCollection<IAccount> Accounts => _accounts;

        public void OpenNewAccount(Bank bank, IAccount account)
        {
            if (account == null)
                throw new ArgumentException("Null bank account");
            if (bank == null)
                throw new ArgumentException("Null bank");
            IAccount desiredAccount = _accounts.SingleOrDefault(desiredAccount => desiredAccount.Id == account.Id);
            if (desiredAccount != null)
                throw new ArgumentException("This client already has this account");
            IAccount desiredInBankAccount =
                bank.Accounts.SingleOrDefault(desiredInBankAccount => desiredInBankAccount.Id == account.Id);
            if (desiredInBankAccount != null)
                throw new ArgumentException("This bank already has this account");
            if (account is CorrespondentAccount)
                throw new ArgumentException("Client is not a bank");
            _accounts.Add(account);
            bank.AddClient(this, account);
        }

        public bool DoesThisAccountBelongToThisClient(IAccount account)
        {
            if (account == null)
                throw new ArgumentException("Null bank account");
            IAccount desiredAccount = _accounts.SingleOrDefault(desiredAccount => desiredAccount.Id == account.Id);
            return desiredAccount != null;
        }

        public void GetUpdates(Message message)
        {
            _logger.SendMessage(message);
        }
    }
}