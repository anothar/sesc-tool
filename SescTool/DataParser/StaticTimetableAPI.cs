using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using Extreme.Net;

//eav чтобы тесты видели внутренний класс!
[assembly: InternalsVisibleTo("SESC.DataParser.Tests")]

namespace SESC.DataParser
{
    /// <summary>
    /// Внутрення реализация интерфейса получения html-документов с сайта СУНЦА
    /// </summary>
    internal class StaticTimetableAPI : IStaticTimetableAPI
    {
        /// <summary>
        /// ССылка на страницу с расписанием на сайте СУНЦа
        /// </summary>
        private const string LyceumUrl = "http://lyceum.urfu.ru/study/?id=11";

        /// <summary>
	    /// Получает html-страницу с занятостью кабинетов
	    /// </summary>
	    /// <param name="dayOfWeek">День для которого получаем занятость кабинетов</param>
	    /// <returns>html-документ с занятостью кабинетов</returns>
        public async Task<IHtmlDocument> GetStaticClassroomsEmploymentPage(String dayOfWeek)
        {
            var req = new HttpRequest {CharacterSet = Encoding.GetEncoding("windows-1251")};

            var reqParams = new RequestParams
            {
                ["form"] = "tmtblF1",
                ["tmtblTip"] = "0",
                ["form"] = "tmtblF5",
                ["showKla"] = dayOfWeek
            };

            var resp = await req.PostAsync(LyceumUrl, reqParams);
            var respString = resp.ToString();

            return await new AngleSharp.Parser.Html.HtmlParser().ParseAsync(respString);
        }

        /// <summary>
        /// Получает html-страницу с расписанием класса
        /// </summary>
        /// <param name="className">Класс для которого получаем данные о расписании</param>
        /// <returns>html-докумет с расписанием</returns>
        public async Task<IHtmlDocument> GetStaticClassTimetablePage(string className)
        {

            var req = new HttpRequest {CharacterSet = Encoding.GetEncoding("windows-1251")};

            var reqParams = new RequestParams
            {
                ["form"] = "tmtblF1",
                ["tmtblTip"] = "0",
                ["form"] = "tmtblF5",
                ["showKla"] = className
            };

            var resp = await req.PostAsync(LyceumUrl, reqParams);
            var respString = resp.ToString();

            return await new AngleSharp.Parser.Html.HtmlParser().ParseAsync(respString);
        }
    }
}
