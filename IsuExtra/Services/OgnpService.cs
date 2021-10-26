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
            OgnpCourse desiredCourse = _ognpCourses.SingleOrDefault(desiredCourse => desiredCourse.Id == course.Id);
            if (desiredCourse != null)
                throw new ArgumentException("We have already added this flow");
            desiredCourse = _ognpCourses.SingleOrDefault(desiredCourse => desiredCourse.Faculty == course.Faculty);
            if (desiredCourse != null)
                throw new ArgumentException("We have already added flow from this faculty");
            _ognpCourses.Add(course);
        }

        public void AddRegularGroup(RegularGroup group)
        {
            RegularGroup desiredGroup = _groups.SingleOrDefault(desiredGroup => desiredGroup.Id == group.Id);
            if (desiredGroup != null)
                throw new ArgumentException("We have already added this group");
            desiredGroup = _groups.SingleOrDefault(desiredGroup => desiredGroup.Faculty == group.Faculty);
            if (desiredGroup != null)
                throw new ArgumentException("We have already added group from this faculty");
            _groups.Add(group);
        }

        public List<OgnpFlow> GetCourseFlows(OgnpCourse course)
        {
            return course.Flows;
        }

        public List<ExtraStudent> GetListOfOgnpStudentsFromThisGroup(OgnpGroup group)
        {
            return group.GroupList;
        }

        public List<ExtraStudent> GetListOfNotRecordedStudentsFromThisGroup(RegularGroup group)
        {
            List<ExtraStudent> result = new List<ExtraStudent>();
            foreach (var ognpStudent in group.GetGroupList)
            {
                if (ognpStudent.OgnpStatus == 0)
                    result.Add(ognpStudent);
            }

            if (result.Count == 0)
                throw new ArgumentException("All students have ognp at this group");
            return result;
        }

        public ExtraFaculty GetFaculty(ExtraStudent student)
        {
            RegularGroup desiredGroup = _groups.FirstOrDefault(desiredGroup => desiredGroup.FindStudent(student) == student);
            if (desiredGroup == null)
                throw new ArgumentException("Invalid argument: there is no any regular group that contained this student");
            return desiredGroup.Faculty;
        }

        public void EnrollStudentOnCourse(ExtraStudent ognpStudent, OgnpCourse ognpCourse)
        {
            ExtraFaculty faculty = GetFaculty(ognpStudent);

            if (faculty == ExtraFaculty.NoFaculty)
                throw new ArgumentException("Can't find out student regular groups faculty");
            if (ognpCourse == null)
                throw new ArgumentException("There is no any ognp with this name");
            if (ognpStudent == null)
                throw new ArgumentException("There is no such student");
            List<OgnpGroup> selectedGroups = ognpCourse.Flows.SelectMany(flow => @flow.ListGroups).ToList();
            if (!selectedGroups.Any())
                throw new ArgumentException("This ognp course hasn't any groups");

            OgnpGroup desiredGroup =
                selectedGroups.Find(x => x.CanAddStudent(ognpStudent) && x.Faculty != faculty);
            if (desiredGroup == null)
                throw new ArgumentException("This student can't go to one of the groups from this ognp");
            desiredGroup.AddStudent(ognpStudent);
        }

        public void RemoveRecordingFromCcourse(ExtraStudent ognpStudent, OgnpCourse ognpCourse)
        {
            if (ognpCourse == null)
                throw new ArgumentException("There is no any ognp with this name");
            if (ognpStudent == null)
                throw new ArgumentException("There is no such student");
            List<OgnpGroup> selectedGroups = ognpCourse.Flows.SelectMany(flow => @flow.ListGroups).ToList();
            if (!selectedGroups.Any())
                throw new ArgumentException("This ognp course hasn't any groups");
            OgnpGroup desiredGroup =
                selectedGroups.FirstOrDefault(desiredGroup => desiredGroup.CanRemoveStudent(ognpStudent));
            if (desiredGroup == null)
                throw new ArgumentException("This student doesn't go to one of the groups from this ognp");
            desiredGroup.RemoveStudent(ognpStudent);
        }
    }
}