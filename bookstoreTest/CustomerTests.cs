using System;
using System.Collections.Generic;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class CustomerTests
    {
        private Customer customer1;
        private Customer customer2;
        private Order order1;
        private Review review1;
        private Wishlist wishlist1;
        private Discount discount1;

        [SetUp]
        public void Setup()
        {
            Customer.ClearUsers();
            Order.ClearOrders();
            Review.ClearReviews();
            Discount.ClearDiscounts();

            customer1 = new Customer("Elena Gilbert", "123-456-7890", "elena@gilbert.com", new DateTime(1992, 6, 22),
                "Mystic Falls, VA");
            customer2 = new Customer("Stefan Salvatore", "987-654-3210", "stefan@salvatore.com",
                new DateTime(1847, 11, 1), "Salvatore Mansion, Mystic Falls");

            order1 = new Order(DateTime.Today, "Pending");
            review1 = new Review(5, "Great book!", DateTime.Today);
            wishlist1 = new Wishlist();
            discount1 = new Discount("Single Use", 10, DateTime.Today.AddDays(30));
        }

        [Test]
        public void CheckCustomerAttributes()
        {
            Assert.That(customer1.Name, Is.EqualTo("Elena Gilbert"));
            Assert.That(customer1.Address, Is.EqualTo("Mystic Falls, VA"));
        }

        [Test]
        public void AddOrder_ShouldSetReverseConnection()
        {
            customer1.addOrder(order1);
            Assert.That(customer1.GetAssociatedOrders().Count, Is.EqualTo(1));
            Assert.That(order1.AssociatedCustomer, Is.EqualTo(customer1));
        }

        [Test]
        public void RemoveOrder_ShouldClearReverseConnection()
        {
            customer1.addOrder(order1);
            customer1.removeOrder(order1);

            Assert.That(customer1.GetAssociatedOrders().Count, Is.EqualTo(0));
            Assert.That(order1.AssociatedCustomer, Is.Null);
        }

        [Test]
        public void AddReview_ShouldSetReverseConnection()
        {
            customer1.addReview(review1);
            Assert.That(customer1.getAssociatedReviews().Count, Is.EqualTo(1));
            Assert.That(review1.AssociatedCustomer, Is.EqualTo(customer1));
        }

        [Test]
        public void RemoveReview_ShouldClearReverseConnection()
        {
            customer1.addReview(review1);
            customer1.removeReview(review1);

            Assert.That(customer1.getAssociatedReviews().Count, Is.EqualTo(0));
            Assert.That(review1.AssociatedCustomer, Is.Null);
        }

        [Test]
        public void AssignWishlist_ShouldSetReverseConnection()
        {
            customer1.assignWishlist(wishlist1);
            Assert.That(customer1.AssociatedWishlist, Is.EqualTo(wishlist1));
            Assert.That(wishlist1.AssociatedCustomer, Is.EqualTo(customer1));
        }

        [Test]
        public void RemoveWishlist_ShouldClearReverseConnection()
        {
            customer1.assignWishlist(wishlist1);
            customer1.removeFromWishlist(wishlist1);

            Assert.That(customer1.AssociatedWishlist, Is.Null);
            Assert.That(wishlist1.AssociatedCustomer, Is.Null);
        }

        [Test]
        public void AddDiscount_ShouldSetReverseConnection()
        {
            customer1.addDiscount(discount1);
            Assert.That(customer1.getAssociatedDiscounts().Count, Is.EqualTo(1));
            Assert.That(discount1.getAssociatedCustomers().Contains(customer1), Is.True);
        }

        [Test]
        public void RemoveDiscount_ShouldClearReverseConnection()
        {
            customer1.addDiscount(discount1);
            customer1.removeDiscount(discount1);

            Assert.That(customer1.getAssociatedDiscounts().Count, Is.EqualTo(0));
            Assert.That(discount1.getAssociatedCustomers().Contains(customer1), Is.False);
        }
    }
}