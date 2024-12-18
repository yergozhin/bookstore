using System;
using System.Collections.Generic;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class ReviewTests
    {
        private Review review1;
        private Review review2;
        private Book book1;
        private Customer customer1;

        [SetUp]
        public void Setup()
        {
            Review.ClearReviews();
            Book.ClearBooks();
            Customer.ClearUsers();

            review1 = new Review(5, "Amazing read!", new DateTime(2024, 10, 31));
            review2 = new Review(3, "It was ok.", new DateTime(2024, 11, 1));

            book1 = new Book("Vampire Diaries", 299.99, "English");
            customer1 = new Customer("Elena Gilbert", "123-456-7890", "elena@gilbert.com", new DateTime(1992, 6, 22), "Mystic Falls, VA");
        }

        [Test]
        public void CheckReview()
        {
            Assert.That(review1.Rating, Is.EqualTo(5));
            Assert.That(review1.Comment, Is.EqualTo("Amazing read!"));
            Assert.That(review1.ReviewDate, Is.EqualTo(new DateTime(2024, 10, 31)));
        }

        [Test]
        public void CheckRatingOutOfRange()
        {
            Assert.Throws<ArgumentException>(() => new Review(0, "Invalid rating", new DateTime(2024, 11, 2)));
            Assert.Throws<ArgumentException>(() => new Review(6, "Invalid rating", new DateTime(2024, 11, 3)));
        }

        [Test]
        public void AssignBook_ShouldSetReverseConnection()
        {
            review1.assignBook(book1);

            Assert.That(review1.AssociatedBook, Is.EqualTo(book1));
            Assert.That(book1.getAssociatedReviews().Contains(review1), Is.True);
        }

        [Test]
        public void RemoveBook_ShouldClearReverseConnection()
        {
            review1.assignBook(book1);
            review1.removeFromBook(book1);

            Assert.That(review1.AssociatedBook, Is.Null);
            Assert.That(book1.getAssociatedReviews().Contains(review1), Is.False);
        }

        [Test]
        public void AssignCustomer_ShouldSetReverseConnection()
        {
            review1.assignCustomer(customer1);

            Assert.That(review1.AssociatedCustomer, Is.EqualTo(customer1));
            Assert.That(customer1.getAssociatedReviews().Contains(review1), Is.True);
        }

        [Test]
        public void RemoveCustomer_ShouldClearReverseConnection()
        {
            review1.assignCustomer(customer1);
            review1.removeFromCustomer();

            Assert.That(review1.AssociatedCustomer, Is.Null);
            Assert.That(customer1.getAssociatedReviews().Contains(review1), Is.False);
        }

        [Test]
        public void CheckEmptyComment()
        {
            Assert.Throws<ArgumentException>(() => new Review(4, "", new DateTime(2024, 11, 2)));
        }

        [Test]
        public void CheckClassExtentStoresCorrectInstances()
        {
            List<Review> reviews = Review.GetReviews();
            Assert.That(reviews.Count, Is.EqualTo(2));
            Assert.That(reviews[0].Comment, Is.EqualTo("Amazing read!"));
            Assert.That(reviews[1].Comment, Is.EqualTo("It was ok."));
        }

        [Test]
        public void CheckExtentPersistency()
        {
            BookstoreFileManager.SaveBookstore();
            Review.ClearReviews();
            Assert.That(Review.GetReviews().Count, Is.EqualTo(0));
            BookstoreFileManager.LoadBookstore();
            List<Review> reviews = Review.GetReviews();
            Assert.That(reviews.Count, Is.EqualTo(2));
            Assert.That(reviews[0].Comment, Is.EqualTo("Amazing read!"));
            Assert.That(reviews[1].Comment, Is.EqualTo("It was ok."));
        }
    }
}
