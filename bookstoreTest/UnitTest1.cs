using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using NUnit.Framework;

namespace Bookstore.@class.Tests
{
    public class Tests
    {
        private readonly User user = new User("John Doe", "123456789", "john.doe@example.com", new DateTime(1990, 1, 1));

        [Test]
        public void TestUserGetInformation()
        {
            Assert.That(user.Name, Is.EqualTo("John Doe"));
            Assert.That(user.PhoneNumber, Is.EqualTo("123456789"));
            Assert.That(user.Email, Is.EqualTo("john.doe@example.com"));
            Assert.That(user.DateOfBirth, Is.EqualTo(new DateTime(1990, 1, 1)));
        }

    }
}