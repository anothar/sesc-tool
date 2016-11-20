﻿using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SESC.DataParser.Tests
{
    [TestFixture]
    public class SESCWebSiteParserTests
    {
        [Test]
        public async Task TestGetStaticClassTimetablePage()
        {
            var parser=new SESCWebSiteParser();
            foreach (var schoolClass in TimetableParser.Classes)
            {
                var htmlDocument=await parser.GetStaticClassTimetablePage(schoolClass);
                Assert.IsTrue(htmlDocument.GetElementsByClassName("tmtbl").Any());
            }
        }
    }
}
