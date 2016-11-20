using System.IO;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using NUnit.Framework;

namespace SESC.DataParser.Tests
{
    [TestFixture]
    public class StaticTimetableAPITests
    {
        [Test]
        public async Task Test10vSchedule()
        {
            var api=new StaticTimetableAPI(new SESCWebSiteParser());
            var schedule = await api.GetStaticClassTimetablePage("10в");
            Assert.AreEqual(6,schedule.Days.Count);
        }

        private class SESCWebSiteParser:ISESCWebSiteParser
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
