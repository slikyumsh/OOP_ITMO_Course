using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Banks.Interfaces;

namespace Banks.Entities
{
    public class Bank : IObservable
    {
        private static int _numberOfBanks;
        private readonly int _id;
        private readonly string _name;
        private readonly double _limitForNotConfirmedClients;
        private readonly CorrespondentAccount _correspondentAccount;
        private readonly List<Client> _clients;
        private readonly List<ISubscriber> _observers;
        private readonly List<IAccount> _accounts;
        private readonly List<Transaction> _allTransactions;
        private readonly List<Transaction> _canceledTransaions;
        private double _procent;
        private double _comission;

        public Bank(string name, int percent, int commission, int limitForNotConfirmedClients, CorrespondentAccount correspondentAccount)
        {
            _id = _numberOfBanks++;
            _name = name;
            _procent = percent;
            _comission = commission;
            _limitForNotConfirmedClients = limitForNotConfirmedClients;
            _clients = new List<Client>();
            _observers = new List<ISubscriber>();
            _accounts = new List<IAccount>();
            _allTransactions = new List<Transaction>();
            _canceledTransaions = new List<Transaction>();
            _correspondentAccount = correspondentAccount;
        }

        public int Id => _id;
        public IAccount CorrespondentAccount => _correspondentAccount;
        public IReadOnlyList<Client> Clients => _clients;
        public IReadOnlyList<IAccount> Accounts => _accounts;
        public double Money => _correspondentAccount.Money;

        public void SendMessage(Message message, Client client)
        {
            if (message is null)
                throw new ArgumentException("Null message");
            if (client is null)
                throw new ArgumentException("Null client");

            client.GetUpdates(message);
        }

        public Client AddClient(Client client, IAccount account)
        {
            if (client == null)
                throw new ArgumentException("Client is null");
            if (account == null)
                throw new ArgumentException("Account is null");
            Client desiredClient = _clients.SingleOrDefault(desiredClient => desiredClient.Id == client.Id);
            if (desiredClient != null)
                throw new ArgumentException("We already have this client");
            IAccount desiredAccount = _accounts.SingleOrDefault(desiredAccount => desiredAccount.Id == account.Id);
            if (desiredAccount != null)
                throw new ArgumentException("We already have this account");
            _clients.Add(client);
            _accounts.Add(account);
            return client;
        }

        public void AddObserver(Client client)
        {
            ISubscriber desiredClient = _observers.SingleOrDefault(desiredClient => desiredClient.Id == client.Id);
            if (desiredClient != null)
                throw new ArgumentException("This client has already observed");
            foreach (IAccount account in _accounts.Where(client.DoesThisAccountBelongToThisClient))
            {
                _observers.Add(client);
            }

            throw new ArgumentException("Client hasn't any accounts in this bank");
        }

        public void RemoveObserver(Client client)
        {
            ISubscriber desiredClient = _observers.SingleOrDefault(desiredClient => desiredClient.Id == client.Id);
            if (desiredClient == null)
                throw new ArgumentException("This client hasn't observed");
            _observers.Remove(client);
        }

        public void NotifyObservers(Message message)
        {
            foreach (Client observer in _observers)
            {
                SendMessage(message, observer);
            }
        }

        public bool AccountOwnerVerification(IAccount account)
        {
            if (account == null)
                throw new ArgumentException("Account is null");
            IAccount desiredAccount = _accounts.SingleOrDefault(desiredAccount => desiredAccount.Id == account.Id);
            if (desiredAccount == null)
                throw new ArgumentException("We haven't this account");
            return _clients.Where(client => client.DoesThisAccountBelongToThisClient(account)).Any(client => !string.IsNullOrEmpty(client.Address) && !string.IsNullOrEmpty(client.Passport));
        }

        public Client AccountOwner(IAccount account)
        {
            if (account == null)
                throw new ArgumentException("Account is null");
            IAccount desiredAccount = _accounts.SingleOrDefault(desiredAccount => desiredAccount.Id == account.Id);
            if (desiredAccount == null)
                throw new ArgumentException("We haven't this account");
            Client desiredClient =
                _clients.SingleOrDefault(desiredClient => desiredClient.DoesThisAccountBelongToThisClient(account));
            if (desiredClient == null)
                throw new ArgumentException("Nobody has not this account");
            return desiredClient;
        }

        public void MakeTransferWithinOneBank(IAccount sender, IAccount recipient, double money)
        {
            if ((!AccountOwnerVerification(sender) || !AccountOwnerVerification(recipient)) && money > _limitForNotConfirmedClients)
                throw new ArgumentException("Bank transfer denied due to suspicion of fraud1");
            var transactionBankAndAccount1 = new Transaction(sender, _correspondentAccount, money);
            transactionBankAndAccount1.TransferMoney();
            var transactionBankAndAccount2 = new Transaction(_correspondentAccount, recipient, money);
            transactionBankAndAccount2.TransferMoney();
            _allTransactions.Add(transactionBankAndAccount1);
            _allTransactions.Add(transactionBankAndAccount2);
        }

        public void CancelTransferWithinOneBank(Transaction transaction)
        {
            if (_canceledTransaions.Any(item => item == transaction))
                throw new DuplicateNameException("This transaction has been already canceled.");

            transaction.ReverseTransaction();
            var transactionBankAndAccount1 = new Transaction(transaction.Sender, _correspondentAccount, transaction.Money);
            transactionBankAndAccount1.TransferMoney();
            var transactionBankAndAccount2 = new Transaction(_correspondentAccount, transaction.Recipient, transaction.Money);
            transactionBankAndAccount2.TransferMoney();
            _allTransactions.Add(transactionBankAndAccount1);
            _allTransactions.Add(transactionBankAndAccount2);
            _canceledTransaions.Add(transactionBankAndAccount1);
            _canceledTransaions.Add(transactionBankAndAccount2);
        }

        public void ChangePercent(double percent, Message message)
        {
            NotifyObservers(message);
            foreach (IAccount account in _accounts)
            {
                switch (account)
                {
                    case DepositeAccount depositeAccount:
                        depositeAccount.ChangePercent(percent);
                        break;
                    case DebitAccount debitAccount:
                        debitAccount.ChangePercent(percent);
                        break;
                }
            }
        }

        public void ChangeCommission(double commission, Message message)
        {
            NotifyObservers(message);
            foreach (IAccount account in _accounts)
            {
                switch (account)
                {
                    case CreditAccount creditAccount:
                        creditAccount.ChangeCommission(commission);
                        break;
                }
            }
        }

        public void ChangeCreditLimit(double creditLimit, Message message)
        {
            NotifyObservers(message);
            foreach (IAccount account in _accounts)
            {
                switch (account)
                {
                    case CreditAccount creditAccount:
                        creditAccount.ChangeCreditLimit(creditLimit);
                        break;
                }
            }
        }

        public void PayPercents(Message message)
        {
            foreach (IAccount account in _accounts)
            {
                if (AccountOwnerVerification(account))
                {
                    Client client = AccountOwner(account);
                    ISubscriber desiredClient = _observers.SingleOrDefault(desiredClient => desiredClient.Id == client.Id);
                    switch (account)
                    {
                        case DepositeAccount depositeAccount:
                            depositeAccount.PayPercent();
                            SendMessage(message, client);
                            break;
                        case DebitAccount debitAccount:
                            debitAccount.PayPercent();
                            SendMessage(message, client);
                            break;
                    }
                }
            }
        }

        public void CommissionWriteOff(Message message)
        {
            foreach (IAccount account in _accounts)
            {
                Client client = AccountOwner(account);
                ISubscriber desiredClient = _observers.SingleOrDefault(desiredClient => desiredClient.Id == client.Id);
                switch (account)
                    {
                        case CreditAccount creditAccount:
                            creditAccount.CommissionWriteOff();
                            SendMessage(message, client);
                            break;
                    }
            }
        }

        public bool IsTheClientUserOfThisBank(Client client)
        {
            foreach (IAccount account in _accounts)
            {
                if (client.DoesThisAccountBelongToThisClient(account))
                    return true;
            }

            return false;
        }

        public bool IsTheUserAccountBelongsThisBank(IAccount account)
        {
            if (account == null)
                throw new ArgumentException("Account is null");
            if (account is CorrespondentAccount)
                throw new ArgumentException("Bank Account");
            IAccount desiredAccount = _accounts.SingleOrDefault(desiredAccount => desiredAccount.Id == account.Id);
            return !(desiredAccount is null);
        }
    }
}