using System;
using Isu.Models;

namespace IsuExtra.Models
{
    public class ExtraStudent : Student
    {
        private Schedule _personalSchedule;
        private OgnpStatus _ognpStatus;

        public ExtraStudent(string name)
            : base(name)
        {
            _personalSchedule = new Schedule();
            _ognpStatus = 0;
        }

        public OgnpStatus OgnpStatus => _ognpStatus;
        public Schedule PersonalSchedule => _personalSchedule;

        public void SetRegularSchedule(Schedule schedule)
        {
            if (schedule == null)
                throw new ArgumentException("Invalid schedule");
            _personalSchedule = schedule;
        }

        public void IncrementOgnpStatus()
        {
            if (_ognpStatus == OgnpStatus.TwoOgnp)
                throw new Exception("We have alredy chosen two OGNP");
            _ognpStatus++;
        }

        public void DecrementOgnpStatus()
        {
            if (_ognpStatus == OgnpStatus.NotRecorded)
                throw new Exception("We haven't alredy chosen any OGNP");
            _ognpStatus--;
        }
    }
}