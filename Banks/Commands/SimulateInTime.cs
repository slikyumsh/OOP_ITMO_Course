using System;
using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Commands
{
    public class SimulateInTime : ICommand
    {
        private readonly Parser _parser;
        private readonly CenterBank _centerBank;

        public SimulateInTime(string command, CenterBank centerBank)
        {
            _parser = new Parser(command);
            _centerBank = centerBank;
        }

        public string Name { get; }

        public void Run()
        {
            if (_parser.Arguments.Count != 1)
                throw new ArgumentException("Invalid command");
            _centerBank.ModelingWorkOfTheBankingSystemAfterCertainNumberOfDays(int.Parse(_parser.Arguments[0]), new Message("hey"), new Message("hello"));
        }
    }
}