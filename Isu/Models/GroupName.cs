using System;
namespace Isu.Models
{
    public class GroupName
    {
        private char _letter;
        private int _studyingType;
        private CourseNumber _courseNumber;
        private GroupNumber _groupNumber;

        public GroupName()
        {
            _letter = 'A';
            _studyingType = 0;
            _courseNumber = new CourseNumber();
            _groupNumber = new GroupNumber();
        }

        public GroupName(char letter)
        {
            if (letter >= 'A' && letter <= 'Z')
                _letter = letter;
            else
                throw new ArgumentOutOfRangeException("_letter", "ArgumentOutOfRange");
            _studyingType = 0;
            _courseNumber = new CourseNumber();
            _groupNumber = new GroupNumber();
        }

        public GroupName(char letter, int studyingType, CourseNumber courseNumber, GroupNumber groupNumber)
        {
            _letter = letter;
            _studyingType = studyingType;
            _courseNumber = courseNumber;
            _groupNumber = groupNumber;
        }

        public char Letter
        {
            get
            {
                return _letter;
            }
        }

        public int StudyingType
        {
            get
            {
                return _studyingType;
            }
        }

        public CourseNumber GetCourseNumber
        {
            get
            {
                return _courseNumber;
            }
        }

        public GroupNumber GetNumber
        {
            get
            {
                return _groupNumber;
            }
        }
    }
}