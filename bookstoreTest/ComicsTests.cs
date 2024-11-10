using System;
using System.Collections.Generic;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class ComicsTests
    {
        private readonly Comics comics1 = new Comics("Mystic Chronicles", 49.99f, "English", 1);
        private readonly Comics comics2 = new Comics("Supernatural Adventures", 59.99f, "Spanish", 5);

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
            List<Comics> comicsList = Comics.GetBooks().ConvertAll(book => (Comics)book);
            Assert.That(comicsList.Count, Is.EqualTo(2));
            Assert.That(comicsList[0].Title, Is.EqualTo("Mystic Chronicles"));
            Assert.That(comicsList[1].Title, Is.EqualTo("Supernatural Adventures"));
        }

        [Test]
        public void CheckEncapsulationInExtent()
        {
            comics1.Title = "Changed Title";
            List<Comics> comicsList = Comics.GetBooks().ConvertAll(book => (Comics)book);
            Assert.That(comicsList[0].Title, Is.EqualTo("Mystic Chronicles"));
        }

        [Test]
        public void CheckExtentPersistency()
        {
            BookstoreFileManager.SaveBookstore();
            Comics.GetBooks().Clear();
            Assert.That(Comics.GetBooks().Count, Is.EqualTo(0));
            BookstoreFileManager.LoadBookstore();
            List<Comics> comicsList = Comics.GetBooks().ConvertAll(book => (Comics)book);
            Assert.That(comicsList.Count, Is.EqualTo(2));
            Assert.That(comicsList[0].Title, Is.EqualTo("Mystic Chronicles"));
            Assert.That(comicsList[1].Title, Is.EqualTo("Supernatural Adventures"));
        }
    }
}
