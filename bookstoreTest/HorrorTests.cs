using System;
using System.Collections.Generic;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class HorrorTests
    {
        private Horror horror1;
        private Horror horror2;

        [SetUp]
        public void Setup()
        {
            Book.ClearBooks();

            horror1 = new Horror("Mystic Legends", 199.99f, "English", 7);
            horror2 = new Horror("The Vampire's Secret", 299.99f, "French", 5);
        }

        [Test]
        public void CheckHorrorAttributes()
        {
            Assert.That(horror1.Title, Is.EqualTo("Mystic Legends"));
            Assert.That(horror1.Price, Is.EqualTo(199.99f));
            Assert.That(horror1.LanguageOfPublication, Is.EqualTo("English"));
            Assert.That(horror1.LevelOfScariness, Is.EqualTo(7));
        }

        [Test]
        public void CheckLevelOutOfRangeException()
        {
            Assert.Throws<ArgumentException>(() => new Horror("Dark Shadows", 199.99f, "Spanish", 0));
            Assert.Throws<ArgumentException>(() => new Horror("Dark Shadows", 199.99f, "Spanish", 11));
        }

        [Test]
        public void CheckAnotherHorrorBook()
        {
            Assert.That(horror2.Title, Is.EqualTo("The Vampire's Secret"));
            Assert.That(horror2.Price, Is.EqualTo(299.99f));
            Assert.That(horror2.LanguageOfPublication, Is.EqualTo("French"));
            Assert.That(horror2.LevelOfScariness, Is.EqualTo(5));
        }

        [Test]
        public void CheckHorrorExtent()
        {
            List<Horror> horrors = Book.GetBooks().ConvertAll(book => book as Horror).FindAll(b => b != null);
            Assert.That(horrors.Count, Is.EqualTo(2));
            Assert.That(horrors[0].Title, Is.EqualTo("Mystic Legends"));
            Assert.That(horrors[1].Title, Is.EqualTo("The Vampire's Secret"));
        }

        [Test]
        public void CheckEncapsulationInExtent()
        {
            horror1.LevelOfScariness = 8;
            List<Horror> horrors = Book.GetBooks().ConvertAll(book => book as Horror).FindAll(b => b != null);
            Assert.That(horrors[0].LevelOfScariness, Is.EqualTo(8));
        }

        [Test]
        public void CheckExtentPersistency()
        {
            BookstoreFileManager.SaveBookstore();
            Book.ClearBooks();
            Assert.That(Book.GetBooks().Count, Is.EqualTo(0));
            BookstoreFileManager.LoadBookstore();
            List<Horror> horrors = Book.GetBooks().ConvertAll(book => book as Horror).FindAll(b => b != null);
            Assert.That(horrors.Count, Is.EqualTo(2));
            Assert.That(horrors[0].Title, Is.EqualTo("Mystic Legends"));
            Assert.That(horrors[1].Title, Is.EqualTo("The Vampire's Secret"));
        }
    }
}