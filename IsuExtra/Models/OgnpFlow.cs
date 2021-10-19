using System;
using System.Collections.Generic;
using System.Linq;

namespace IsuExtra.Models
{
    public class OgnpFlow
    {
        private readonly List<OgnpGroup> _ognpGroups;
        private ExtraFaculty _faculty;
        private Guid _id;

        public OgnpFlow(ExtraFaculty faculty)
        {
            _ognpGroups = new List<OgnpGroup>();
            _faculty = faculty;
            _id = Guid.NewGuid();
        }

        public Guid Id => _id;
        public ExtraFaculty Faculty => _faculty;
        public List<OgnpGroup> ListGroups => _ognpGroups;

        public void AddOgnpGroup(OgnpGroup ognpGroup)
        {
            OgnpGroup desiredOgnpGroup = _ognpGroups.SingleOrDefault(desiredOgnpGroup => desiredOgnpGroup.Id == ognpGroup.Id);
            if (desiredOgnpGroup != null)
                throw new ArgumentException("We already have this ognp flow");
            if (_faculty != ognpGroup.Faculty)
                throw new ArgumentException("Invalid faculty");
            _ognpGroups.Add(ognpGroup);
        }
    }
}