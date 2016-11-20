using System;
using System.Threading.Tasks;

namespace SESC.DataParser
{
    /// <summary>
    /// ѕарсер данных полученных с сайта —”Ќ÷а
    /// </summary>
    public static class TimetableParser
    {
        /// <summary>
        /// —уществующие в лицее классы
        /// </summary>
        public static readonly string[] Classes =
        {
            "8а", "8л","8с",
            "9а","9б","9в","9е","9л","9с",
            "10а","10б","10в","10г","10д","10е","10з","10к","10л","10м","10с",
            "11а","11б","11в","11г","11д","11е","11з","11к","11л","11м","11с"
        };

        private static StaticTimetableAPI API=new StaticTimetableAPI(new SESCWebSiteParser());

        /// <summary>
        /// ѕолучает из тега form расписание на неделю дл€ данного класса
        /// </summary>
        /// <param name="Class"> ласс дл€ которого получаем расписание</param>
        /// <returns>ќбъект SchoolWeek дл€ учебной недели данного класса</returns>
        /// <remarks>
        /// ћетод использует async потому что содержит фоновые задачи, которые могут выполн€тьс€ долго
        /// </remarks>
        public static async Task<SchoolWeek> GetStaticSchoolWeekOfClass(string Class)
        {
            if (Array.IndexOf(Classes,Class) < 0)
                throw new ArgumentException("Class is invalid");
           return await API.GetStaticClassTimetablePage(Class);
        }
    }
}