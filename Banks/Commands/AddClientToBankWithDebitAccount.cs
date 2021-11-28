using System;
using System.Collections.Generic;
using Banks.Entities;
using Banks.Interfaces;
using Banks.Tools;

namespace Banks.Commands
{
    public class AddClientToBankWithDebitAccount : ICommand
    {
        private readonly Parser _parser;
        private readonly CenterBank _centerBank;
        private readonly List<IAccount> _accounts;

        public AddClientToBankWithDebitAccount(string command, CenterBank centerBank, List<IAccount> accounts)
        {
            _parser = new Parser(command);
            _centerBank = centerBank;
            _accounts = accounts;
        }

        public string Name { get; }

        public void Run()
        {
            if (_parser.Arguments.Count != 5)
                throw new ArgumentException("Invalid command");
            Bank bank = _centerBank.GetBankById(int.Parse(_parser.Arguments[0]));
            Client client = _centerBank.GetClientById(int.Parse(_parser.Arguments[1]));
            var debitAccountCreator = new DebitAccountCreator(int.Parse(_parser.Arguments[2]), int.Parse(_parser.Arguments[3]), int.Parse(_parser.Arguments[4]));
            IAccount account1 = debitAccountCreator.Create();
            var account2 = account1 as DebitAccount;
            client.OpenNewAccount(bank, account2);
            _accounts.Add(account2);
        }
    }
}