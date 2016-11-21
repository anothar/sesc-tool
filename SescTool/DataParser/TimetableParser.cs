using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using SESC.DataParser.ClassTimetable;

//eav чтобы тесты видели внутренний класс!
[assembly: InternalsVisibleTo("SESC.DataParser.Tests")]

namespace SESC.DataParser
{
    /// <summary>
    /// Парсер данных полученных с сайта СУНЦа
    /// </summary>
    public class TimetableParser
    {
        private readonly IStaticTimetableAPI _parser;

        /// <summary>
        /// Конструктмо с передачей класса для получения html-документов
        /// </summary>
        /// <param name="parser"></param>
	    internal TimetableParser(IStaticTimetableAPI parser)
        {
            _parser = parser;
        }

        public static  TimetableParser Instance { get; }=new TimetableParser(new StaticTimetableAPI());

        /// <summary>
        /// Существующие в лицее классы
        /// </summary>
        public static readonly string[] Classes =
        {
            "8а", "8л","8с",
            "9а","9б","9в","9е","9л","9с",
            "10а","10б","10в","10г","10д","10е","10з","10к","10л","10м","10с",
            "11а","11б","11в","11г","11д","11е","11з","11к","11л","11м","11с"
        };

        #region ClassTimetable

        /// <summary>
        /// Получает из тега form расписание на неделю для данного класса
        /// </summary>
        /// <param name="Class">Класс для которого получаем расписание</param>
        /// <returns>Объект SchoolWeek для учебной недели данного класса</returns>
        public async Task<SchoolWeek> GetStaticSchoolWeekOfClass(string Class)
        {
            var document = await _parser.GetStaticClassTimetablePage(Class);
            var schoolWeek = new SchoolWeek(Class);

            foreach (var item in document.GetElementsByClassName("tmtbl"))
                if (IsSchoolDayOfClass(item))
                    schoolWeek.AddSchoolDay(GetSchoolDayOfClass(item));

            return schoolWeek;
        }
        /// <summary>
        /// Метод проверяет является ли table таблицей с уроками или с пояснениями
        /// </summary>
        /// <param name="table">Тег table со значением class="tmtbl"</param>
        /// <returns>true - если в table храняться уроки, иначе - false</returns>
        private bool IsSchoolDayOfClass(IElement table)
        {
            var style = table.GetAttribute("style");
            return style == null;
        }

        /// <summary>
		/// Получает из тегов h3 и table день недели и уроки в нем
		/// </summary>
		/// <param name="element">Тег table с днем недели</param>
		/// <returns>Объект SchoolDay для данного дня недели</returns>
		private SchoolDay GetSchoolDayOfClass(IElement element)
        {
            var dayOfWeekName = element.PreviousSibling;
            var schoolDay = new SchoolDay(dayOfWeekName.TextContent);

            foreach (var item in element.GetElementsByTagName("tr"))
                if (IsLessonOfClass(item))
                    schoolDay.AddLesson(GetLessonOfClass(item));

            return schoolDay;
        }

        /// <summary>
        /// Проверяет является ли содержимое тега tr уроком
        /// </summary>
        /// <param name="tr">Тег tr</param>
        /// <returns>true - в tr уроки, иначе - false</returns>
        private bool IsLessonOfClass(IParentNode tr)
        {
            var x = tr.FirstElementChild.NodeName;
            return x != "TH";
        }

        /// <summary>
        /// Получает из тега tr набор PrimaryLesson для урока по группам, или один PrimaryLesson для урока всем классом
        /// </summary>
        /// <param name="element">Тег tr с набором уроков в td</param>
        /// <returns>Объект Lesson для данного номера урока</returns>
        private Lesson GetLessonOfClass(IParentNode element)
        {
            switch (element.Children[1].GetAttribute("class"))
            {
                case "yok":
                    return null;
                case "cv":
                    return new Lesson(GetOneGroupLessonOfClass(element.Children[1]));
                default:
                    var lesson = new Lesson();
                    foreach (var item in element.Children)
                        if (IsPrimaryLessonOfClass(item))
                            lesson.AddOneGroupLesson(GetOneGroupLessonOfClass(item));
                    return lesson;
            }
        }

        /// <summary>
        /// Проверяет является ли содержимое тега td уроком
        /// </summary>
        /// <param name="td">Тег td</param>
        /// <returns>true - если в td урок</returns>
        private bool IsPrimaryLessonOfClass(IElement td)
        {
            var elementClass = td.GetAttribute("class");
            return elementClass != "whi";
        }

        /// <summary>
        /// Получает из тега td название урока и номер кабинет, для урока в одной группе
        /// </summary>
        /// <param name="element">Тег td с названием урока и номером кабинета</param>
        /// <returns>Возвращает объект PrimaryLesson для данного урока</returns>
        private PrimaryLesson GetOneGroupLessonOfClass(IElement element)
        {
            var str = element.InnerHtml;
            if (str == "&nbsp;")
                return null;

            var x = str.Split(' ');
            return x.Length == 1 ? new PrimaryLesson(x[0]) : new PrimaryLesson(x[0], x[1]);
        }

        /// <summary>
        /// Получает html-страницу с расписанием класса
        /// </summary>
        /// <param name="className">Класс для которого получаем данные о расписании</param>
        /// <returns>Расписание на неделю</returns>
        public async Task<SchoolWeek> GetStaticClassTimetablePage(string className)
        {
            if (Array.IndexOf(Classes, className) < 0)
                throw new ArgumentException("Class is invalid",nameof(className));
            var document = await _parser.GetStaticClassTimetablePage(className);
            var schoolWeek = new SchoolWeek(className);

            foreach (var item in document.GetElementsByClassName("tmtbl"))
                if (IsSchoolDayOfClass(item))
                    schoolWeek.AddSchoolDay(GetSchoolDayOfClass(item));

            return schoolWeek;
        }



        /// <summary>
        /// Получает html-страницу с занятостью кабинетов
        /// </summary>
        /// <param name="dayOfWeek">День для которого получаем занятость кабинетов</param>
        /// <returns>html-докумет с занятостью кабинетов</returns>
        public async Task<IHtmlDocument> GetStaticClassroomsEmploymentPage(string dayOfWeek)
        {
            dayOfWeek = DayOfWeekToInt(dayOfWeek);
            return await _parser.GetStaticClassroomsEmploymentPage(dayOfWeek);
        }

        /// <summary>
        /// Получает из дня недели его номер
        /// </summary>
        /// <param name="str">Название дня недели по-русски</param>
        /// <returns>Номер дня недели</returns>
        private static string DayOfWeekToInt(string str)
        {
            switch (str)
            {
                case "Понедельник": return "1";
                case "Вторник": return "2";
                case "Среда": return "3";
                case "Четверг": return "4";
                case "Пятница": return "5";
                default: return "6";
            }
        }
        #endregion


        #region ClassroomsEmployment

        public async Task<ClassroomsEmployment.SchoolDay> GetStaticSchoolDayOfClassroomsEmployment(string dayOfWeek)
        {
            var document = await _parser.GetStaticClassroomsEmploymentPage(dayOfWeek);
            var schoolDay = new ClassroomsEmployment.SchoolDay(dayOfWeek);

            var arr = document.GetElementsByClassName("tmtbl");
            foreach (var item in arr)
                schoolDay.AddLesson(GetLessonOfClassroomsEmployment(item));

            return schoolDay;
        }

        private static ClassroomsEmployment.Lesson GetLessonOfClassroomsEmployment(IElement element)
        {
            var number = GetLessonNumberOfClassroomsEmployment(element);
            var lesson = new ClassroomsEmployment.Lesson(number);

            var classrooms = element.FirstElementChild.Children;
            foreach (var item in classrooms)
                if (IsClassroomDescription(item))
                    lesson.AddClassroom(GetClassroomDescription(item));

            return lesson;
        }

        private static int GetLessonNumberOfClassroomsEmployment(IElement element)
        {
            return Convert.ToInt32(element.PreviousElementSibling.InnerHtml[0].ToString());
        }

        private static bool IsClassroomDescription(IElement tr)
        {
            var x = tr.FirstElementChild.NodeName;
            return x != "TH";
        }

        private static ClassroomsEmployment.Classroom GetClassroomDescription(IElement element)
        {
            var audience = element.FirstElementChild.FirstElementChild.InnerHtml;
            var free = element.Children[1].InnerHtml == "&nbsp;";
            if (free)
                return new ClassroomsEmployment.Classroom(audience);

            var _class = element.Children[1].InnerHtml;
            var lesson = element.Children[2].InnerHtml;
            var teacher = element.Children[3].InnerHtml;

            return new ClassroomsEmployment.Classroom(audience, _class, lesson, teacher);
        }

        #endregion
    }
}