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
            Name = $"{letter}{studyingType}{courseNumber}{groupNumber}";
        }

        public string Name { get; }

        public char Letter => _letter;

        public int StudyingType => _studyingType;

        public CourseNumber CourseNumber => _courseNumber;

        public GroupNumber Number => _groupNumber;
    }
}