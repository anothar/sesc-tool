namespace SESC.DataParser
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
}
