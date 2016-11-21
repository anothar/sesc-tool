using System.Collections.Generic;

namespace SESC.DataParser.ClassTimetable
{
    /// <summary>
    /// ���� ������� ���
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
        /// �������� ���� � ����������
        /// </summary>
        /// <param name="lesson"></param>
        public void AddOneGroupLesson(PrimaryLesson lesson)
        {
            LessonsInGroups.Add(lesson);
            IsWholeClassLesson = false;
        }

        /// <summary>
        /// true - ���� ���� � ����� ������, false - ���� ���� �� ����������
        /// </summary>
        public bool IsWholeClassLesson { get; private set; }
        
        /// <summary>
        /// ���� � ����� ������, null - ���� � ����������
        /// </summary>
        public PrimaryLesson WholeClassLesson { get; }
        
        /// <summary>
        /// ����� � ����������, null - ���� � ����� ������
        /// </summary>
        public List<PrimaryLesson> LessonsInGroups { get; private set; }
    }
}