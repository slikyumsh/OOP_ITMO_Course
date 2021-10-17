using System;
using System.Collections.Generic;
using System.Linq;

namespace IsuExtra.Models
{
    public class Schedule
    {
        private List<Lesson>[] _schedule;

        public Schedule()
        {
            _schedule = new List<Lesson>[6];
        }

        public List<Lesson>[] ListLessons() => _schedule;

        public void AddLesson(Lesson lesson, int day)
        {
            if (day > 5 || day < 0)
                throw new ArgumentException("Invalid Argument: Incorrect format of a day");
            Lesson desiredLesson = _schedule[day].SingleOrDefault(desiredLesson => desiredLesson.Id() == lesson.Id());
            if (desiredLesson != null)
                throw new ArgumentException("We can't add Lesson at this day: already have this lesson today");
            _schedule[day].Add(lesson);
        }

        public List<Lesson> GetScheduleOfDay(int day)
        {
            if (day > 5 || day < 0)
                throw new ArgumentException("Invalid Argument: Incorrect format of a day");
            return _schedule[day];
        }

        public bool IsIntersect(Schedule schedule)
        {
            if (_schedule == null && schedule == null)
                return true;
            for (int i = 0; i < 6; i++)
            {
                if (_schedule[i] == null && schedule.GetScheduleOfDay(i) == null) continue;
                foreach (Lesson lesson1 in _schedule[i])
                {
                    foreach (Lesson lesson2 in schedule.GetScheduleOfDay(i))
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
            for (int i = 0; i < 6; i++)
            {
                List<Lesson> todaySchedule = GetScheduleOfDay(i);
                if (todaySchedule == null) continue;
                foreach (Lesson lesson2 in todaySchedule)
                {
                    _schedule[i].Add(lesson2);
                }
            }
        }

        public void DeleteSchedules(Schedule schedule)
        {
            for (int i = 0; i < 6; i++)
            {
                List<Lesson> todaySchedule = schedule.GetScheduleOfDay(i);
                if (todaySchedule == null) continue;
                foreach (Lesson lesson2 in todaySchedule)
                {
                    _schedule[i].Remove(lesson2);
                }
            }
        }
    }
}