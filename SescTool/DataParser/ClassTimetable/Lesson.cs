using System.Collections.Generic;

namespace SESC.DataParser.ClassTimetable
{
    /// <summary>
    /// Один учебный час
    /// </summary>
    public class Lesson
    {
        public Lesson()
        {
            LessonsInGroups = new List<PrimaryLesson>();
        }

        public Lesson(PrimaryLesson lesson)
        {
            WholeClassLesson = lesson;
            IsWholeClassLesson = true;
        }

        /// <summary>
        /// Добавить урок в подгруппах
        /// </summary>
        /// <param name="lesson"></param>
        public void AddOneGroupLesson(PrimaryLesson lesson)
        {
            LessonsInGroups.Add(lesson);
            IsWholeClassLesson = false;
        }

        /// <summary>
        /// true - если урок у всего класса, false - если урок по подгруппам
        /// </summary>
        public bool IsWholeClassLesson { get; private set; }
        
        /// <summary>
        /// Урок у всего класса, null - если в подгруппах
        /// </summary>
        public PrimaryLesson WholeClassLesson { get; }
        
        /// <summary>
        /// Уроки в подгруппах, null - если у всего класса
        /// </summary>
        public List<PrimaryLesson> LessonsInGroups { get; private set; }
    }
}