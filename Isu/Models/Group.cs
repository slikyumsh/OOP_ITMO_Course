using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Tools;

namespace Isu.Models
{
    public class Group
    {
        private GroupName _groupName;
        private List<Student> _groupList;
        private int maxNumberOfStudents = 25;

        public Group(GroupName groupName)
        {
            _groupName = groupName;
            _groupList = new List<Student>();
        }

        public GroupName GetGroupName
        {
            get
            {
                return _groupName;
            }
        }

        public List<Student> GetGroupList
        {
            get
            {
                return _groupList;
            }
        }

        public Student AddStudent(Student student)
        {
            if (_groupList.Count == maxNumberOfStudents)
                throw new IsuException("A lot of students at one group");
            _groupList.Add(student);
            return student;
        }

        public Student FindStudent(string name)
        {
            Student desiredStudent = _groupList.SingleOrDefault(desiredStudent => desiredStudent.Name == name);
            return desiredStudent;
        }

        public Student FindStudent(int id)
        {
            Student desiredStudent = _groupList.SingleOrDefault(desiredStudent => desiredStudent.Id == id);
            return desiredStudent;
        }

        public Student RemoveStudent(int id)
        {
            Student student = _groupList.SingleOrDefault(student => student.Id == id);
            _groupList.Remove(student);
            return student;
        }
    }
}