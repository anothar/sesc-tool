using System.Collections.Generic;

namespace SESC.DataParser.ClassTimetable
{
    /// <summary>
    /// Расписание на один день для одного класса
    /// </summary>
    public class SchoolDay
    {
        public SchoolDay()
        {
            Lessons = new List<Lesson>();
        }

        public SchoolDay(string name):this()
        {
            DayOfWeekName = name;
        }

        public void AddLesson(Lesson lesson)
        {
            Lessons.Add(lesson);
        }

        /// <summary>
        /// День недели
        /// </summary>
        public string DayOfWeekName { get; private set; }

        /// <summary>
        /// Уроки в течение дня
        /// </summary>
        public List<Lesson> Lessons { get; }
    }
}

