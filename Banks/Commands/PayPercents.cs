using System;
using System.Collections.Generic;
using Banks.Entities;
using Banks.Interfaces;
using Banks.Tools;

namespace Banks.Commands
{
    public class PayPercents : ICommand
    {
        private readonly Parser _parser;
        private readonly CenterBank _centerBank;

        public PayPercents(string command, CenterBank centerBank)
        {
            _parser = new Parser(command);
            _centerBank = centerBank;
        }

        public string Name { get; }

        public void Run()
        {
            if (_parser.Arguments.Count != 1)
                throw new ArgumentException("Invalid command");
            _centerBank.PayPercents(new Message(_parser.Arguments[0]));
        }
    }
}