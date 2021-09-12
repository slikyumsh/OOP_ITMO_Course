using System;
using System.Collections.Generic;
using Isu.Models;
using Isu.Services;

namespace Isu
{
    public class Isu : IIsuService
    {
        private List<Group> groups = new List<Group>();
        private List<Student> students = new List<Student>();
        private Dictionary<Student, Group> arrayOfPairsGroupStudent = new Dictionary<Student, Group>();

        public Isu()
        {
        }

        public Group AddGroup(GroupName name)
        {
            Group newGroup = new Group(name);
            groups.Add(newGroup);
            return newGroup;
        }

        public Student AddStudent(Group group, string name)
        {
            Student newStudent = new Student(name);
            arrayOfPairsGroupStudent.Add(newStudent, group);
            return newStudent;
        }

        public Student GetStudent(int id)
        {
            foreach (Student student in students)
            {
                if (student.Id == id)
                    return student;
            }

            return null;
        }

        public Student FindStudent(string name)
        {
            foreach (Student student in students)
            {
                if (student.Name == name)
                    return student;
            }

            return null;
        }

        public List<Student> FindStudents(GroupName groupName)
        {
            List<Student> classmates = new List<Student>();
            foreach (var pair in arrayOfPairsGroupStudent)
            {
                if (pair.Value.GetGroupName == groupName)
                    classmates.Add(pair.Key);
            }

            return classmates;
        }

        public Group FindGroup(GroupName groupName)
        {
            foreach (Group group in groups)
            {
                if (group.GetGroupName == groupName)
                    return group;
            }

            return null;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            List<Student> fellowStudents = new List<Student>();
            foreach (var pair in arrayOfPairsGroupStudent)
            {
                if (pair.Value.GetCourseNumber == courseNumber)
                    fellowStudents.Add(pair.Key);
            }

            return fellowStudents;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            List<Group> fellowGroups = new List<Group>();
            foreach (Group group in groups)
            {
                if (group.GetCourseNumber == courseNumber)
                    fellowGroups.Add(group);
            }

            return fellowGroups;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            foreach (var pair in arrayOfPairsGroupStudent)
            {
                if (pair.Key.Id == student.Id && pair.Key.Name == student.Name)
                {
                    arrayOfPairsGroupStudent.Remove(student);
                    arrayOfPairsGroupStudent.Add(student, newGroup);
                }
            }
        }
    }
}