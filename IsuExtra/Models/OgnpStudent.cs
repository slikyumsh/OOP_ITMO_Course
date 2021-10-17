using System;
using Isu.Models;

namespace IsuExtra.Models
{
    public class OgnpStudent : Student
    {
        private Schedule _personalSchedule;
        private int _ognpStatus;

        public OgnpStudent(string name)
            : base(name)
        {
            _personalSchedule = new Schedule();
            _ognpStatus = 0;
        }

        public void SetRegularSchedule(Schedule schedule)
        {
            if (schedule == null)
                throw new ArgumentException("Invalid schedule");
            _personalSchedule = schedule;
        }

        public int OgnpStatus() => _ognpStatus;
        public Schedule PersonalSchedule() => _personalSchedule;

        public void IncrementOgnpStatus()
        {
            if (_ognpStatus == 2)
                throw new Exception("We have alredy chosen two OGNP");
            _ognpStatus++;
        }

        public void DecrementOgnpStatus()
        {
            if (_ognpStatus == 0)
                throw new Exception("We haven't alredy chosen any OGNP");
            _ognpStatus--;
        }
    }
}