using System;
using System.Collections.Generic;
using System.Linq;

namespace IsuExtra.Models
{
    public class Schedule
    {
        private Dictionary<DayOfWeek, List<Lesson>> _schedule;

        public Schedule()
        {
            _schedule = new Dictionary<DayOfWeek, List<Lesson>>();
            _schedule[DayOfWeek.Sunday] = null;
            _schedule[DayOfWeek.Monday] = null;
            _schedule[DayOfWeek.Tuesday] = null;
            _schedule[DayOfWeek.Wednesday] = null;
            _schedule[DayOfWeek.Thursday] = null;
            _schedule[DayOfWeek.Friday] = null;
            _schedule[DayOfWeek.Saturday] = null;
        }

        public Dictionary<DayOfWeek, List<Lesson>> ListLessons => _schedule;

        public void AddLesson(Lesson lesson, DayOfWeek day)
        {
            if (day == DayOfWeek.Sunday)
                throw new ArgumentException("Invalid Argument: We haven't any lessons at thi day");
            Lesson desiredLesson = _schedule[day].SingleOrDefault(desiredLesson => desiredLesson.Id == lesson.Id);
            if (desiredLesson != null)
                throw new ArgumentException("We can't add Lesson at this day: already have this lesson today");
            _schedule[day].Add(lesson);
        }

        public List<Lesson> GetScheduleOfDay(DayOfWeek day)
        {
            return _schedule[day];
        }

        public bool IsIntersect(Schedule schedule)
        {
            if (_schedule == null && schedule == null)
                throw new ArgumentException("Invalid schedule");
            foreach (DayOfWeek day in DayOfWeek.GetValues(typeof(DayOfWeek)))
            {
                if (_schedule[day] == null && schedule.GetScheduleOfDay(day) == null) continue;
                foreach (Lesson lesson1 in _schedule[day])
                {
                    foreach (Lesson lesson2 in schedule.GetScheduleOfDay(day))
                    {
                        if (lesson1.IsIntersect(lesson2)) return true;
                    }
                }
            }

            return false;
        }

        public void ConcatSchedules(Schedule schedule)
        {
            if (IsIntersect(schedule))
                throw new ArgumentException("Schedules are intersected");
            if (_schedule == null && schedule == null)
                throw new ArgumentException("Invalid schedule");
            foreach (DayOfWeek day in DayOfWeek.GetValues(typeof(DayOfWeek)))
            {
                if (_schedule[day] == null && schedule.GetScheduleOfDay(day) == null) continue;
                _schedule[day].AddRange(schedule.GetScheduleOfDay(day));
            }
        }

        public void DeleteSchedules(Schedule schedule)
        {
            if (IsIntersect(schedule))
                throw new ArgumentException("Schedules are intersected");
            if (_schedule == null && schedule == null)
                throw new ArgumentException("Invalid schedule");
            foreach (DayOfWeek day in DayOfWeek.GetValues(typeof(DayOfWeek)))
            {
                if (_schedule[day] == null && schedule.GetScheduleOfDay(day) == null) continue;
                foreach (Lesson lesson2 in schedule.GetScheduleOfDay(day))
                    {
                        _schedule[day].Remove(lesson2);
                    }
            }
        }
    }
}