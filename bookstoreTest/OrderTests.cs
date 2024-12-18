using System;
using NUnit.Framework;
using Bookstore.@class;
using System.Collections.Generic;

namespace Bookstore.@class.Tests
{
    public class OrderTests
    {
        private Order order1;
        private Order order2;
        private Book book1;
        private Book book2;
        private Employee employee1;
        private Customer customer1;

        [SetUp]
        public void Setup()
        {
            Order.ClearOrders();
            Book.ClearBooks();
            Employee.ClearUsers();
            Customer.ClearUsers();

            order1 = new Order(new DateTime(2024, 10, 31), "Processed");
            order2 = new Order(new DateTime(2024, 11, 5), "Shipped");

            book1 = new Book("Vampire Chronicles", 199.99, "English");
            book2 = new Book("Mystic Legends", 159.99, "English");

            employee1 = new Employee("Damon Salvatore", "555-555-5555", "damon@salvatore.com",
                new DateTime(1840, 10, 1), "Manager");
            customer1 = new Customer("Elena Gilbert", "123-456-7890", "elena@gilbert.com", new DateTime(1992, 6, 22),
                "Mystic Falls, VA");
        }

        [Test]
        public void CheckOrderAttributes()
        {
            Assert.That(order1.OrderDate, Is.EqualTo(new DateTime(2024, 10, 31)));
            Assert.That(order1.Status, Is.EqualTo("Processed"));
        }

        [Test]
        public void AddBook_ShouldSetReverseConnection()
        {
            order1.addBook(book1);

            Assert.That(order1.getAssociatedBooks().Count, Is.EqualTo(1));
            Assert.That(order1.getAssociatedBooks()[0], Is.EqualTo(book1));
            Assert.That(book1.getAssociatedOrders().Contains(order1), Is.True);
        }

        [Test]
        public void RemoveBook_ShouldClearReverseConnection()
        {
            order1.addBook(book1);
            order1.removeBook(book1);

            Assert.That(order1.getAssociatedBooks().Count, Is.EqualTo(0));
            Assert.That(book1.getAssociatedOrders().Contains(order1), Is.False);
        }

        [Test]
        public void RemoveAllBooks_ShouldClearAllAssociations()
        {
            order1.addBook(book1);
            order1.addBook(book2);

            Assert.That(order1.getAssociatedBooks().Count, Is.EqualTo(0));
            Assert.That(book1.getAssociatedOrders().Contains(order1), Is.False);
            Assert.That(book2.getAssociatedOrders().Contains(order1), Is.False);
        }

        [Test]
        public void AssignEmployee_ShouldSetReverseConnection()
        {
            order1.assignEmployeeWhoProcesses(employee1);

            Assert.That(order1.getAssociatedEmployees().Count, Is.EqualTo(1));
            Assert.That(order1.getAssociatedEmployees()[0], Is.EqualTo(employee1));
            Assert.That(employee1.getAssociatedOrders().Contains(order1), Is.True);
        }

        [Test]
        public void RemoveEmployee_ShouldClearReverseConnection()
        {
            order1.assignEmployeeWhoProcesses(employee1);
            order1.removeEmployeeFromProcessing(employee1);

            Assert.That(order1.getAssociatedEmployees().Count, Is.EqualTo(0));
            Assert.That(employee1.getAssociatedOrders().Contains(order1), Is.False);
        }

        [Test]
        public void AssignCustomer_ShouldSetReverseConnection()
        {
            order1.assignCustomer(customer1);

            Assert.That(order1.AssociatedCustomer, Is.EqualTo(customer1));
            Assert.That(customer1.GetAssociatedOrders().Contains(order1), Is.True);
        }

        [Test]
        public void RemoveCustomer_ShouldClearReverseConnection()
        {
            order1.assignCustomer(customer1);
            order1.removeFromCustomer();

            Assert.That(order1.AssociatedCustomer, Is.Null);
            Assert.That(customer1.GetAssociatedOrders().Contains(order1), Is.False);
        }

        [Test]
        public void CheckEmptyStatusException()
        {
            Assert.Throws<ArgumentException>(() => new Order(new DateTime(2024, 11, 1), ""));
        }

        [Test]
        public void CheckOrderExtent()
        {
            List<Order> orders = Order.GetOrders();
            Assert.That(orders.Count, Is.EqualTo(2));
            Assert.That(orders[0].Status, Is.EqualTo("Processed"));
            Assert.That(orders[1].Status, Is.EqualTo("Shipped"));
        }

        [Test]
        public void CheckExtentPersistency()
        {
            BookstoreFileManager.SaveBookstore();
            Order.ClearOrders();
            Assert.That(Order.GetOrders().Count, Is.EqualTo(0));
            BookstoreFileManager.LoadBookstore();
            List<Order> orders = Order.GetOrders();
            Assert.That(orders.Count, Is.EqualTo(2));
            Assert.That(orders[0].Status, Is.EqualTo("Processed"));
            Assert.That(orders[1].Status, Is.EqualTo("Shipped"));
        }
    }
}