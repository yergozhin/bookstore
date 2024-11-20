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

        [SetUp]
        public void Setup()
        {
            Review.GetReviews().Clear();

            review1 = new Review(5, "Amazing read!", new DateTime(2024, 10, 31));
            review2 = new Review(3, "It was ok.", new DateTime(2024, 11, 1));
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
        public void CheckEmptyComment()
        {
            Assert.Throws<ArgumentException>(() => new Review(4, "", new DateTime(2024, 11, 2)));
        }

        [Test]
        public void CheckReviewWithAnotherData()
        {
            Assert.That(review2.Rating, Is.EqualTo(3));
            Assert.That(review2.Comment, Is.EqualTo("It was ok."));
            Assert.That(review2.ReviewDate, Is.EqualTo(new DateTime(2024, 11, 1)));
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
        public void CheckEncapsulation()
        {
            review1.Rating = 4;
            List<Review> reviews = Review.GetReviews();
            Assert.That(reviews[0].Rating, Is.EqualTo(4));
        }

        [Test]
        public void CheckExtentPersistency()
        {
            BookstoreFileManager.SaveBookstore();
            Review.GetReviews().Clear();
            Assert.That(Review.GetReviews().Count, Is.EqualTo(0));
            BookstoreFileManager.LoadBookstore();
            List<Review> reviews = Review.GetReviews();
            Assert.That(reviews.Count, Is.EqualTo(2));
            Assert.That(reviews[0].Comment, Is.EqualTo("Amazing read!"));
            Assert.That(reviews[1].Comment, Is.EqualTo("It was ok."));
        }
    }
}
