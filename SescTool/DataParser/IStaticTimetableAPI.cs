using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
[assembly: InternalsVisibleTo("SESC.DataParser.Tests")]

namespace SESC.DataParser
{
    /// <summary>
    /// ��������� ��� �������� html-���������� � ����� �����
    /// </summary>
    internal interface IStaticTimetableAPI
    {
        /// <summary>
        /// �������� html-�������� � ���������� ���������
        /// </summary>
        /// <param name="dayOfWeek">���� ��� �������� �������� ��������� ���������</param>
        /// <returns>html-�������� � ���������� ���������</returns>
        Task<IHtmlDocument> GetStaticClassroomsEmploymentPage(String dayOfWeek);

        /// <summary>
        /// �������� html-�������� � ����������� ������
        /// </summary>
        /// <param name="className">����� ��� �������� �������� ������ � ����������</param>
        /// <returns>html-������� � �����������</returns>
        Task<IHtmlDocument> GetStaticClassTimetablePage(string className);
    }
}