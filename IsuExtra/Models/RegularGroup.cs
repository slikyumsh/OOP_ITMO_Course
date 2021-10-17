using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Models;

namespace IsuExtra.Models
{
    public class RegularGroup : Group
    {
        private readonly int maxNumberOfStudents = 25;
        private readonly List<OgnpStudent> _groupList;
        private Schedule _schedule;
        private Guid _id;
        private char _faculty;

        public RegularGroup(GroupName groupName)
            : base(groupName)
        {
            _schedule = new Schedule();
            _groupList = new List<OgnpStudent>();
            _id = Guid.NewGuid();
            _faculty = groupName.Letter;
        }

        public List<OgnpStudent> GetGroupList() => _groupList;
        public Guid Id() => _id;
        public char Faculty() => _faculty;

        public OgnpStudent FindStudent(OgnpStudent ognpStudent)
        {
            OgnpStudent desiredStudent =
                _groupList.SingleOrDefault(desiredStudent => desiredStudent.Id == ognpStudent.Id);
            if (desiredStudent == null)
                throw new ArgumentException("We can't find this student");
            return desiredStudent;
        }

        public OgnpStudent AddStudent(OgnpStudent ognpStudent)
        {
            if (_groupList.Count == maxNumberOfStudents)
                throw new ArgumentException("A lot of students at one group");
            _groupList.Add(ognpStudent);
            ognpStudent.SetRegularSchedule(_schedule);
            return ognpStudent;
        }
    }
}