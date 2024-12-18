using System;
using System.Collections.Generic;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class PublisherTests
    {
        private Publisher publisher1;
        private Publisher publisher2;
        private Book book1;
        private Book book2;

        [SetUp]
        public void Setup()
        {
            Publisher.ClearPublishers();
            Book.ClearBooks();

            publisher1 = new Publisher("Mystic Falls Publishing", "123 Vampire Lane", "contact@mysticfalls.com", "123-456-7890");
            publisher2 = new Publisher("Salvatore Books", "Salvatore Mansion", "info@salvatorebooks.com");

            book1 = new Book("The Founder's Diaries", 199.99, "English");
            book2 = new Book("Dark Shadows", 299.99, "English");
        }

        [Test]
        public void CheckPublisher()
        {
            Assert.That(publisher1.Name, Is.EqualTo("Mystic Falls Publishing"));
            Assert.That(publisher1.Address, Is.EqualTo("123 Vampire Lane"));
            Assert.That(publisher1.Email, Is.EqualTo("contact@mysticfalls.com"));
            Assert.That(publisher1.PhoneNumber, Is.EqualTo("123-456-7890"));
        }

        [Test]
        public void AddBook_ShouldAddBookAndSetReverseConnection()
        {
            publisher1.AddBook(book1);

            Assert.That(publisher1.PublishedBooks.Count, Is.EqualTo(1));
            Assert.That(publisher1.PublishedBooks[0], Is.EqualTo(book1));
            Assert.That(book1.Publisher, Is.EqualTo(publisher1));
        }

        [Test]
        public void RemoveBook_ShouldRemoveBookAndClearReverseConnection()
        {
            publisher1.AddBook(book1);
            publisher1.RemoveBook(book1);

            Assert.That(publisher1.PublishedBooks.Count, Is.EqualTo(0));
            Assert.That(book1.Publisher, Is.Null);
        }

        [Test]
        public void UpdateBook_ShouldReplaceOldBookWithNewBook()
        {
            publisher1.AddBook(book1);
            publisher1.UpdateBook(book1, book2);

            Assert.That(publisher1.PublishedBooks.Count, Is.EqualTo(1));
            Assert.That(publisher1.PublishedBooks[0], Is.EqualTo(book2));
            Assert.That(book1.Publisher, Is.Null);
            Assert.That(book2.Publisher, Is.EqualTo(publisher1));
        }

        [Test]
        public void CheckEmptyName()
        {
            Assert.Throws<ArgumentException>(() => new Publisher("", "Unknown Address", "info@publisher.com", "555-555-5555"));
        }

        [Test]
        public void CheckInvalidEmail()
        {
            Assert.Throws<ArgumentException>(() => new Publisher("Mystic Falls Publishing", "123 Vampire Lane", "contactmysticfalls.com"));
        }

        [Test]
        public void CheckPublisherExtent()
        {
            List<Publisher> publishers = Publisher.GetPublishers();
            Assert.That(publishers.Count, Is.EqualTo(2));
            Assert.That(publishers[0].Name, Is.EqualTo("Mystic Falls Publishing"));
            Assert.That(publishers[1].Name, Is.EqualTo("Salvatore Books"));
        }

        [Test]
        public void CheckEncapsulationInExtent()
        {
            publisher1.Name = "Updated Name";
            List<Publisher> publishers = Publisher.GetPublishers();
            Assert.That(publishers[0].Name, Is.EqualTo("Updated Name"));
        }

        [Test]
        public void CheckPublisherNoPhone()
        {
            Assert.That(publisher2.Name, Is.EqualTo("Salvatore Books"));
            Assert.That(publisher2.Address, Is.EqualTo("Salvatore Mansion"));
            Assert.That(publisher2.Email, Is.EqualTo("info@salvatorebooks.com"));
            Assert.That(publisher2.PhoneNumber, Is.Null);
        }

        [Test]
        public void CheckExtentPersistency()
        {
            BookstoreFileManager.SaveBookstore();
            Publisher.ClearPublishers();
            Assert.That(Publisher.GetPublishers().Count, Is.EqualTo(0));
            BookstoreFileManager.LoadBookstore();
            List<Publisher> publishers = Publisher.GetPublishers();
            Assert.That(publishers.Count, Is.EqualTo(2));
            Assert.That(publishers[0].Name, Is.EqualTo("Mystic Falls Publishing"));
            Assert.That(publishers[1].Name, Is.EqualTo("Salvatore Books"));
        }
    }
}
