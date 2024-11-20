using System;
using System.Collections.Generic;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class ComicsTests
    {
        private Comics comics1;
        private Comics comics2;

        [SetUp]
        public void Setup()
        {
            Book.GetBooks().Clear();
            comics1 = new Comics("Mystic Chronicles", 49.99f, "English", 1);
            comics2 = new Comics("Supernatural Adventures", 59.99f, "Spanish", 5);
        }

        [Test]
        public void CheckComicsAttributes()
        {
            Assert.That(comics1.Title, Is.EqualTo("Mystic Chronicles"));
            Assert.That(comics1.Price, Is.EqualTo(49.99f));
            Assert.That(comics1.LanguageOfPublication, Is.EqualTo("English"));
            Assert.That(comics1.IssueNumber, Is.EqualTo(1));
        }

        [Test]
        public void CheckInvalidIssueNumberException()
        {
            Assert.Throws<ArgumentException>(() => new Comics("Dark Shadows", 39.99f, "French", 0));
        }

        [Test]
        public void CheckAnotherComicsBook()
        {
            Assert.That(comics2.Title, Is.EqualTo("Supernatural Adventures"));
            Assert.That(comics2.Price, Is.EqualTo(59.99f));
            Assert.That(comics2.LanguageOfPublication, Is.EqualTo("Spanish"));
            Assert.That(comics2.IssueNumber, Is.EqualTo(5));
        }

        [Test]
        public void CheckComicsExtent()
        {
            List<Comics> comicsList = Book.GetBooks().ConvertAll(book => book as Comics).FindAll(b => b != null);
            Assert.That(comicsList.Count, Is.EqualTo(2));
            Assert.That(comicsList[0].Title, Is.EqualTo("Mystic Chronicles"));
            Assert.That(comicsList[1].Title, Is.EqualTo("Supernatural Adventures"));
        }

        [Test]
        public void CheckEncapsulationInExtent()
        {
            comics1.Title = "Changed Title";
            List<Comics> comicsList = Book.GetBooks().ConvertAll(book => book as Comics).FindAll(b => b != null);
            Assert.That(comicsList[0].Title, Is.EqualTo("Changed Title"));
        }

        [Test]
        public void CheckExtentPersistency()
        {
            BookstoreFileManager.SaveBookstore();
            Book.GetBooks().Clear();
            Assert.That(Book.GetBooks().Count, Is.EqualTo(0));
            BookstoreFileManager.LoadBookstore();
            List<Comics> comicsList = Book.GetBooks().ConvertAll(book => book as Comics).FindAll(b => b != null);
            Assert.That(comicsList.Count, Is.EqualTo(2));
            Assert.That(comicsList[0].Title, Is.EqualTo("Mystic Chronicles"));
            Assert.That(comicsList[1].Title, Is.EqualTo("Supernatural Adventures"));
        }
    }
}
