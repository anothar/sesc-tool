using System;
using System.Threading.Tasks;

namespace SESC.DataParser
{
    /// <summary>
    /// ������ ������ ���������� � ����� �����
    /// </summary>
    public static class TimetableParser
    {
        /// <summary>
        /// ������������ � ����� ������
        /// </summary>
        public static readonly string[] Classes =
        {
            "8�", "8�","8�",
            "9�","9�","9�","9�","9�","9�",
            "10�","10�","10�","10�","10�","10�","10�","10�","10�","10�","10�",
            "11�","11�","11�","11�","11�","11�","11�","11�","11�","11�","11�"
        };

        private static StaticTimetableAPI API=new StaticTimetableAPI(new SESCWebSiteParser());

        /// <summary>
        /// �������� �� ���� form ���������� �� ������ ��� ������� ������
        /// </summary>
        /// <param name="Class">����� ��� �������� �������� ����������</param>
        /// <returns>������ SchoolWeek ��� ������� ������ ������� ������</returns>
        /// <remarks>
        /// ����� ���������� async ������ ��� �������� ������� ������, ������� ����� ����������� �����
        /// </remarks>
        public static async Task<SchoolWeek> GetStaticSchoolWeekOfClass(string Class)
        {
            if (Array.IndexOf(Classes,Class) < 0)
                throw new ArgumentException("Class is invalid");
           return await API.GetStaticClassTimetablePage(Class);
        }
    }
}