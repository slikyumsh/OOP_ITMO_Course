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

        public void PayProcents(Message message)
        {
            foreach (var bank in _banks)
            {
                bank.PayProcents(message);
            }
        }

        public void CommissionWriteOff(Message message)
        {
            foreach (var bank in _banks)
            {
                bank.CommissionWriteOff(message);
            }
        }

        public void ChangeProcent(Bank bank, double newProcent, Message message)
        {
            Bank desiredBank = _banks.SingleOrDefault(desiredBank => desiredBank.Id == bank.Id);
            if (desiredBank == null)
                throw new ArgumentException("Center Bank hasn't any banks with this Bank-Id");
            bank.ChangeProcent(newProcent, message);
        }

        public void ChangeCommission(Bank bank, double newCommission, Message message)
        {
            Bank desiredBank = _banks.SingleOrDefault(desiredBank => desiredBank.Id == bank.Id);
            if (desiredBank == null)
                throw new ArgumentException("Center Bank hasn't any banks with this Bank-Id");
            bank.ChangeProcent(newCommission, message);
        }

        public void BankTransfersMoney(Bank bank1, Bank bank2, double money)
        {
            Transaction transaction = new Transaction(bank1.CorrespondentAccount, bank2.CorrespondentAccount, money);
            transaction.TransferMoney();
        }

        public void TransferMoney(IAccount account1, IAccount account2, double money)
        {
            if (account1 == null)
                throw new ArgumentException("One client is null");
            if (account2 == null)
                throw new ArgumentException("One client is null");
            Bank desiredBank1 = _banks.SingleOrDefault(desiredBank1 => desiredBank1.AccountOwnerVerification(account1));
            if (desiredBank1 == null)
                throw new ArgumentException("There is no bank, that contains this account");
            Bank desiredBank2 = _banks.SingleOrDefault(desiredBank2 => desiredBank2.AccountOwnerVerification(account2));
            if (desiredBank2 == null)
                throw new ArgumentException("There is no bank, that contains this account");
            if (desiredBank1.Id == desiredBank2.Id)
            {
                desiredBank1.MakeTransferWithinOneBank(account1, account2, money);
                return;
            }

            Transaction transactionBankAndAccount1 =
                new Transaction(account1, desiredBank1.CorrespondentAccount, money);
            transactionBankAndAccount1.TransferMoney();
            BankTransfersMoney(desiredBank1, desiredBank2, money);
            Transaction transactionBankAndAccount2 =
                new Transaction(desiredBank2.CorrespondentAccount, account2, money);
        }

        public void CancellationOfTransaction(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentException("Transaction is NULL");
            transaction.ReverseTransaction();
            TransferMoney(transaction.Sender, transaction.Recipient, transaction.Money);
        }
    }
}