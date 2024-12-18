using System;
using System.Collections.Generic;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class UserTests
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
        public void CheckUserAttributes()
        {
            Assert.That(user1.Name, Is.EqualTo("Elena Gilbert"));
            Assert.That(user1.PhoneNumber, Is.EqualTo("123-456-7890"));
            Assert.That(user1.Email, Is.EqualTo("elena@gilbert.com"));
            Assert.That(user1.DateOfBirth, Is.EqualTo(new DateTime(1992, 6, 22)));
        }

        [Test]
        public void CheckEmptyName_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => new User("", "555-555-5555", "test@user.com", new DateTime(2000, 1, 1)));
        }

        [Test]
        public void CheckInvalidEmail_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => new User("Stefan Salvatore", "555-555-5555", "stefansalvatore.com", new DateTime(1846, 11, 1)));
        }

        [Test]
        public void CheckDuplicateEmail_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => new User("Klaus Mikaelson", "555-123-4567", "elena@gilbert.com", new DateTime(1000, 5, 5)));
        }

        [Test]
        public void FindUser_ShouldReturnCorrectUser()
        {
            User foundUser = User.FindUser("elena@gilbert.com");

            Assert.That(foundUser, Is.EqualTo(user1));
            Assert.That(foundUser.Name, Is.EqualTo("Elena Gilbert"));
        }

        [Test]
        public void FindUser_ShouldReturnNull_WhenUserNotFound()
        {
            User foundUser = User.FindUser("unknown@user.com");
            Assert.That(foundUser, Is.Null);
        }

        [Test]
        public void UpdateUser_ShouldUpdateAttributes()
        {
            user1.UpdateUser(newName: "Elena Salvatore", newPhoneNumber: "555-555-5555", newEmail: "elena@salvatore.com");

            Assert.That(user1.Name, Is.EqualTo("Elena Salvatore"));
            Assert.That(user1.PhoneNumber, Is.EqualTo("555-555-5555"));
            Assert.That(user1.Email, Is.EqualTo("elena@salvatore.com"));
        }

        [Test]
        public void UpdateUser_ShouldThrowException_WhenEmailAlreadyExists()
        {
            Assert.Throws<InvalidOperationException>(() => user1.UpdateUser(newEmail: "damon@salvatore.com"));
        }

        [Test]
        public void CheckUserExtent()
        {
            List<User> users = User.GetUsers();

            Assert.That(users.Count, Is.EqualTo(2));
            Assert.That(users[0], Is.EqualTo(user1));
            Assert.That(users[1], Is.EqualTo(user2));
        }

        [Test]
        public void ClearUsers_ShouldClearExtent()
        {
            User.ClearUsers();

            Assert.That(User.GetUsers().Count, Is.EqualTo(0));
        }
    }
}
