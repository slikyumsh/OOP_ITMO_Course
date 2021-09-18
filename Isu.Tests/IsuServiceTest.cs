using System;
using Isu.Models;
using Isu.Services;
using System.Collections.Generic;
using Isu.Tools;
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
            var groupName = new GroupName('M', 3, 2, 11);
            Group group = _isuService.AddGroup(groupName);
            Student student = _isuService.AddStudent(group, "DIMA");
            
            if (_isuService.GetStudent(student.Id) == null)
                Assert.Fail("This student haven't any group");
            if (_isuService.GetStudent(student.Id) != null)
            {
                if (!group.GetGroupList.Contains(student))
                    Assert.Fail("This student have another group");
            }
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            var groupName = new GroupName('M', 3, 2, 11);
            Group group = _isuService.AddGroup(groupName);
            Assert.Catch<IsuException>(() =>
            {
                for (int i = 0; i < 26; i++)
                {
                    _isuService.AddStudent(group, "DIMA");
                }
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                var groupName = new GroupName('1', 1, 1, 10);
                var group = new Group(groupName);
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            var groupName1 = new GroupName('M', 3, 2, 11);
            Group group1 = _isuService.AddGroup(groupName1);
            
            var groupName2 = new GroupName('M', 3, 2, 12);
            Group group2 = _isuService.AddGroup(groupName2);

            Student student = _isuService.AddStudent(group1, "DIMA");
            _isuService.ChangeStudentGroup(student, group2);

            if (group2.FindStudent(student.Id) == null)
                Assert.Fail("Group hasn't changed");
            
            if (group1.FindStudent(student.Id) != null)
                Assert.Fail("Group hasn't changed");
        }
    }
}