using System;
using System.Collections.Generic;
using System.Linq;

namespace Banks
{
    public class Bank : IObservable
    {
        private static int _numberOfBanks;
        private readonly int _id;
        private readonly string _name;
        private readonly double _limitForNotConfirmedClients;
        private readonly CorrespondentAccount _correspondentAccount;
        private double _procent;
        private double _comission;
        private List<Client> _clients;
        private List<Client> _observers;
        private List<IAccount> _accounts;

        public Bank(string name, int percent, int commission, int limitForNotConfirmedClients, CorrespondentAccount correspondentAccount)
        {
            _id = _numberOfBanks++;
            _name = name;
            _procent = percent;
            _comission = commission;
            _limitForNotConfirmedClients = limitForNotConfirmedClients;
            _clients = new List<Client>();
            _observers = new List<Client>();
            _accounts = new List<IAccount>();
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
            Client desiredClient = _observers.SingleOrDefault(desiredClient => desiredClient.Id == client.Id);
            if (desiredClient != null)
                throw new ArgumentException("This client has already observed");
            foreach (IAccount account in _accounts)
            {
                if (client.DoesThisAccountBelongToThisClient(account))
                {
                    _observers.Add(client);
                    return;
                }
            }

            throw new ArgumentException("Client hasn't any accounts in this bank");
        }

        public void RemoveObserver(Client client)
        {
            Client desiredClient = _observers.SingleOrDefault(desiredClient => desiredClient.Id == client.Id);
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
            foreach (Client client in _clients)
            {
                if (client.DoesThisAccountBelongToThisClient(account))
                {
                    if (client.Address != null && client.Passport != null)
                        return true;
                }
            }

            return false;
        }

        public void MakeTransferWithinOneBank(IAccount account1, IAccount account2, double money)
        {
            if ((!AccountOwnerVerification(account1) || !AccountOwnerVerification(account2)) && money > _limitForNotConfirmedClients)
                throw new ArgumentException("Bank transfer denied due to suspicion of fraud1");
            var transactionBankAndAccount1 = new Transaction(account1, _correspondentAccount, money);
            transactionBankAndAccount1.TransferMoney();
            var transactionBankAndAccount2 = new Transaction(_correspondentAccount, account2, money);
            transactionBankAndAccount2.TransferMoney();
        }

        public void CancelTransferWithinOneBank(Transaction transaction)
        {
            transaction.ReverseTransaction();
            var transactionBankAndAccount1 = new Transaction(transaction.Sender, _correspondentAccount, transaction.Money);
            transactionBankAndAccount1.TransferMoney();
            var transactionBankAndAccount2 = new Transaction(_correspondentAccount, transaction.Recipient, transaction.Money);
            transactionBankAndAccount2.TransferMoney();
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
            NotifyObservers(message);
            foreach (IAccount account in _accounts)
            {
                if (AccountOwnerVerification(account))
                {
                    switch (account)
                    {
                        case DepositeAccount depositeAccount:
                            depositeAccount.PayPercent();
                            break;
                        case DebitAccount debitAccount:
                            debitAccount.PayPercent();
                            break;
                    }
                }
            }
        }

        public void CommissionWriteOff(Message message)
        {
            NotifyObservers(message);
            foreach (IAccount account in _accounts)
            {
                switch (account)
                    {
                        case CreditAccount creditAccount:
                            creditAccount.CommissionWriteOff();
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