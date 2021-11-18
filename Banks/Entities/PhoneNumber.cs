using System;

namespace Banks
{
    public class PhoneNumber
    {
        private string _number;
        private int _numberLength = 11;
        private int _firstDigit = 8;

        public PhoneNumber(string number)
        {
            if (string.IsNullOrEmpty(number))
                throw new ArgumentException("Empty number-string");
            if (number.Length != _numberLength || number[0] - '0' != _firstDigit)
                throw new ArgumentException("Invalid phone number");
            _number = number;
        }
    }
}