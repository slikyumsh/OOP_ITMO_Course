using System;
using System.Collections.Generic;
using Isu.Models;
using NUnit.Framework;
using IsuExtra.Models;
using IsuExtra.Models.Models;

namespace IsuExtra.Tests
{
    public class IsuExtraServiceTests
    {
        private OgnpService _service;
        [SetUp]
        public void Setup()
        {
            _service = new OgnpService();
        }
        
        [Test]
        public void AddOgnp()
        {
            var ognpCourse = new OgnpCourse('M');
            _service.AddOgnpCourse(ognpCourse);
            Assert.AreEqual(1,_service.NumberOfCourses() );
        }
        
        [Test]
        public void CheckLessonsIntrsection()
        {
            var time1 = new DateTime(2008, 11, 10, 11, 0, 0);
            var time2 = new DateTime(2008, 11, 10, 12, 0, 0);
            var lesson1 = new Lesson(time1, 'M', "OOP", new Cabinet(), new Professor());
            var lesson2 = new Lesson(time2, 'M', "OOP", new Cabinet(), new Professor());
            Assert.AreEqual(true,lesson1.IsIntersect(lesson2));
        }

        [Test]
        public void SuccesEnrollStudentToThisOgnp()
        {
            var student = new OgnpStudent("DIMA");
            var groupName = new GroupName('M', 3, 2, 11);
            var mainGroup = new RegularGroup(groupName);
            _service.AddRegularGroup(mainGroup);
            mainGroup.AddStudent(student);
            var ognpCourse = new OgnpCourse('A');
            var groupName2 = new GroupName('A', 3, 2, 10);
            var ognpGroup = new OgnpGroup(groupName2);
            var flow = new OgnpFlow('A');
            flow.AddOgnpGroup(ognpGroup);
            ognpCourse.AddFlow(flow);
            _service.AddOgnpCourse(ognpCourse);
            _service.EnrollStudentOnCourse(student, ognpCourse);
            Assert.AreEqual(1, student.OgnpStatus() );
        }
        
        [Test]
        public void FailEnrollStudentToThisOgnp()
        {
            var student = new OgnpStudent("DIMA");
            var groupName = new GroupName('M', 3, 2, 11);
            var mainGroup = new RegularGroup(groupName);
            _service.AddRegularGroup(mainGroup);
            mainGroup.AddStudent(student);
            var ognpCourse = new OgnpCourse('M');
            var groupName2 = new GroupName('M', 3, 2, 10);
            var ognpGroup = new OgnpGroup(groupName2);
            var flow = new OgnpFlow('M');
            flow.AddOgnpGroup(ognpGroup);
            ognpCourse.AddFlow(flow);
            _service.AddOgnpCourse(ognpCourse);
            _service.EnrollStudentOnCourse(student, ognpCourse);
            Assert.AreEqual(0, student.OgnpStatus() );
        }
        
        [Test]
        public void GetFlowsByCourse()
        {
            var flow1 = new OgnpFlow('A');
            var flow2 = new OgnpFlow('A');
            var flow3 = new OgnpFlow('A');
            var flow4 = new OgnpFlow('A');
            var ognpCourse = new OgnpCourse('A');
            _service.AddOgnpCourse(ognpCourse);
            ognpCourse.AddFlow(flow1);
            ognpCourse.AddFlow(flow2);
            ognpCourse.AddFlow(flow3);
            ognpCourse.AddFlow(flow4);
            List<OgnpFlow> result = ognpCourse.Flows();
            Assert.AreEqual(4, result.Count);
        }
        
        [Test]
        public void GetStudentsListFromThisOgnpFroup()
        {
            var groupName = new GroupName('M', 3, 2, 11);
            var ognpGroup = new OgnpGroup(groupName);
            var student1 = new OgnpStudent("DIMA");
            var student2 = new OgnpStudent("Sasha");
            var student3 = new OgnpStudent("Misha");
            ognpGroup.AddStudent(student1);
            ognpGroup.AddStudent(student2);
            ognpGroup.AddStudent(student3);
            List<OgnpStudent> result = ognpGroup.GetSudentList();
            string answer = "";
            foreach (OgnpStudent student in result)
            {
                answer += student.Name;
            }
            Assert.AreEqual("DIMASashaMisha", answer);
        }
        
        [Test]
        public void GetStudentsListWithoutOgnpFroup()
        {
            var groupName = new GroupName('M', 3, 2, 11);
            var group = new RegularGroup(groupName);
            _service.AddRegularGroup(group);
            var student1 = new OgnpStudent("DIMA");
            var student2 = new OgnpStudent("Sasha");
            var student3 = new OgnpStudent("Misha");
            group.AddStudent(student1);
            group.AddStudent(student2);
            group.AddStudent(student3);
            var groupName1 = new GroupName('A', 3, 2, 11);
            var ognpGroup = new OgnpGroup(groupName1);
            ognpGroup.AddStudent(student1);
            var flow = new OgnpFlow('A');
            var course = new OgnpCourse('A');
            flow.AddOgnpGroup(ognpGroup);
            course.AddFlow(flow);
            _service.AddOgnpCourse(course);
            List<OgnpStudent> result = _service.GetListOfNotRecordedStudentsFromThisGroup(group);
            string answer = "";
            foreach (OgnpStudent student in result)
            {
                answer += student.Name;
            }
            Assert.AreEqual("SashaMisha", answer);
        }
        
        [Test]
        public void RemoveEntryForOgnpforStudent()
        {
            var student = new OgnpStudent("DIMA");
            var groupName = new GroupName('M', 3, 2, 11);
            var mainGroup = new RegularGroup(groupName);
            _service.AddRegularGroup(mainGroup);
            mainGroup.AddStudent(student);
            var ognpCourse = new OgnpCourse('A');
            var groupName2 = new GroupName('A', 3, 2, 10);
            var ognpGroup = new OgnpGroup(groupName2);
            ognpGroup.AddStudent(student);
            var flow = new OgnpFlow('A');
            flow.AddOgnpGroup(ognpGroup);
            ognpCourse.AddFlow(flow);
            _service.AddOgnpCourse(ognpCourse);
            _service.RemoveRecordingFromCcourse(student, ognpCourse);
            Assert.AreEqual(0, student.OgnpStatus() );
        }
        
    }
}