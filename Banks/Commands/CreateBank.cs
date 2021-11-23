using System;
using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Commands
{
    public class CreateBank : ICommand
    {
        private readonly Parser _parser;
        private readonly CenterBank _centerBank;

        public CreateBank(string command, CenterBank centerBank)
        {
            _parser = new Parser(command);
            _centerBank = centerBank;
        }

        public string Name { get; }

        public void Run()
        {
            if (_parser.Arguments.Count != 5)
                throw new ArgumentException("Invalid command");
            var bank = new Bank(_parser.Arguments[0], int.Parse(_parser.Arguments[1]), int.Parse(_parser.Arguments[2]), int.Parse(_parser.Arguments[3]), new CorrespondentAccount(int.Parse(_parser.Arguments[4])));
            _centerBank.AddBank(bank);
        }
    }
}