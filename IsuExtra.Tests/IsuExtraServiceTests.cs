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
            var ognpCourse = new OgnpCourse(ExtraFaculty.MKTU);
            _service.AddOgnpCourse(ognpCourse);
            Assert.AreEqual(1,_service.NumberOfCourses() );
        }
        
        [Test]
        public void CheckLessonsIntrsection()
        {
            var time1 = new DateTime(2008, 11, 10, 11, 0, 0);
            var time2 = new DateTime(2008, 11, 10, 12, 0, 0);
            var lesson1 = new Lesson(time1, ExtraFaculty.FTMI, "OOP", new Cabinet(1), new Professor());
            var lesson2 = new Lesson(time2, ExtraFaculty.MBiNS, "OOP", new Cabinet(2), new Professor());
            Assert.AreEqual(true,lesson1.IsIntersect(lesson2));
        }

        [Test]
        public void SuccesEnrollStudentToThisOgnp()
        {
            var student = new ExtraStudent("DIMA");
            var groupName = new GroupName('M', 3, 2, 11);
            var mainGroup = new RegularGroup(groupName);
            _service.AddRegularGroup(mainGroup);
            mainGroup.AddStudent(student);
            
            var ognpCourse = new OgnpCourse(ExtraFaculty.FTMI);
            var groupName2 = new GroupName('C', 3, 2, 10);
            var ognpGroup = new OgnpGroup(groupName2);
            var flow = new OgnpFlow(ExtraFaculty.FTMI);
            flow.AddOgnpGroup(ognpGroup);
            ognpCourse.AddFlow(flow);
            _service.AddOgnpCourse(ognpCourse);
            _service.EnrollStudentOnCourse(student, ognpCourse);
            Assert.AreEqual(OgnpStatus.OneOgnp, student.OgnpStatus );
        }
        
        [Test]
        public void FailEnrollStudentToThisOgnp()
        {
            var student = new ExtraStudent("DIMA");
            var groupName = new GroupName('M', 3, 2, 11);
            var mainGroup = new RegularGroup(groupName);
            _service.AddRegularGroup(mainGroup);
            mainGroup.AddStudent(student);
            var ognpCourse = new OgnpCourse(ExtraFaculty.FITiP);
            var groupName2 = new GroupName('M', 3, 2, 10);
            var ognpGroup = new OgnpGroup(groupName2);
            var flow = new OgnpFlow(ExtraFaculty.FITiP);
            flow.AddOgnpGroup(ognpGroup);
            ognpCourse.AddFlow(flow);
            _service.AddOgnpCourse(ognpCourse);
            _service.EnrollStudentOnCourse(student, ognpCourse);
            Assert.AreEqual(OgnpStatus.NotRecorded, student.OgnpStatus );
        }
        
        [Test]
        public void GetFlowsByCourse()
        {
            var flow1 = new OgnpFlow(ExtraFaculty.IMRP);
            var flow2 = new OgnpFlow(ExtraFaculty.IMRP);
            var flow3 = new OgnpFlow(ExtraFaculty.IMRP);
            var flow4 = new OgnpFlow(ExtraFaculty.IMRP);
            var ognpCourse = new OgnpCourse(ExtraFaculty.IMRP);
            _service.AddOgnpCourse(ognpCourse);
            ognpCourse.AddFlow(flow1);
            ognpCourse.AddFlow(flow2);
            ognpCourse.AddFlow(flow3);
            ognpCourse.AddFlow(flow4);
            List<OgnpFlow> result = ognpCourse.Flows;
            Assert.AreEqual(4, result.Count);
        }
        
        [Test]
        public void GetStudentsListFromThisOgnpFroup()
        {
            var groupName = new GroupName('M', 3, 2, 11);
            var ognpGroup = new OgnpGroup(groupName);
            var student1 = new ExtraStudent("DIMA");
            var student2 = new ExtraStudent("Sasha");
            var student3 = new ExtraStudent("Misha");
            ognpGroup.AddStudent(student1);
            ognpGroup.AddStudent(student2);
            ognpGroup.AddStudent(student3);
            List<ExtraStudent> result = ognpGroup.GroupList;
            string answer = "";
            foreach (ExtraStudent student in result)
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
            var student1 = new ExtraStudent("DIMA");
            var student2 = new ExtraStudent("Sasha");
            var student3 = new ExtraStudent("Misha");
            group.AddStudent(student1);
            group.AddStudent(student2);
            group.AddStudent(student3);
            var groupName1 = new GroupName('C', 3, 2, 11);
            var ognpGroup = new OgnpGroup(groupName1);
            ognpGroup.AddStudent(student1);
            var flow = new OgnpFlow(ExtraFaculty.FTMI);
            var course = new OgnpCourse(ExtraFaculty.FTMI);
            flow.AddOgnpGroup(ognpGroup);
            course.AddFlow(flow);
            _service.AddOgnpCourse(course);
            List<ExtraStudent> result = _service.GetListOfNotRecordedStudentsFromThisGroup(group);
            string answer = "";
            foreach (ExtraStudent student in result)
            {
                answer += student.Name;
            }
            Assert.AreEqual("SashaMisha", answer);
        }
        
        [Test]
        public void RemoveEntryForOgnpforStudent()
        {
            var student = new ExtraStudent("DIMA");
            var groupName = new GroupName('M', 3, 2, 11);
            var mainGroup = new RegularGroup(groupName);
            _service.AddRegularGroup(mainGroup);
            mainGroup.AddStudent(student);
            var ognpCourse = new OgnpCourse(ExtraFaculty.FT);
            var groupName2 = new GroupName('E', 3, 2, 10);
            var ognpGroup = new OgnpGroup(groupName2);
            ognpGroup.AddStudent(student);
            var flow = new OgnpFlow(ExtraFaculty.FT);
            flow.AddOgnpGroup(ognpGroup);
            ognpCourse.AddFlow(flow);
            _service.AddOgnpCourse(ognpCourse);
            _service.RemoveRecordingFromCcourse(student, ognpCourse);
            Assert.AreEqual(OgnpStatus.NotRecorded, student.OgnpStatus );
        }
        
    }
}