﻿using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Models;
using Isu.Services;
using Isu.Tools;

namespace Isu
{
    public class Isu : IIsuService
    {
        private List<Group> groups = new List<Group>();

        public Isu()
        {
        }

        public Group AddGroup(GroupName name)
        {
            var newGroup = new Group(name);
            groups.Add(newGroup);
            return newGroup;
        }

        public Student AddStudent(Group group, string name)
        {
            Group desiredGroup = groups.SingleOrDefault(desiredGroup => desiredGroup.GetGroupName == group.GetGroupName);
            if (desiredGroup == null)
                throw new IsuException("Can't find the group");
            var student = new Student(name);
            desiredGroup.AddStudent(student);
            return student;
        }

        public Student GetStudent(int id)
        {
            Group group = groups.SingleOrDefault(group => group.FindStudent(id) != null);
            if (group == null)
                throw new IsuException("Unable to find student");
            return group.FindStudent(id);
        }

        public Student FindStudent(string name)
        {
            Group group = groups.SingleOrDefault(group => group.FindStudent(name) != null);
            if (group == null)
                throw new IsuException("Unable to find student");
            return group.FindStudent(name);
        }

        public List<Student> FindStudents(GroupName groupName)
        {
            Group group = groups.SingleOrDefault(group => group.GetGroupName.Name == groupName.Name);
            if (group == null)
                throw new IsuException("Can't find students");
            return group.GetGroupList;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            Group group = groups.SingleOrDefault(group => group.GetGroupName.GetCourseNumber.GetNumber == courseNumber.GetNumber);
            if (group == null)
                throw new IsuException("Course doesn't exist");
            return group.GetGroupList;
        }

        public Group FindGroup(GroupName groupName)
        {
            Group group = groups.SingleOrDefault(group => group.GetGroupName.Name == groupName.Name);
            if (group == null)
                throw new IsuException("Group doesn't exist");
            return group;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            var searchingGroups = groups.Where(group => group.GetGroupName.GetCourseNumber.GetNumber == courseNumber.GetNumber).ToList();
            if (!searchingGroups.Any())
                throw new IsuException("There are not any groups with this CourseNumber");
            return searchingGroups;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            Group groupToRemoveStudentFrom = groups.SingleOrDefault(group => group.FindStudent(student.Id) != null);
            newGroup.AddStudent(student);
            groupToRemoveStudentFrom.RemoveStudent(student.Id);
        }
    }
}