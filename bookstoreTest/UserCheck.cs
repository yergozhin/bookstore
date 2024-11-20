using System;
using System.Collections.Generic;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class UserCheck
    {
        private User user1;
        private User user2;

        [SetUp]
        public void Setup()
        {
            User.ClearUsers();

            user1 = new User("Elena Gilbert", "123-456-7890", "elena@gilbert.com", new DateTime(1992, 6, 22));
            user2 = new User("Damon Salvatore", "987-654-3210", "damon@salvatore.com", new DateTime(1840, 10, 1));
        }

        [Test]
        public void CheckUserExtent()
        {
            List<User> users = User.GetUsers();
            Assert.That(users.Count, Is.EqualTo(2));
            Assert.That(users[0].Name, Is.EqualTo("Elena Gilbert"));
            Assert.That(users[1].Name, Is.EqualTo("Damon Salvatore"));
        }

        [Test]
        public void CheckEncapsulationInExtent()
        {
            user1.Name = "Caroline Forbes";
            List<User> users = User.GetUsers();
            Assert.That(users[0].Name, Is.EqualTo("Caroline Forbes"));
        }

        [Test]
        public void CheckExtentPersistency()
        {
            BookstoreFileManager.SaveBookstore();
            User.ClearUsers();
            Assert.That(User.GetUsers().Count, Is.EqualTo(0));
            BookstoreFileManager.LoadBookstore();
            List<User> users = User.GetUsers();
            Assert.That(users.Count, Is.EqualTo(2));
            Assert.That(users[0].Name, Is.EqualTo("Elena Gilbert"));
            Assert.That(users[1].Name, Is.EqualTo("Damon Salvatore"));
        }
    }
}
