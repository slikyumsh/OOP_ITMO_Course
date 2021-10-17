using System;
using System.Data;
using Isu;

namespace IsuExtra.Models
{
    public class Lesson
    {
        private readonly DateTime _end;
        private readonly string _name;
        private DateTime _start;
        private char _faculty;
        private Cabinet _cabinet;
        private Professor _professor;
        private Guid _id;

        public Lesson(DateTime start, char faculty, string name, Cabinet cabinet, Professor professor)
        {
            _start = start;
            _end = start.AddHours(1.5);
            _faculty = faculty;
            _name = name;
            _cabinet = cabinet;
            _professor = professor;
            _id = Guid.NewGuid();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Lesson))
            {
                return false;
            }

            return _id == ((Lesson)obj).Id();
        }

        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }

        public DateTime Start() => _start;
        public DateTime End() => _end;
        public char Faculty() => _faculty;
        public string Name() => _name;
        public Guid Cabinet() => _cabinet.Id();
        public Guid Professor() => _professor.Id();
        public Guid Id() => _id;

        public bool IsIntersect(Lesson lesson)
        {
            if (lesson == null)
                throw new ArgumentException("Null-Lesson");
            if (_start < lesson._start && _end > lesson._start)
                return true;
            if (_start > lesson._start && _start < lesson._end)
                return true;
            return false;
        }
    }
}