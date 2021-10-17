using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Models;

namespace IsuExtra.Models
{
    public class OgnpCourse
    {
        private readonly List<OgnpFlow> _flows;
        private char _faculty;
        private Guid _id;

        public OgnpCourse(char faculty)
        {
            _flows = new List<OgnpFlow>();
            _faculty = faculty;
            _id = Guid.NewGuid();
        }

        public Guid Id() => _id;
        public char Faculty() => _faculty;
        public List<OgnpFlow> Flows() => _flows;

        public void AddFlow(OgnpFlow flow)
        {
            OgnpFlow desiredFlow = _flows.SingleOrDefault(desiredFlow => desiredFlow.Id() == flow.Id());
            if (desiredFlow != null)
                throw new ArgumentException("We have already added this flow");
            if (_faculty != flow.Faculty())
                throw new ArgumentException("Invalid faculty");
            _flows.Add(flow);
        }
    }
}