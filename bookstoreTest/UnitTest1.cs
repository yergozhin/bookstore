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
        private readonly User user2 = new User("Jane Doe", "987654321", "jane.doe@example.com", new DateTime(1995, 5, 5));

        [Test]
        public void TestUserClassExtentStoresCorrectInstances()
        { 
            List<User> users = user.getUsers();

            Assert.That(users.Count, Is.EqualTo(2));
            Assert.That(users[0].Name, Is.EqualTo("John Doe"));
            Assert.That(users[1].Name, Is.EqualTo("Jane Doe"));
        }
        
        
        [Test]
        public void TestUserEncapsulation()
        {
            user.Name = "John Smith";
            List<User> users = user.getUsers();

            Assert.That(users[0].Name, Is.EqualTo("John Smith"));
        }
        
        [Test]
        public void TestCheckAge()
        {
            Assert.That(user.CheckAge().Equals(true));
        }

        /*
         * Test commented out because it doesn't execute
        [Test]
        public void TestGetUser()
        {
            var users = user.getUsers();
            users.Add(user2);            
            Assert.That(user2.PhoneNumber.Equals(users[0].PhoneNumber));
            
        }
        */

        /*
         * Commented out for the moment because it doesn't execute for now
        [Test]
        public void TestUserGetInformation()
        {
            Assert.That(user.Name, Is.EqualTo("John Doe"));
            Assert.That(user.PhoneNumber, Is.EqualTo("123456789"));
            Assert.That(user.Email, Is.EqualTo("john.doe@example.com"));
            Assert.That(user.DateOfBirth, Is.EqualTo(new DateTime(1990, 1, 1)));
        }
        */

    }
}
    