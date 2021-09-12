using System;
using Isu.Tools;
namespace Isu.Models
{
    public class GroupNumber
    {
        private int _groupNumber;

        public GroupNumber()
        {
            _groupNumber = 0;
        }

        public GroupNumber(int number)
        {
            if (number > Constants.MaxNumberOfGroups || number < 0)
                throw new ArgumentOutOfRangeException("_groupNumber", "ArgumentOutOfRange");
            else
                _groupNumber = number;
        }

        public int NumberGroup
        {
            get
            {
                return _groupNumber;
            }
        }
    }
}