using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SESC.DataParser.Tests
{
    [TestFixture]
    public class TimetableParserTests
    {
        /// <summary>
        /// Проверяем, что парсер возвращает что-то валидное
        /// </summary>
        [Test]
        public async Task TestGetStaticSchoolWeekOfClass()
        {
            foreach (var schoolClass in TimetableParser.Classes)
            {
                var result = await TimetableParser.GetStaticSchoolWeekOfClass(schoolClass);
                Assert.IsTrue(result.Days.Any());
            }
        }
    }
}
