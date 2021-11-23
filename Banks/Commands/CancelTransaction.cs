using System;
using System.Collections.Generic;
using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Commands
{
    public class CancelTransaction : ICommand
    {
        private readonly Parser _parser;
        private readonly CenterBank _centerBank;
        private readonly List<Transaction> _transactions;

        public CancelTransaction(string command, CenterBank centerBank, List<Transaction> transactions)
        {
            _parser = new Parser(command);
            _centerBank = centerBank;
            _transactions = transactions;
        }

        public string Name { get; }

        public void Run()
        {
            if (_parser.Arguments.Count != 1)
                throw new ArgumentException("Invalid command");
            int transactionNumber = int.Parse(_parser.Arguments[0]);
            if (transactionNumber >= _transactions.Count || transactionNumber < 0)
                throw new ArgumentException("Invalid number of account");
            _centerBank.CancellationOfTransaction(_transactions[transactionNumber]);
        }
    }
}