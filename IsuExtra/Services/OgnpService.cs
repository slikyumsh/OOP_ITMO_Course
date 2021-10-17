using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Models;

namespace IsuExtra.Models.Models
{
    public class OgnpService
    {
        private readonly List<OgnpCourse> _ognpCourses;
        private readonly List<RegularGroup> _groups;

        public OgnpService()
        {
            _ognpCourses = new List<OgnpCourse>();
            _groups = new List<RegularGroup>();
        }

        public int NumberOfCourses()
        {
            return _ognpCourses.Count;
        }

        public void AddOgnpCourse(OgnpCourse course)
        {
            OgnpCourse desiredCourse = _ognpCourses.SingleOrDefault(desiredCourse => desiredCourse.Id() == course.Id());
            if (desiredCourse != null)
                throw new ArgumentException("We have already added this flow");
            desiredCourse = _ognpCourses.SingleOrDefault(desiredCourse => desiredCourse.Faculty() == course.Faculty());
            if (desiredCourse != null)
                throw new ArgumentException("We have already added flow from this faculty");
            _ognpCourses.Add(course);
        }

        public void AddRegularGroup(RegularGroup group)
        {
            RegularGroup desiredGroup = _groups.SingleOrDefault(desiredGroup => desiredGroup.Id() == group.Id());
            if (desiredGroup != null)
                throw new ArgumentException("We have already added this group");
            desiredGroup = _groups.SingleOrDefault(desiredGroup => desiredGroup.Faculty() == group.Faculty());
            if (desiredGroup != null)
                throw new ArgumentException("We have already added group from this faculty");
            _groups.Add(group);
        }

        public List<OgnpFlow> GetCourseFlows(OgnpCourse course)
        {
            return course.Flows();
        }

        public List<OgnpStudent> GetListOfOgnpStudentsFromThisGroup(OgnpGroup group)
        {
            return group.GetSudentList();
        }

        public List<OgnpStudent> GetListOfNotRecordedStudentsFromThisGroup(RegularGroup group)
        {
            List<OgnpStudent> result = new List<OgnpStudent>();
            foreach (var ognpStudent in group.GetGroupList())
            {
                if (ognpStudent.OgnpStatus() == 0)
                    result.Add(ognpStudent);
            }

            if (result.Count == 0)
                throw new ArgumentException("All students have ognp at this group");
            return result;
        }

        public void EnrollStudentOnCourse(OgnpStudent ognpStudent, OgnpCourse ognpCourse)
        {
            char faculty = '0';
            foreach (var group in _groups)
            {
                if (group.FindStudent(ognpStudent) != null)
                {
                    faculty = group.Faculty();
                    break;
                }
            }

            if (faculty == '0')
                throw new ArgumentException("Can't find out student regular groups faculty");
            foreach (var flow in ognpCourse.Flows())
            {
                foreach (var group in flow.ListGroups())
                {
                    if (faculty != group.Faculty() && group.CanAddStudent(ognpStudent))
                    {
                        group.AddStudent(ognpStudent);
                        return;
                    }
                }
            }
        }

        public void RemoveRecordingFromCcourse(OgnpStudent ognpStudent, OgnpCourse ognpCourse)
        {
            foreach (var flow in ognpCourse.Flows())
            {
                foreach (var group in flow.ListGroups())
                {
                    if (group.CanRemoveStudent(ognpStudent))
                    {
                        group.RemoveStudent(ognpStudent);
                        return;
                    }
                }
            }

            throw new Exception("Can't enroll student on this course");
        }
    }
}