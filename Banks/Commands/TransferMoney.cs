using System;
using System.Collections.Generic;
using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Commands
{
    public class TransferMoney : ICommand
    {
        private readonly Parser _parser;
        private readonly CenterBank _centerBank;
        private readonly List<IAccount> _accounts;
        private readonly List<Transaction> _transactions;

        public TransferMoney(string command, CenterBank centerBank, List<IAccount> accounts, List<Transaction> transactions)
        {
            _parser = new Parser(command);
            _centerBank = centerBank;
            _accounts = accounts;
            _transactions = transactions;
        }

        public string Name { get; }

        public void Run()
        {
            if (_parser.Arguments.Count != 3)
                throw new ArgumentException("Invalid command");
            int accountNumber1 = int.Parse(_parser.Arguments[0]);
            int accountNumber2 = int.Parse(_parser.Arguments[1]);
            int money = int.Parse(_parser.Arguments[2]);
            if (accountNumber1 >= _accounts.Count || accountNumber1 < 0)
                throw new ArgumentException("Invalid number of account");
            if (accountNumber2 >= _accounts.Count || accountNumber2 < 0)
                throw new ArgumentException("Invalid number of account");
            var transaction =
                new Transaction(_accounts[accountNumber1], _accounts[accountNumber2], money);
            _transactions.Add(transaction);
            _centerBank.TransferMoney(_accounts[accountNumber1], _accounts[accountNumber2], money);
        }
    }
}