using System;
using System.Collections.Generic;
using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Commands
{
    public class ShowIDsOfClients : ICommand
    {
        private readonly Parser _parser;
        private readonly CenterBank _centerBank;

        public ShowIDsOfClients(string command, CenterBank centerBank)
        {
            _parser = new Parser(command);
            _centerBank = centerBank;
        }

        public string Name { get; }

        public void Run()
        {
            if (_parser.Arguments.Count > 0)
                throw new ArgumentException("Invalid command");
            IReadOnlyList<Client> clients = _centerBank.Clients;
            foreach (Client desiredClient in clients)
            {
                Console.WriteLine(desiredClient.Id);
            }
        }
    }
}