using System;
using System.Collections.Generic;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class RomanceTests
    {
        private Romance romance1;
        private Romance romance2;

        [SetUp]
        public void Setup()
        {
            Book.ClearBooks();

            romance1 = new Romance("Love in Mystic Falls", 159.99f, "English", "Forbidden Love");
            romance2 = new Romance("Eternal Flame", 199.99f, "French", "Unrequited Love");
        }

        [Test]
        public void CheckRomanceAttributes()
        {
            Assert.That(romance1.Title, Is.EqualTo("Love in Mystic Falls"));
            Assert.That(romance1.Price, Is.EqualTo(159.99f));
            Assert.That(romance1.LanguageOfPublication, Is.EqualTo("English"));
            Assert.That(romance1.RelationshipsType, Is.EqualTo("Forbidden Love"));
        }

        [Test]
        public void CheckEmptyRelationshipsTypeException()
        {
            Assert.Throws<ArgumentException>(() => new Romance("A Vampire's Heart", 179.99f, "German", ""));
        }

        [Test]
        public void CheckAnotherRomanceBook()
        {
            Assert.That(romance2.Title, Is.EqualTo("Eternal Flame"));
            Assert.That(romance2.Price, Is.EqualTo(199.99f));
            Assert.That(romance2.LanguageOfPublication, Is.EqualTo("French"));
            Assert.That(romance2.RelationshipsType, Is.EqualTo("Unrequited Love"));
        }

        [Test]
        public void CheckRomanceExtent()
        {
            List<Romance> romances = Book.GetBooks().ConvertAll(book => book as Romance).FindAll(b => b != null);
            Assert.That(romances.Count, Is.EqualTo(2));
            Assert.That(romances[0].Title, Is.EqualTo("Love in Mystic Falls"));
            Assert.That(romances[1].Title, Is.EqualTo("Eternal Flame"));
        }

        [Test]
        public void CheckEncapsulationInExtent()
        {
            romance1.RelationshipsType = "Friends to Lovers";
            List<Romance> romances = Book.GetBooks().ConvertAll(book => book as Romance).FindAll(b => b != null);
            Assert.That(romances[0].RelationshipsType, Is.EqualTo("Friends to Lovers"));
        }

        [Test]
        public void CheckExtentPersistency()
        {
            BookstoreFileManager.SaveBookstore();
            Book.ClearBooks();
            Assert.That(Book.GetBooks().Count, Is.EqualTo(0));
            BookstoreFileManager.LoadBookstore();
            List<Romance> romances = Book.GetBooks().ConvertAll(book => book as Romance).FindAll(b => b != null);
            Assert.That(romances.Count, Is.EqualTo(2));
            Assert.That(romances[0].Title, Is.EqualTo("Love in Mystic Falls"));
            Assert.That(romances[1].Title, Is.EqualTo("Eternal Flame"));
        }
    }
}
