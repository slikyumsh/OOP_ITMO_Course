using System;
using System.Collections.Generic;
using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Commands
{
    public class ShowMoneyInAccount : ICommand
    {
        private readonly Parser _parser;
        private readonly List<IAccount> _accounts;

        public ShowMoneyInAccount(string command, CenterBank centerBank, List<IAccount> accounts)
        {
            _parser = new Parser(command);
            _accounts = accounts;
        }

        public string Name { get; }

        public void Run()
        {
            if (_parser.Arguments.Count != 1)
                throw new ArgumentException("Invalid command");
            int accountNumber = int.Parse(_parser.Arguments[0]);
            if (accountNumber >= _accounts.Count || accountNumber < 0)
                throw new ArgumentException("Invalid number of account");
            Console.WriteLine(_accounts[accountNumber].Money);
        }
    }
}