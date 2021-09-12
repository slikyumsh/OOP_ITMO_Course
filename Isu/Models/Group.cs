using System;

namespace Isu.Models
{
    public class Group
    {
        private CourseNumber _courseNumber;
        private GroupName _groupName;

        public Group()
        {
            _courseNumber = new CourseNumber();
            _groupName = new GroupName();
        }

        public Group(CourseNumber courseNumber)
        {
            _courseNumber = courseNumber;
            _groupName = new GroupName();
        }

        public Group(GroupName groupName)
        {
            _courseNumber = new CourseNumber();
            _groupName = groupName;
        }

        public Group(CourseNumber courseNumber, GroupName groupName)
        {
            _courseNumber = courseNumber;
            _groupName = groupName;
        }

        public CourseNumber GetCourseNumber
        {
            get
            {
                return _courseNumber;
            }
        }

        public GroupName GetGroupName
        {
            get
            {
                return _groupName;
            }
        }
    }
}