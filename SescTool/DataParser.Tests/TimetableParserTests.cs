using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using NUnit.Framework;

namespace SESC.DataParser.Tests
{
    [TestFixture]
    public class TimetableParserTests
    {
        /// <summary>
        /// Проверяем парсинг расписания для 10в
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Test10vSchedule()
        {
            var api=new TimetableParser(new SESCWebSiteParser());
            var schedule = await api.GetStaticClassTimetablePage("10в");
            Assert.AreEqual(6,schedule.Days.Count);
        }

        /// <summary>
        /// Проверяем, что парсер возвращает что-то валидное
        /// </summary>
        [Test]
        public async Task TestWithWebsiteParser()
        {
            foreach (var schoolClass in TimetableParser.Classes)
            {
                var result = await TimetableParser.Instance.GetStaticSchoolWeekOfClass(schoolClass);
                Assert.IsTrue(result.Days.Any());
            }
        }

        private class SESCWebSiteParser:IStaticTimetableAPI
        {
            public async Task<IHtmlDocument> GetStaticClassroomsEmploymentPage(string dayOfWeek)
            {
                throw new System.NotImplementedException();
            }

            public async Task<IHtmlDocument> GetStaticClassTimetablePage(string className)
            {
               return await new HtmlParser().ParseAsync(File.ReadAllText(TestContext.CurrentContext.TestDirectory+"\\Data\\" + className+".html"));
            }
        }
    }

}
