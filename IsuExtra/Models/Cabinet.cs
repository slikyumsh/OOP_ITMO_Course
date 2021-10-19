using System;

namespace IsuExtra.Models
{
    public class Cabinet
    {
        private static int _minCabinetNumber = 0;
        private Guid _id;
        private int _number;

        public Cabinet(int number)
        {
            _id = Guid.NewGuid();
            _number = _minCabinetNumber;
            _minCabinetNumber++;
        }

        public Guid Id => _id;
    }
}