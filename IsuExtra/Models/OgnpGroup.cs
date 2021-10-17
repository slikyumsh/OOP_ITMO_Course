using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Models;

namespace IsuExtra.Models
{
    public class OgnpGroup : Group
    {
        private readonly int maxNumberOfStudents = 20;
        private readonly List<OgnpStudent> _groupList;
        private char _faculty;
        private Schedule _schedule;
        private Guid _id;

        public OgnpGroup(GroupName groupName)
            : base(groupName)
        {
            _faculty = groupName.Letter;
            _schedule = new Schedule();
            _id = Guid.NewGuid();
            _groupList = new List<OgnpStudent>();
        }

        public Guid Id() => _id;
        public char Faculty() => _faculty;

        public List<OgnpStudent> GetSudentList()
        {
            return _groupList;
        }

        public void AddLesson(Lesson lesson, int day)
        {
            if (lesson.Faculty() != _faculty)
                throw new ArgumentException("Another faculty of lesson");
            _schedule.AddLesson(lesson, day);
        }

        public void AddStudent(OgnpStudent ognpStudent)
        {
            if (_groupList.Count == maxNumberOfStudents)
                throw new ArgumentException("A lot of students at one group");
            if (_schedule.IsIntersect(ognpStudent.PersonalSchedule()))
                throw new Exception("Can't add student to this OGNP");
            _groupList.Add(ognpStudent);
            ognpStudent.PersonalSchedule().ConcatSchedules(_schedule);
            ognpStudent.IncrementOgnpStatus();
        }

        public bool CanAddStudent(OgnpStudent ognpStudent)
        {
            if (_groupList.Count == maxNumberOfStudents)
                return false;
            if (_schedule.IsIntersect(ognpStudent.PersonalSchedule()))
                return false;
            _groupList.Add(ognpStudent);
            ognpStudent.PersonalSchedule().ConcatSchedules(_schedule);
            return true;
        }

        public void RemoveStudent(OgnpStudent ognpStudent)
        {
            if (_groupList.Count == 0)
                throw new ArgumentException("Not enough students at one group");
            OgnpStudent desiredStudent =
                _groupList.SingleOrDefault(desiredStudent => desiredStudent.Id == ognpStudent.Id);
            if (desiredStudent == null)
                throw new ArgumentException("We haven't this student at this group");
            _groupList.Remove(ognpStudent);
            ognpStudent.DecrementOgnpStatus();
            ognpStudent.PersonalSchedule().DeleteSchedules(_schedule);
        }

        public bool CanRemoveStudent(OgnpStudent ognpStudent)
        {
            if (_groupList.Count == 0)
                return false;
            OgnpStudent desiredStudent =
                _groupList.SingleOrDefault(desiredStudent => desiredStudent.Id == ognpStudent.Id);
            if (desiredStudent == null)
                return false;
            return true;
        }
    }
}