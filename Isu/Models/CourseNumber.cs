using System;
using Isu.Tools;
namespace Isu.Models
{
    public class CourseNumber
    {
        private int _courseNumber;

        public CourseNumber()
        {
            _courseNumber = 1;
        }

        public CourseNumber(int number)
        {
            if (number > Constants.GradesNumber)
                _courseNumber = (number % Constants.GradesNumber) + 1;
            else
                _courseNumber = number;
        }

        public int GetCourseNumber
        {
            get
            {
                return _courseNumber;
            }
        }
    }
}