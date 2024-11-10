using System;
using System.Collections.Generic;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class DiscountTests
    {
        private readonly Discount discount1 = new Discount("Mystic Falls Loyalty Card", 10.0f, new DateTime(2025, 1, 1));
        private readonly Discount discount2 = new Discount("Salvatore Boarding House Special", 25.0f, new DateTime(2024, 12, 31));

        [Test]
        public void CheckDiscountAttributes()
        {
            Assert.That(discount1.Type, Is.EqualTo("Mystic Falls Loyalty Card"));
            Assert.That(discount1.AmountInPercentage, Is.EqualTo(10.0f));
            Assert.That(discount1.DeadlineDate, Is.EqualTo(new DateTime(2025, 1, 1)));
        }

        [Test]
        public void CheckEmptyTypeException()
        {
            Assert.Throws<ArgumentException>(() => new Discount("", 15.0f, new DateTime(2024, 11, 30)));
        }

        [Test]
        public void CheckAmountOutOfRangeException()
        {
            Assert.Throws<ArgumentException>(() => new Discount("Mystic Falls Newcomer Card", -5.0f, new DateTime(2025, 6, 1)));
            Assert.Throws<ArgumentException>(() => new Discount("Mystic Falls Newcomer Card", 105.0f, new DateTime(2025, 6, 1)));
        }

        [Test]
        public void CheckDiscountWithValidData()
        {
            Assert.That(discount2.Type, Is.EqualTo("Salvatore Boarding House Special"));
            Assert.That(discount2.AmountInPercentage, Is.EqualTo(25.0f));
            Assert.That(discount2.DeadlineDate, Is.EqualTo(new DateTime(2024, 12, 31)));
        }

        [Test]
        public void CheckDiscountExtent()
        {
            List<Discount> discounts = Discount.GetDiscounts();
            Assert.That(discounts.Count, Is.EqualTo(2));
            Assert.That(discounts[0].Type, Is.EqualTo("Mystic Falls Loyalty Card"));
            Assert.That(discounts[1].Type, Is.EqualTo("Salvatore Boarding House Special"));
        }

        [Test]
        public void CheckEncapsulationInExtent()
        {
            discount1.Type = "Changed Discount Type";
            List<Discount> discounts = Discount.GetDiscounts();
            Assert.That(discounts[0].Type, Is.EqualTo("Mystic Falls Loyalty Card"));
        }

        [Test]
        public void CheckExtentPersistency()
        {
            BookstoreFileManager.SaveBookstore();
            Discount.GetDiscounts().Clear();
            Assert.That(Discount.GetDiscounts().Count, Is.EqualTo(0));
            BookstoreFileManager.LoadBookstore();
            List<Discount> discounts = Discount.GetDiscounts();
            Assert.That(discounts.Count, Is.EqualTo(2));
            Assert.That(discounts[0].Type, Is.EqualTo("Mystic Falls Loyalty Card"));
            Assert.That(discounts[1].Type, Is.EqualTo("Salvatore Boarding House Special"));
        }
    }
}
