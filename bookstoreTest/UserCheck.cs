using System;
using System.Collections.Generic;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class UserCheck
    {
        private readonly User user1 = new User("Elena Gilbert", "123-456-7890", "elena@gilbert.com", new DateTime(1992, 6, 22));
        private readonly User user2 = new User("Damon Salvatore", "987-654-3210", "damon@salvatore.com", new DateTime(1840, 10, 1));

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
            user1.Name = "Changed Name";
            List<User> users = User.GetUsers();
            Assert.That(users[0].Name, Is.EqualTo("Elena Gilbert"));
        }

        [Test]
        public void CheckExtentPersistency()
        {
            BookstoreFileManager.SaveBookstore();
            User.GetUsers().Clear();
            Assert.That(User.GetUsers().Count, Is.EqualTo(0));
            BookstoreFileManager.LoadBookstore();
            List<User> users = User.GetUsers();
            Assert.That(users.Count, Is.EqualTo(2));
            Assert.That(users[0].Name, Is.EqualTo("Elena Gilbert"));
            Assert.That(users[1].Name, Is.EqualTo("Damon Salvatore"));
        }
    }
}
