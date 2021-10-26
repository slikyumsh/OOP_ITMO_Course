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
            _schedule[DayOfWeek.Sunday] = new List<Lesson>();
            _schedule[DayOfWeek.Monday] = new List<Lesson>();
            _schedule[DayOfWeek.Tuesday] = new List<Lesson>();
            _schedule[DayOfWeek.Wednesday] = new List<Lesson>();
            _schedule[DayOfWeek.Thursday] = new List<Lesson>();
            _schedule[DayOfWeek.Friday] = new List<Lesson>();
            _schedule[DayOfWeek.Saturday] = new List<Lesson>();
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

        public bool CheckEmptySchedule()
        {
            int count = 0;
            foreach (DayOfWeek day in DayOfWeek.GetValues(typeof(DayOfWeek)))
            {
                count += _schedule[day].Count;
            }

            if (count == 0) return true;
            return false;
        }

        public List<Lesson> GetScheduleOfDay(DayOfWeek day)
        {
            return _schedule[day];
        }

        public bool IsIntersect(Schedule schedule)
        {
            if (_schedule == null)
                throw new ArgumentException("Invalid schedule");
            if (schedule == null)
                throw new ArgumentException("Invalid schedule");
            foreach (DayOfWeek day in DayOfWeek.GetValues(typeof(DayOfWeek)))
            {
                if (_schedule[day].Count == 0 && schedule.GetScheduleOfDay(day).Count == 0) continue;
                foreach (Lesson lesson1 in GetScheduleOfDay(day))
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