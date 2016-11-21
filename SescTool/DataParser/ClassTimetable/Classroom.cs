namespace SESC.DataParser
{
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