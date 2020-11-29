using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TPT.Exceptions;

namespace TPT.Tests
{
    class UserTests
    {
        TPTClient client;

        [SetUp]
        public void SetUp()
        {
            client = new TPTClient();
        }

        [Test]
        public async Task GetUserInfoAsync()
        {
            UserInfo info = await client.GetUserAsync("CPK");

            Assert.AreEqual("CPK", info.Username, "Username was wrong");
            Assert.AreEqual(185668, info.ID, "User ID was not CPK's ID");
            Assert.AreEqual(1540566062, info.RegisterTime.ToUnixtime(), "Register time was wrong");
        }

        [Test]
        public void ThrowOnUserNotFound()
        {
            Assert.ThrowsAsync<UsernotFoundException>(async delegate { await client.GetUserAsync("CPKdfjöalskdfjöalskdjfölkasjdfaöjf"); });
        }
    }
}
