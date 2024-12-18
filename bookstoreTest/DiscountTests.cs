using System;
using System.Collections.Generic;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class DiscountTests
    {
        private Discount discount1;
        private Discount discount2;
        private Customer customer1;
        private Customer customer2;

        [SetUp]
        public void Setup()
        {
            Discount.ClearDiscounts();
            Customer.ClearUsers();

            discount1 = new Discount("Single Use", 10.0f, new DateTime(2025, 1, 1));
            discount2 = new Discount("Multiple Use", 25.0f, new DateTime(2024, 12, 31));

            customer1 = new Customer("Elena Gilbert", "123-456-7890", "elena@gilbert.com", new DateTime(1992, 6, 22),
                "Mystic Falls, VA");
            customer2 = new Customer("Stefan Salvatore", "987-654-3210", "stefan@salvatore.com",
                new DateTime(1847, 11, 1), "Salvatore Mansion, Mystic Falls");
        }

        [Test]
        public void CheckDiscountAttributes()
        {
            Assert.That(discount1.Type, Is.EqualTo("Single Use"));
            Assert.That(discount1.AmountInPercentage, Is.EqualTo(10.0f));
            Assert.That(discount1.DeadlineDate, Is.EqualTo(new DateTime(2025, 1, 1)));
        }

        [Test]
        public void AddCustomer_ShouldSetReverseConnection()
        {
            discount1.addCustomer(customer1);

            Assert.That(discount1.getAssociatedCustomers().Count, Is.EqualTo(1));
            Assert.That(discount1.getAssociatedCustomers()[0], Is.EqualTo(customer1));
            Assert.That(customer1.getAssociatedDiscounts().Contains(discount1), Is.True);
        }

        [Test]
        public void RemoveCustomer_ShouldClearReverseConnection()
        {
            discount1.addCustomer(customer1);
            discount1.removeCustomer(customer1);

            Assert.That(discount1.getAssociatedCustomers().Count, Is.EqualTo(0));
            Assert.That(customer1.getAssociatedDiscounts().Contains(discount1), Is.False);
        }

        [Test]
        public void RemoveAllCustomers_ShouldClearAllConnections()
        {
            discount1.addCustomer(customer1);
            discount1.addCustomer(customer2);

            Assert.That(discount1.getAssociatedCustomers().Count, Is.EqualTo(0));
            Assert.That(customer1.getAssociatedDiscounts().Contains(discount1), Is.False);
            Assert.That(customer2.getAssociatedDiscounts().Contains(discount1), Is.False);
        }

        [Test]
        public void CheckEmptyTypeException()
        {
            Assert.Throws<ArgumentException>(() => new Discount("", 15.0f, new DateTime(2024, 11, 30)));
        }

        [Test]
        public void CheckAmountOutOfRangeException()
        {
            Assert.Throws<ArgumentException>(() => new Discount("Single Use", -5.0f, new DateTime(2025, 6, 1)));
            Assert.Throws<ArgumentException>(() => new Discount("Single Use", 105.0f, new DateTime(2025, 6, 1)));
        }

        [Test]
        public void CheckDiscountExtent()
        {
            List<Discount> discounts = Discount.GetDiscounts();
            Assert.That(discounts.Count, Is.EqualTo(2));
            Assert.That(discounts[0].Type, Is.EqualTo("Single Use"));
            Assert.That(discounts[1].Type, Is.EqualTo("Multiple Use"));
        }

        [Test]
        public void CheckEncapsulationInExtent()
        {
            discount1.Type = "Multiple Use";

            List<Discount> discounts = Discount.GetDiscounts();
            Assert.That(discounts[0].Type, Is.EqualTo("Multiple Use"));
        }

        [Test]
        public void CheckExtentPersistency()
        {
            BookstoreFileManager.SaveBookstore();
            Discount.ClearDiscounts();
            Assert.That(Discount.GetDiscounts().Count, Is.EqualTo(0));
            BookstoreFileManager.LoadBookstore();
            List<Discount> discounts = Discount.GetDiscounts();
            Assert.That(discounts.Count, Is.EqualTo(2));
            Assert.That(discounts[0].Type, Is.EqualTo("Single Use"));
            Assert.That(discounts[1].Type, Is.EqualTo("Multiple Use"));
        }
    }
}