using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;

//eav чтобы тесты видели внутренний класс!
[assembly: InternalsVisibleTo("SESC.DataParser.Tests")]

namespace SESC.DataParser
{
	/// <summary>
	/// Работа с сайтом СУНЦа, для получения расписания
	/// </summary>
	internal class StaticTimetableAPI
	{
	    private readonly ISESCWebSiteParser _parser;

	    public StaticTimetableAPI(ISESCWebSiteParser parser)
	    {
	        _parser = parser;
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
			switch(str)
			{
				case "Понедельник": return "1";
				case "Вторник": return "2";
				case "Среда": return "3";
				case "Четверг": return "4";
				case "Пятница": return "5";
				default: return "6";
			}
		}

		
	}
}