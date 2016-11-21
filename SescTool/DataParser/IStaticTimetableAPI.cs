using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
[assembly: InternalsVisibleTo("SESC.DataParser.Tests")]

namespace SESC.DataParser
{
    /// <summary>
    /// Интерфейс для олучения html-документов с сайта СУНЦА
    /// </summary>
    internal interface IStaticTimetableAPI
    {
        /// <summary>
        /// Получает html-страницу с занятостью кабинетов
        /// </summary>
        /// <param name="dayOfWeek">День для которого получаем занятость кабинетов</param>
        /// <returns>html-документ с занятостью кабинетов</returns>
        Task<IHtmlDocument> GetStaticClassroomsEmploymentPage(String dayOfWeek);

        /// <summary>
        /// Получает html-страницу с расписанием класса
        /// </summary>
        /// <param name="className">Класс для которого получаем данные о расписании</param>
        /// <returns>html-докумет с расписанием</returns>
        Task<IHtmlDocument> GetStaticClassTimetablePage(string className);
    }
}