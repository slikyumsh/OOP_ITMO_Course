using System;
using Isu.Tools;
namespace Isu.Models
{
    public class CourseNumber
    {
        private int _courseNumber;

        public CourseNumber(int number)
        {
            if (number > Constants.GradesNumber || number < 1)
                throw new IsuException("Invalid CourseNumber");
            else
                _courseNumber = number;
        }

        public int Number => _courseNumber;
    }
}