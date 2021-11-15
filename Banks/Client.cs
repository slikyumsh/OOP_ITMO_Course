using System;
using System.Collections.Generic;

namespace Banks
{
    public class Client
    {
        private int _id;
        private string _name;
        private string _surname;
        private string _address;
        private string _passport;
        private PhoneNumber _phone;
        private List<IAccount> _accounts;

        public Client(string name, string surname, string address, string passport, PhoneNumber phone, List<IAccount> accounts, int id)
        {
            _name = name;
            _surname = surname;
            _address = address;
            _passport = passport;
            _phone = phone;
            _id = id;
            _accounts = accounts;
        }

        public int Id => _id;
    }
}