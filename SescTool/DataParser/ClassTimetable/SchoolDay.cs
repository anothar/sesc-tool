using System.Collections.Generic;

namespace SESC.DataParser.ClassTimetable
{
    /// <summary>
    /// ���������� �� ���� ���� ��� ������ ������
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
        /// ���� ������
        /// </summary>
        public string DayOfWeekName { get; private set; }

        /// <summary>
        /// ����� � ������� ���
        /// </summary>
        public List<Lesson> Lessons { get; }
    }
}

