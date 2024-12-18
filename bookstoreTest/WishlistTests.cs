using System;
using System.Collections.Generic;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class WishlistTests
    {
        private Wishlist wishlist;
        private Book book1;
        private Book book2;
        private Customer customer;

        [SetUp]
        public void Setup()
        {
            Book.ClearBooks();
            Customer.ClearUsers();

            wishlist = new Wishlist();
            book1 = new Book("The Founder's Diaries", 199.99, "English");
            book2 = new Book("Mystic Legends", 159.99, "English");
            customer = new Customer("Elena Gilbert", "123-456-7890", "elena@gilbert.com", new DateTime(1992, 6, 22), "Mystic Falls, VA");
        }

        [Test]
        public void AddBook_ShouldAddBookToWishlistAndSetReverseConnection()
        {
            wishlist.AddBook(book1);

            Assert.That(wishlist.GetAssociatedBooks().Count, Is.EqualTo(1));
            Assert.That(wishlist.GetAssociatedBooks()[0], Is.EqualTo(book1));
            Assert.That(book1.getAssociatedWishlists().Contains(wishlist), Is.True);
        }

        [Test]
        public void AddBook_ShouldThrowException_WhenBookIsNull()
        {
            Assert.Throws<ArgumentException>(() => wishlist.AddBook(null));
        }

        [Test]
        public void AddBook_ShouldThrowException_WhenWishlistIsFull()
        {
            wishlist.MaxCapacity = 1;
            wishlist.AddBook(book1);

            Assert.Throws<InvalidOperationException>(() => wishlist.AddBook(book2));
        }

        [Test]
        public void RemoveBook_ShouldRemoveBookFromWishlistAndClearReverseConnection()
        {
            wishlist.AddBook(book1);
            wishlist.RemoveBook(book1);

            Assert.That(wishlist.GetAssociatedBooks().Count, Is.EqualTo(0));
            Assert.That(book1.getAssociatedWishlists().Contains(wishlist), Is.False);
        }

        [Test]
        public void RemoveBook_ShouldThrowException_WhenBookIsNull()
        {
            Assert.Throws<ArgumentException>(() => wishlist.RemoveBook(null));
        }

        [Test]
        public void RemoveAllBooks_ShouldClearWishlistAndAllReverseConnections()
        {
            wishlist.AddBook(book1);
            wishlist.AddBook(book2);
            wishlist.RemoveAllBooks();

            Assert.That(wishlist.GetAssociatedBooks().Count, Is.EqualTo(0));
            Assert.That(book1.getAssociatedWishlists().Contains(wishlist), Is.False);
            Assert.That(book2.getAssociatedWishlists().Contains(wishlist), Is.False);
        }

        [Test]
        public void UpdateBook_ShouldReplaceOldBookWithNewBook()
        {
            wishlist.AddBook(book1);
            wishlist.UpdateBook(book1, book2);

            Assert.That(wishlist.GetAssociatedBooks().Count, Is.EqualTo(1));
            Assert.That(wishlist.GetAssociatedBooks()[0], Is.EqualTo(book2));
            Assert.That(book1.getAssociatedWishlists().Contains(wishlist), Is.False);
            Assert.That(book2.getAssociatedWishlists().Contains(wishlist), Is.True);
        }

        [Test]
        public void UpdateBook_ShouldThrowException_WhenOldBookDoesNotExist()
        {
            Assert.Throws<InvalidOperationException>(() => wishlist.UpdateBook(book1, book2));
        }

        [Test]
        public void AssignCustomer_ShouldAssignCustomerAndSetReverseConnection()
        {
            wishlist.AssignCustomer(customer);

            Assert.That(wishlist.AssociatedCustomer, Is.EqualTo(customer));
            Assert.That(customer.AssociatedWishlist, Is.EqualTo(wishlist));
        }

        [Test]
        public void AssignCustomer_ShouldThrowException_WhenCustomerIsNull()
        {
            Assert.Throws<ArgumentException>(() => wishlist.AssignCustomer(null));
        }

        [Test]
        public void RemoveFromCustomer_ShouldClearCustomerConnectionAndReverseConnection()
        {
            wishlist.AssignCustomer(customer);
            wishlist.RemoveFromCustomer();

            Assert.That(wishlist.AssociatedCustomer, Is.Null);
            Assert.That(customer.AssociatedWishlist, Is.Null);
        }

        [Test]
        public void UpdateCustomer_ShouldReplaceOldCustomerWithNewCustomer()
        {
            Customer newCustomer = new Customer("Stefan Salvatore", "987-654-3210", "stefan@salvatore.com", new DateTime(1847, 11, 1), "Salvatore Mansion");

            wishlist.AssignCustomer(customer);
            wishlist.UpdateCustomer(newCustomer);

            Assert.That(wishlist.AssociatedCustomer, Is.EqualTo(newCustomer));
            Assert.That(newCustomer.AssociatedWishlist, Is.EqualTo(wishlist));
            Assert.That(customer.AssociatedWishlist, Is.Null);
        }

        [Test]
        public void UpdateCustomer_ShouldThrowException_WhenNewCustomerIsNull()
        {
            wishlist.AssignCustomer(customer);
            Assert.Throws<ArgumentException>(() => wishlist.UpdateCustomer(null));
        }
    }
}
