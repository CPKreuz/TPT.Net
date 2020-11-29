using NUnit.Framework;
using System.Threading.Tasks;
using TPT;
using TPT.Exceptions;

namespace TPT.Tests
{
    public class SaveTests
    {
        TPTClient client;
        const int testSaveId = 2617661;

        [SetUp]
        public void SetUp()
        {
            client = new TPTClient();
        }

        [Test]
        public async Task GetSaveAsyncTest()
        {
            SaveInfo save = await client.GetSaveInfoAsync(testSaveId);

            Assert.AreEqual(testSaveId, save.ID, "Save ID was wrong");
            Assert.AreEqual("CPK", save.AuthorName , "Save Author was wrong");
            Assert.AreEqual(Extensions.FromUnixtime(1606577751), save.DateCreated, "Date was not parsed correctly");
            Assert.IsNull(save.Description, "Save description was not null");
        }

        [Test]
        public void ThrowSaveNotFoundTest()
        {
            Assert.ThrowsAsync<SaveNotFoundException>(async delegate { SaveInfo save = await client.GetSaveInfoAsync(-1); }, "Did not throw SaveNotFound exception");
        }
    }
}
