using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Banks
{
    public class ClientBuilder
    {
        private static int _currentClientId = 0;
        private int _id;
        private string _name;
        private string _surname;
        private string _address;
        private string _passport;
        private PhoneNumber _phone;
        private List<IAccount> _accounts;

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
            _address = address;
            return this;
        }

        public ClientBuilder AddPassport(string passport)
        {
            _passport = passport;
            return this;
        }

        public ClientBuilder AddPhone(PhoneNumber phone)
        {
            _phone = phone;
            return this;
        }

        public ClientBuilder AddListAccounts()
        {
            _accounts = new List<IAccount>();
            return this;
        }

        public ClientBuilder AddId()
        {
            _id = _currentClientId;
            _currentClientId++;
            return this;
        }

        public Client Build()
        {
            return new Client(_name, _surname, _address, _passport, _phone, _accounts, _id);
        }
    }
}