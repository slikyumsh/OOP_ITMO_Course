using System;
using System.Collections.Generic;
using System.Linq;

namespace Banks
{
    public class Bank
    {
        private readonly Guid _id;
        private readonly string _name;
        private readonly double _limitForNotConfirmedClients;
        private double _procent;
        private double _comission;
        private List<Client> _clients;
        private List<Client> _followers;

        public Bank(string name, int procent, int comission, int limitForNotConfirmedClients)
        {
            _id = Guid.NewGuid();
            _name = name;
            _procent = procent;
            _comission = comission;
            _limitForNotConfirmedClients = limitForNotConfirmedClients;
            _clients = new List<Client>();
            _followers = new List<Client>();
        }

        public Guid Id => _id;

        public void AddClient(Client client)
        {
            Client desiredClient = _clients.SingleOrDefault(desiredClient => desiredClient.Id == client.Id);
            if (desiredClient != null)
                throw new ArgumentException("We already have this client");
            _clients.Add(client);
        }

        public void ChangeProcent(int newProcent)
        {
            if (newProcent < 0)
                throw new ArgumentException("Invalid Procent");
            _procent = newProcent;
        }

        public void ChangeComission(int newComission)
        {
            if (newComission < 0)
                throw new ArgumentException("Invalid Comission");
            _procent = newComission;
        }
    }
}