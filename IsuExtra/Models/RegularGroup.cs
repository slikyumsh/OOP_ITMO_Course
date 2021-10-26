using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Models;

namespace IsuExtra.Models
{
    public class RegularGroup : Group
    {
        private readonly int maxNumberOfStudents = 25;
        private readonly List<ExtraStudent> _groupList;
        private Schedule _schedule;
        private Guid _id;
        private ExtraFaculty _faculty;

        public RegularGroup(GroupName groupName)
            : base(groupName)
        {
            _schedule = new Schedule();
            _groupList = new List<ExtraStudent>();
            _id = Guid.NewGuid();
            _faculty = CharToExtraFacultyEnum(groupName.Letter);
        }

        public RegularGroup(GroupName groupName, Schedule schedule)
            : base(groupName)
        {
            _schedule = schedule;
            _groupList = new List<ExtraStudent>();
            _id = Guid.NewGuid();
            _faculty = CharToExtraFacultyEnum(groupName.Letter);
        }

        public Guid Id => _id;
        public ExtraFaculty Faculty => _faculty;

        public List<ExtraStudent> GetGroupList => _groupList;
        public ExtraFaculty CharToExtraFacultyEnum(char input)
        {
            if ((input < 'A' && input != '0') || (input > 'E' && input != 'M'))
                throw new ArgumentException("Invalid char value");
            return (ExtraFaculty)Enum.ToObject(typeof(ExtraFaculty), input);
        }

        public ExtraStudent FindStudent(ExtraStudent ognpStudent)
        {
            ExtraStudent desiredStudent =
                _groupList.SingleOrDefault(desiredStudent => desiredStudent.Id == ognpStudent.Id);
            if (desiredStudent == null)
                throw new ArgumentException("We can't find this student");
            return desiredStudent;
        }

        public ExtraStudent AddStudent(ExtraStudent ognpStudent)
        {
            if (_groupList.Count == maxNumberOfStudents)
                throw new ArgumentException("A lot of students at one group");
            _groupList.Add(ognpStudent);
            ognpStudent.SetRegularSchedule(_schedule);
            return ognpStudent;
        }
    }
}