using System;
using System.Collections.Generic;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class OrderTests
    {
        private readonly Order order1 = new Order(new DateTime(2024, 10, 31), "Processed");
        private readonly Order order2 = new Order(new DateTime(2024, 11, 5), "Shipped");

        [Test]
        public void CheckOrderAttributes()
        {
            Assert.That(order1.OrderDate, Is.EqualTo(new DateTime(2024, 10, 31)));
            Assert.That(order1.Status, Is.EqualTo("Processed"));
        }

        [Test]
        public void CheckEmptyStatusException()
        {
            Assert.Throws<ArgumentException>(() => new Order(new DateTime(2024, 11, 1), ""));
        }

        [Test]
        public void CheckOrderWithValidData()
        {
            Assert.That(order2.OrderDate, Is.EqualTo(new DateTime(2024, 11, 5)));
            Assert.That(order2.Status, Is.EqualTo("Shipped"));
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
        public void CheckEncapsulationInExtent()
        {
            order1.Status = "Updated Status";
            List<Order> orders = Order.GetOrders();
            Assert.That(orders[0].Status, Is.EqualTo("Processed"));
        }

        [Test]
        public void CheckExtentPersistency()
        {
            BookstoreFileManager.SaveBookstore();
            Order.GetOrders().Clear();
            Assert.That(Order.GetOrders().Count, Is.EqualTo(0));
            BookstoreFileManager.LoadBookstore();
            List<Order> orders = Order.GetOrders();
            Assert.That(orders.Count, Is.EqualTo(2));
            Assert.That(orders[0].Status, Is.EqualTo("Processed"));
            Assert.That(orders[1].Status, Is.EqualTo("Shipped"));
        }
    }
}
