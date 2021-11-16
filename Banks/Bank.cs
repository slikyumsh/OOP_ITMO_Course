using System;
using System.Collections.Generic;
using System.Linq;

namespace Banks
{
    public class Bank : IObservable
    {
        private readonly Guid _id;
        private readonly string _name;
        private readonly double _limitForNotConfirmedClients;
        private readonly CorrespondentAccount _correspondentAccount;
        private double _procent;
        private double _comission;
        private List<Client> _clients;
        private List<Client> _observers;
        private List<IAccount> _accounts;

        public Bank(string name, int procent, int comission, int limitForNotConfirmedClients, CorrespondentAccount correspondentAccount)
        {
            _id = Guid.NewGuid();
            _name = name;
            _procent = procent;
            _comission = comission;
            _limitForNotConfirmedClients = limitForNotConfirmedClients;
            _clients = new List<Client>();
            _observers = new List<Client>();
            _accounts = new List<IAccount>();
            _correspondentAccount = correspondentAccount;
        }

        public Guid Id => _id;
        public List<Client> Clients => _clients;
        public List<IAccount> Accounts => _accounts;

        public void AddClient(Client client, IAccount account)
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
        }

        public void ChangeProcent(int newProcent)
        {
            if (newProcent < 0)
                throw new ArgumentException("Invalid Procent");
            _procent = newProcent;
        }

        public void ChangeComission(int newComission)
        {
            if (newComission < 0)
                throw new ArgumentException("Invalid Comission");
            _procent = newComission;
        }

        public void AddObserver(Client client)
        {
            Client desiredClient = _observers.SingleOrDefault(desiredClient => desiredClient.Id == client.Id);
            if (desiredClient != null)
                throw new ArgumentException("This client has already observed");
            _observers.Add(client);
        }

        public void RemoveObserver(Client client)
        {
            Client desiredClient = _observers.SingleOrDefault(desiredClient => desiredClient.Id == client.Id);
            if (desiredClient == null)
                throw new ArgumentException("This client hasn't observed");
            _observers.Remove(client);
        }

        public void SendMessage(Message message)
        {
        }

        public void NotifyObservers(Message message)
        {
            foreach (var observer in _observers)
            {
                SendMessage(message);
            }
        }

        public bool AccountOwnerVerification(IAccount account)
        {
            if (account == null)
                throw new ArgumentException("Account is null");
            IAccount desiredAccount = _accounts.SingleOrDefault(desiredAccount => desiredAccount.Id == account.Id);
            if (desiredAccount == null)
                throw new ArgumentException("We haven't this account");
            foreach (var client in _clients)
            {
                if (client.DoesThisAccountBelongToThisClient(account))
                {
                    return !(client.Adress is null || client.Passport is null);
                }
            }

            return true;
        }

        public void MakeTransferWithinOneBank(IAccount account1, IAccount account2, double money)
        {
            if ((AccountOwnerVerification(account1) && AccountOwnerVerification(account2) == false) &&
                money > _limitForNotConfirmedClients)
                throw new ArgumentException("Bank transfer denied due to suspicion of fraud");
            Transaction transaction = new Transaction(account1, account2, money);
            transaction.TransferMoney();
        }

        public void CancelTransferWithinOneBank(IAccount account1, IAccount account2, double money)
        {
            Transaction transaction = new Transaction(account1, account2, money);
            transaction.СancellationTransferMoney();
        }

        public void ChangeProcent(double procent, Message message)
        {
            NotifyObservers(message);
            foreach (var account in _accounts)
            {
                switch (account)
                {
                    case DepositeAccount depositeAccount:
                        depositeAccount.ChangeProcent(procent);
                        break;
                    case DebitAccount debitAccount:
                        debitAccount.ChangeProcent(procent);
                        break;
                }
            }
        }

        public void ChangeCommission(double commission, Message message)
        {
            NotifyObservers(message);
            foreach (var account in _accounts)
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
            foreach (var account in _accounts)
            {
                switch (account)
                {
                    case CreditAccount creditAccount:
                        creditAccount.ChangeCreditLimit(creditLimit);
                        break;
                }
            }
        }
    }
}