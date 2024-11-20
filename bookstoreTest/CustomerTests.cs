using System;
using System.Collections.Generic;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class CustomerTests
    {
        private readonly Customer customer1 = new Customer("Elena Gilbert", "123-456-7890", "elena@gilbert.com", new DateTime(1992, 6, 22), "Mystic Falls, VA");
        private readonly Customer customer2 = new Customer("Stefan Salvatore", "987-654-3210", "stefan@salvatore.com", new DateTime(1847, 11, 1), "Salvatore Mansion, Mystic Falls");

        [Test]
        public void CheckCustomerAttributes()
        {
            Assert.That(customer1.Name, Is.EqualTo("Elena Gilbert"));
            Assert.That(customer1.PhoneNumber, Is.EqualTo("123-456-7890"));
            Assert.That(customer1.Email, Is.EqualTo("elena@gilbert.com"));
            Assert.That(customer1.DateOfBirth, Is.EqualTo(new DateTime(1992, 6, 22)));
            Assert.That(customer1.Address, Is.EqualTo("Mystic Falls, VA"));
        }

        [Test]
        public void CheckEmptyAddressException()
        {
            Assert.Throws<ArgumentException>(() => new Customer("Damon Salvatore", "555-555-5555", "damon@salvatore.com", new DateTime(1840, 10, 1), ""));
        }

        [Test]
        public void CheckAnotherCustomer()
        {
            Assert.That(customer2.Name, Is.EqualTo("Stefan Salvatore"));
            Assert.That(customer2.PhoneNumber, Is.EqualTo("987-654-3210"));
            Assert.That(customer2.Email, Is.EqualTo("stefan@salvatore.com"));
            Assert.That(customer2.DateOfBirth, Is.EqualTo(new DateTime(1847, 11, 1)));
            Assert.That(customer2.Address, Is.EqualTo("Salvatore Mansion, Mystic Falls"));
        }

        [Test]
        public void CheckCustomerExtent()
        {
            List<Customer> customers = Customer.GetUsers().ConvertAll(user => (Customer)user);
            Assert.That(customers.Count, Is.EqualTo(2));
            Assert.That(customers[0].Name, Is.EqualTo("Elena Gilbert"));
            Assert.That(customers[1].Name, Is.EqualTo("Stefan Salvatore"));
        }

        [Test]
        public void CheckEncapsulationInExtent()
        {
            customer1.Name = "Niklaus Mikkaleson";
            List<Customer> customers = Customer.GetUsers().ConvertAll(user => (Customer)user);
            Assert.That(customers[0].Name, Is.EqualTo("Niklaus Mikkaleson"));
        }

        [Test]
        public void CheckExtentPersistency()
        {
            BookstoreFileManager.SaveBookstore();
            Customer.ClearUsers();
            Assert.That(Customer.GetUsers().Count, Is.EqualTo(0));
            BookstoreFileManager.LoadBookstore();
            List<Customer> customers = Customer.GetUsers().ConvertAll(user => (Customer)user);
            Assert.That(customers.Count, Is.EqualTo(2));
            Assert.That(customers[0].Name, Is.EqualTo("Elena Gilbert"));
            Assert.That(customers[1].Name, Is.EqualTo("Stefan Salvatore"));
        }
    }
}
