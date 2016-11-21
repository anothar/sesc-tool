using System.Collections.Generic;

namespace SESC.DataParser.ClassroomsEmployment
{
    /// <summary>
    /// Расписание на один день для одной аудитории
    /// </summary>
    public class SchoolDay
    {
        public SchoolDay(string name)
        {
            DayOfWeekName = name;
            Lessons = new List<Lesson>();
        }

        public void AddLesson(Lesson lesson)
        {
            Lessons.Add(lesson);
        }

        /// <summary>
        /// День недели Понедельник-Суббота
        /// </summary>
        public string DayOfWeekName { get; }
        /// <summary>
        /// Уроки в этот день недели 1-7
        /// </summary>
        public List<Lesson> Lessons { get; }
    }
}
