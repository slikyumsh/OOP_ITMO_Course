using System;
using System.Collections.Generic;
using System.Linq;

namespace Banks
{
    public class CenterBank
    {
        private const int DaysPerMonth = 30;
        private List<Bank> _banks;
        private List<Client> _clients;

        public CenterBank()
        {
            _banks = new List<Bank>();
            _clients = new List<Client>();
        }

        public IReadOnlyList<Bank> Banks => _banks;
        public IReadOnlyList<Client> Clients => _clients;

        public Bank AddBank(Bank bank)
        {
            Bank desiredBank = _banks.SingleOrDefault(desiredBank => desiredBank.Id == bank.Id);
            if (desiredBank != null)
                throw new ArgumentException("We already have this bank");
            _banks.Add(bank);
            return bank;
        }

        public Client AddClient(Client client)
        {
            Client desiredClient = _clients.SingleOrDefault(desiredClient => desiredClient.Id == client.Id);
            if (desiredClient != null)
                throw new ArgumentException("We already have this bank");
            _clients.Add(client);
            return client;
        }

        public void PayPercents(Message message)
        {
            foreach (Bank bank in _banks)
            {
                bank.PayPercents(message);
            }
        }

        public void CommissionWriteOff(Message message)
        {
            if (message is null)
                throw new ArgumentException("Null message");
            foreach (Bank bank in _banks)
            {
                bank.CommissionWriteOff(message);
            }
        }

        public void ChangePercent(Bank bank, double newPercent, Message message)
        {
            if (message is null)
                throw new ArgumentException("Null message");
            if (bank is null)
                throw new ArgumentException("Null bank");
            if (newPercent < 0)
                throw new ArgumentException("Invalid percent");
            Bank desiredBank = _banks.SingleOrDefault(desiredBank => desiredBank.Id == bank.Id);
            if (desiredBank == null)
                throw new ArgumentException("Center Bank hasn't any banks with this Bank-Id");
            bank.ChangePercent(newPercent, message);
        }

        public void ChangeCommission(Bank bank, double newCommission, Message message)
        {
            if (message is null)
                throw new ArgumentException("Null message");
            if (bank is null)
                throw new ArgumentException("Null bank");
            if (newCommission < 0)
                throw new ArgumentException("Invalid commission");
            Bank desiredBank = _banks.SingleOrDefault(desiredBank => desiredBank.Id == bank.Id);
            if (desiredBank == null)
                throw new ArgumentException("Center Bank hasn't any banks with this Bank-Id");
            bank.ChangeCommission(newCommission, message);
        }

        public void BankTransfersMoney(Bank bank1, Bank bank2, double money)
        {
            var transaction = new Transaction(bank1.CorrespondentAccount, bank2.CorrespondentAccount, money);
            transaction.TransferMoney();
        }

        public void TransferMoney(IAccount account1, IAccount account2, double money)
        {
            if (account1 == null || account2 == null)
                throw new ArgumentException("One client is null");
            if (account1 is CorrespondentAccount && account2 is CorrespondentAccount)
            {
                Bank desiredBank1 =
                    _banks.SingleOrDefault(desiredBank1 => desiredBank1.CorrespondentAccount.Id == account1.Id);
                Bank desiredBank2 =
                    _banks.SingleOrDefault(desiredBank2 => desiredBank2.CorrespondentAccount.Id == account2.Id);
                if (desiredBank1 == null || desiredBank2 == null)
                    throw new ArgumentException("There is no bank, that contains this account");
                BankTransfersMoney(desiredBank1, desiredBank2, money);
                return;
            }

            Bank desiredBank3 = _banks.SingleOrDefault(desiredBank3 => desiredBank3.IsTheUserAccountBelongsThisBank(account1));
            if (desiredBank3 == null)
                throw new ArgumentException("There is no bank, that contains this account");
            Bank desiredBank4 = _banks.SingleOrDefault(desiredBank4 => desiredBank4.IsTheUserAccountBelongsThisBank(account2));
            if (desiredBank4 == null)
                throw new ArgumentException("There is no bank, that contains this account");
            if (desiredBank3.Id == desiredBank4.Id)
            {
                desiredBank3.MakeTransferWithinOneBank(account1, account2, money);
                return;
            }

            var transactionBankAndAccount1 =
                new Transaction(account1, desiredBank3.CorrespondentAccount, money);
            transactionBankAndAccount1.TransferMoney();
            BankTransfersMoney(desiredBank3, desiredBank4, money);
            var transactionBankAndAccount2 =
                new Transaction(desiredBank4.CorrespondentAccount, account2, money);
            transactionBankAndAccount2.TransferMoney();
        }

        public void TransferMoney(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentException("Transaction is null");
            TransferMoney(transaction.Sender, transaction.Recipient, transaction.Money);
        }

        public void CancellationOfTransaction(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentException("Transaction is NULL");
            TransferMoney(transaction.ReverseTransaction());
        }

        public void ModelingWorkOfTheBankingSystemAfterCertainNumberOfDays(int days, Message commissionMessage, Message percentsMessage)
        {
            if (commissionMessage is null)
                throw new ArgumentException("Null message");
            if (percentsMessage is null)
                throw new ArgumentException("Null message");
            if (days <= 0)
                throw new ArgumentException("Too few days");
            for (int i = 1; i <= days; i++)
            {
                if (i % DaysPerMonth == 0)
                    CommissionWriteOff(commissionMessage);
                PayPercents(percentsMessage);
            }
        }

        public Bank GetBankById(int number)
        {
            if (number < 0)
                throw new ArgumentException("Invalid number of bank");
            Bank desiredBank = _banks.SingleOrDefault(desiredBank => desiredBank.Id == number);
            if (desiredBank == null)
                throw new ArgumentException("We can't find this client");
            return desiredBank;
        }

        public Client GetClientById(int number)
        {
            if (number < 0)
                throw new ArgumentException("Invalid number");
            Client desiredClient = _clients.SingleOrDefault(desiredClient => desiredClient.Id == number);
            if (desiredClient == null)
                throw new ArgumentException("We can't find this client");
            return desiredClient;
        }
    }
}