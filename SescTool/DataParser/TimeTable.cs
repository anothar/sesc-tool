using System.Collections.Generic;

namespace ClassTimetable
{
	/// <summary>
	/// Урок в одной подгруппе
	/// </summary>
	public class PrimaryLesson
	{
		public PrimaryLesson(string name)
		{
			Name = name;
			Classroom = "";
		}

		public PrimaryLesson(string name, string classroom)
		{
			Name = name;
			Classroom = classroom;
		}

		/// <summary>
		/// Название урока
		/// </summary>
		public string Name { get; }
		/// <summary>
		/// Аудитория в которой урок, null - если нет
		/// </summary>
		public string Classroom { get; }
	}

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

	/// <summary>
	/// Расписание на один день для одного класса
	/// </summary>
	public class SchoolDay
	{
		public SchoolDay()
		{
			Lessons = new List<Lesson>();
		}

		public SchoolDay(string name)
        {
            Lessons = new List<Lesson>();
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

namespace ClassroomsEmployment
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
			if(classroom.Free)
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

	/// <summary>
	/// Класс хранящий описание аудитории в данный урок
	/// </summary>
	public class Classroom
	{
		/// <summary />
		/// <param name="audience">Номер аудитории</param>
		public Classroom(string audience)
		{
			Audience = audience;
			Free = true;
		}

		/// <summary />
		/// <param name="audience">Номер аудитории</param>
		/// <param name="сlass">Класс в аудитории</param>
		/// <param name="lesson">Название урока</param>
		/// <param name="teacher">Учитель проводящий урок</param>
		public Classroom(string audience, string сlass, string lesson, string teacher)
		{
			Audience = audience;
			Class = сlass;
			Subject = lesson;
			Teacher = teacher;
			Free = false;
		}

		/// <summary>
		/// Принимает значение true - если кабинет свободен, иначе - false
		/// </summary>
		public bool Free { get; }
		/// <summary>
		/// Номер аудитории
		/// </summary>
		public string Audience { get; }
		/// <summary>
		/// Класс у которого урок в этом кабинете, null - если кабинет свободен
		/// </summary>
		public string Class { get; }
		/// <summary>
		/// Учитель который ведет урок, null - если кабинет свободен
		/// </summary>
		public string Teacher { get; }
		/// <summary>
		/// Название урока, null - если кабинет свободен
		/// </summary>
		public string Subject { get; }
	}
}