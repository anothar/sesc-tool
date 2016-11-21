using System.Collections.Generic;

namespace SESC.DataParser.ClassroomsEmployment
{
    /// <summary>
	/// Один учебный час для данного дня недели
	/// </summary>
	public class Lesson
    {
        public Lesson(int number)
        {
            LessonNumber = number;
            Classrooms = new List<Classroom>();
            FreeClassrooms = new List<Classroom>();
        }

        public void AddClassroom(Classroom classroom)
        {
            Classrooms.Add(classroom);
            if (classroom.Free)
                FreeClassrooms.Add(classroom);
        }

        /// <summary>
        /// Номер урока по порядку 1-7
        /// </summary>
        public int LessonNumber { get; }
        /// <summary>
        /// Все аудитории
        /// </summary>
        public List<Classroom> Classrooms { get; }
        /// <summary>
        /// Свободные аудитории
        /// </summary>
        public List<Classroom> FreeClassrooms { get; }
    }
}
