using System;
using Isu.Models;
using Isu.Services;
using System.Collections.Generic;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _isuService = new Isu();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Student student1 = new Student();
            Group group1 = new Group();
            student1 = _isuService.AddStudent(group1, student1.Name);
            if (student1 == null) 
                Assert.Fail("Error: Can't add this student at this group");
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            const int maxStudentPerGroup = 30;
            Student student = new Student("Dima");
            Group group = new Group();
            for (int i = 0; i < maxStudentPerGroup; i++)
            {
                student = _isuService.AddStudent(group, student.Name);
            }
            student = _isuService.AddStudent(group, student.Name);
            if (student != null)
                Assert.Fail("Error: A lot of stuents at one group");
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            bool throwned = false;
            try
            {
                CourseNumber courseNumber = new CourseNumber(71);
            }
            catch (FormatException)
            {
                throwned = true;
            }
            
            Assert.IsTrue(throwned, "Error at creating CourseNumber");
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Student student = new Student();
            GroupName groupName1 = new GroupName('M', 3, new CourseNumber(2), new GroupNumber(11));
            Group group1 = new Group(groupName1);
            student = _isuService.AddStudent(group1, student.Name);
            
            GroupName groupName2 = new GroupName('P', 3, new CourseNumber(2), new GroupNumber(11));
            Group group2 = new Group(groupName2);
            _isuService.ChangeStudentGroup(student, group2);

            List<Student> changedGroup = _isuService.FindStudents(groupName2);
            foreach (var possibleStudent in changedGroup)
            {
                if (possibleStudent == student) return;
            }
            
            Assert.Fail("Error: Can't change student's group");
        }
    }
}