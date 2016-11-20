using System.Collections.Generic;

namespace SESC.DataParser
{
    /// <summary>
    /// Объект хранит расписание на неделю для одного класса
    /// </summary>
    public class SchoolWeek
    {
        public SchoolWeek(string name)
        {
            Class = name;
            Days = new List<SchoolDay>();
        }

        public void AddSchoolDay(SchoolDay day)
        {
            Days.Add(day);
        }

        /// <summary>
        /// Класс
        /// </summary>
        public string Class { get; private set; }

        /// <summary>
        /// Дни с уроками у данного класса
        /// </summary>
        public List<SchoolDay> Days { get; }
    }
}