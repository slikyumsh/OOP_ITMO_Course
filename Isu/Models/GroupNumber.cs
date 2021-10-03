using System;
using Isu.Tools;
namespace Isu.Models
{
    public class GroupNumber
    {
        private int _groupNumber;

        public GroupNumber(int number)
        {
            if (number > Constants.MaxNumberOfGroups || number < 0)
                throw new IsuException("Invalid GroupNumber");
            _groupNumber = number;
        }

        public int NumberGroup => _groupNumber;
    }
}