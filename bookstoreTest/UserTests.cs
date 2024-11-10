using System;
using System.Collections.Generic;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class UserTests
    {
        private readonly User user1 = new User("Elena Gilbert", "123-456-7890", "elena@gilbert.com", new DateTime(1992, 6, 22));
        private readonly User user2 = new User("Damon Salvatore", "987-654-3210", "damon@salvatore.com", new DateTime(1840, 10, 1));

        [Test]
        public void CheckUser()
        {
            Assert.That(user1.Name, Is.EqualTo("Elena Gilbert"));
            Assert.That(user1.PhoneNumber, Is.EqualTo("123-456-7890"));
            Assert.That(user1.Email, Is.EqualTo("elena@gilbert.com"));
            Assert.That(user1.DateOfBirth, Is.EqualTo(new DateTime(1992, 6, 22)));
        }

        [Test]
        public void CheckEmptyName()
        {
            Assert.Throws<ArgumentException>(() => new User("", "555-555-5555", "test@user.com", new DateTime(2000, 1, 1)));
        }

        [Test]
        public void CheckInvalidEmail()
        {
            Assert.Throws<ArgumentException>(() => new User("Stefan Salvatore", "555-555-5555", "stefansalvatore.com", new DateTime(1846, 11, 1)));
        }

        [Test]
        public void CheckUserWithAnotherData()
        {
            Assert.That(user2.Name, Is.EqualTo("Damon Salvatore"));
            Assert.That(user2.PhoneNumber, Is.EqualTo("987-654-3210"));
            Assert.That(user2.Email, Is.EqualTo("damon@salvatore.com"));
            Assert.That(user2.DateOfBirth, Is.EqualTo(new DateTime(1840, 10, 1)));
        }
    }
}
