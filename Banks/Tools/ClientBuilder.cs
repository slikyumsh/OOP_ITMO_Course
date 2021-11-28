using System;
using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Tools
{
    public class ClientBuilder
    {
        private static int _currentClientId = 0;
        private readonly ILogger _logger;
        private int _id;
        private string _name;
        private string _surname;
        private string _address;
        private string _passport;
        private PhoneNumber _phone;

        public ClientBuilder(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentException("Invalid logger");
        }

        public ClientBuilder AddName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Empty name-string");
            _name = name;
            return this;
        }

        public ClientBuilder AddSurname(string surname)
        {
            if (string.IsNullOrEmpty(surname))
                throw new ArgumentException("Empty surname-string");
            _surname = surname;
            return this;
        }

        public ClientBuilder AddAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentException("Empty address-string");
            _address = address;
            return this;
        }

        public ClientBuilder AddPassport(string passport)
        {
            if (string.IsNullOrEmpty(passport))
                throw new ArgumentException("Empty passport-string");
            _passport = passport;
            return this;
        }

        public ClientBuilder AddPhone(PhoneNumber phone)
        {
            if (phone == null)
                throw new ArgumentException("Null phone");
            _phone = phone;
            return this;
        }

        public Client Build()
        {
            _id = _currentClientId++;
            return new Client(_name, _surname, _address, _passport, _phone, _id, _logger);
        }
    }
}