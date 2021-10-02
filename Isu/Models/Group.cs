using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Tools;

namespace Isu.Models
{
    public class Group
    {
        private GroupName _groupName;
        private List<Student> _students;
        private int maxNumberOfStudents = 25;

        public Group(GroupName groupName)
        {
            _groupName = groupName;
            _students = new List<Student>();
        }

        public GroupName GroupName => _groupName;

        public List<Student> GroupList => _students;

        public Student AddStudent(Student student)
        {
            if (_students.Count == maxNumberOfStudents)
                throw new IsuException("A lot of students at one group");
            _students.Add(student);
            return student;
        }

        public Student FindStudent(string name)
        {
            Student desiredStudent = _students.SingleOrDefault(desiredStudent => desiredStudent.Name == name);
            return desiredStudent;
        }

        public Student FindStudent(int id)
        {
            Student desiredStudent = _students.SingleOrDefault(desiredStudent => desiredStudent.Id == id);
            return desiredStudent;
        }

        public void RemoveStudent(int id)
        {
            Student student = _students.SingleOrDefault(student => student.Id == id);
            _students.Remove(student);
        }
    }
}