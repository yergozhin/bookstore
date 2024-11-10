using System;
using System.Collections.Generic;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class RomanceCheck
    {
        private readonly Romance romance1 = new Romance("Love in Mystic Falls", 159.99f, "English", "Forbidden Love");
        private readonly Romance romance2 = new Romance("Eternal Flame", 199.99f, "French", "Unrequited Love");

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
            romance1.Title = "Changed Title";
            List<Romance> romances = Book.GetBooks().ConvertAll(book => book as Romance).FindAll(b => b != null);
            Assert.That(romances[0].Title, Is.EqualTo("Love in Mystic Falls"));
        }

        [Test]
        public void CheckExtentPersistency()
        {
            BookstoreFileManager.SaveBookstore();
            Book.GetBooks().Clear();
            Assert.That(Book.GetBooks().Count, Is.EqualTo(0));
            BookstoreFileManager.LoadBookstore();
            List<Romance> romances = Book.GetBooks().ConvertAll(book => book as Romance).FindAll(b => b != null);
            Assert.That(romances.Count, Is.EqualTo(2));
            Assert.That(romances[0].Title, Is.EqualTo("Love in Mystic Falls"));
            Assert.That(romances[1].Title, Is.EqualTo("Eternal Flame"));
        }
    }
}
