using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Models;

namespace IsuExtra.Models
{
    public class OgnpGroup : Group
    {
        private readonly int maxNumberOfStudents = 20;
        private readonly List<ExtraStudent> _groupList;
        private ExtraFaculty _faculty;
        private Schedule _schedule;
        private Guid _id;

        public OgnpGroup(GroupName groupName)
            : base(groupName)
        {
            _faculty = CharToExtraFacultyEnum(groupName.Letter);
            _schedule = new Schedule();
            _id = Guid.NewGuid();
            _groupList = new List<ExtraStudent>();
        }

        public OgnpGroup(GroupName groupName, Schedule schedule)
            : base(groupName)
        {
            _faculty = CharToExtraFacultyEnum(groupName.Letter);
            _schedule = schedule;
            _id = Guid.NewGuid();
            _groupList = new List<ExtraStudent>();
        }

        public Guid Id => _id;
        public ExtraFaculty Faculty => _faculty;

        public new List<ExtraStudent> GroupList => _groupList;

        public ExtraFaculty CharToExtraFacultyEnum(char input)
        {
            if ((input < 'A' && input != '0') || (input > 'E' && input != 'M'))
                throw new ArgumentException("Invalid char value");
            return (ExtraFaculty)Enum.ToObject(typeof(ExtraFaculty), input);
        }

        public void AddLesson(Lesson lesson, DayOfWeek day)
        {
            if (lesson.Faculty != _faculty)
                throw new ArgumentException("Another faculty of lesson");
            _schedule.AddLesson(lesson, day);
        }

        public void AddStudent(ExtraStudent ognpStudent)
        {
            if (_groupList.Count == maxNumberOfStudents)
                throw new ArgumentException("A lot of students at one group");
            if (_schedule.IsIntersect(ognpStudent.PersonalSchedule))
                throw new Exception("Can't add student to this OGNP: schedules are intersected");
            _groupList.Add(ognpStudent);
            ognpStudent.PersonalSchedule.ConcatSchedules(_schedule);
            ognpStudent.IncrementOgnpStatus();
        }

        public bool CanAddStudent(ExtraStudent ognpStudent)
        {
            if (_groupList.Count == maxNumberOfStudents)
                return false;
            if (_schedule.IsIntersect(ognpStudent.PersonalSchedule))
                return false;
            _groupList.Add(ognpStudent);
            ognpStudent.PersonalSchedule.ConcatSchedules(_schedule);
            return true;
        }

        public void RemoveStudent(ExtraStudent ognpStudent)
        {
            if (CanRemoveStudent(ognpStudent)) _groupList.Remove(ognpStudent);
            ognpStudent.DecrementOgnpStatus();
            ognpStudent.PersonalSchedule.DeleteSchedules(_schedule);
        }

        public bool CanRemoveStudent(ExtraStudent ognpStudent)
        {
            if (_groupList.Count == 0)
                return false;
            ExtraStudent desiredStudent =
                _groupList.SingleOrDefault(desiredStudent => desiredStudent.Id == ognpStudent.Id);
            if (desiredStudent == null)
                return false;
            return true;
        }
    }
}