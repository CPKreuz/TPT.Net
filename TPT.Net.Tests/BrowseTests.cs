using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TPT.Tests
{
    class BrowseTests
    {
        TPTClient client;

        [SetUp]
        public void SetUp()
        {
            client = new TPTClient();
        }

        [Test]
        public async Task GetBrowseResultTest()
        {
            BrowseResult result = await client.GetFrontPage();

            Assert.AreNotEqual(0, result.Saves.Count, "Front Page returned zero pages");
            Assert.AreNotEqual(0, result.TotalPages, "Total pages returned zero");
        }
    }
}
