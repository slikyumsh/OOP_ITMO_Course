using System;
using Isu.Tools;

namespace Isu.Models
{
    public class GroupName
    {
        private char _letter;
        private int _studyingType;
        private CourseNumber _courseNumber;
        private GroupNumber _groupNumber;
        public GroupName(char letter, int studyingType, int courseNumber, int groupNumber)
        {
            if (letter != 'M' || studyingType != 3)
                throw new IsuException("Invalid GroupName");
            _courseNumber = new CourseNumber(courseNumber);
            _groupNumber = new GroupNumber(groupNumber);
            _letter = letter;
            _studyingType = studyingType;
            Name = Convert.ToString(letter) + Convert.ToString(studyingType) + Convert.ToString(courseNumber) +
                   Convert.ToString(groupNumber);
        }

        public string Name { get; }

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