using System;
using System.Collections.Generic;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class HorrorCheck
    {
        private readonly Horror horror1 = new Horror("Mystic Legends", 199.99f, "English", "Supernatural", 7);
        private readonly Horror horror2 = new Horror("The Vampire's Secret", 299.99f, "French", "Paranormal", 5);

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
            horror1.Title = "Changed Title";
            List<Horror> horrors = Book.GetBooks().ConvertAll(book => book as Horror).FindAll(b => b != null);
            Assert.That(horrors[0].Title, Is.EqualTo("Mystic Legends"));
        }

        [Test]
        public void CheckExtentPersistency()
        {
            BookstoreFileManager.SaveBookstore();
            Book.GetBooks().Clear();
            Assert.That(Book.GetBooks().Count, Is.EqualTo(0));
            BookstoreFileManager.LoadBookstore();
            List<Horror> horrors = Book.GetBooks().ConvertAll(book => book as Horror).FindAll(b => b != null);
            Assert.That(horrors.Count, Is.EqualTo(2));
            Assert.That(horrors[0].Title, Is.EqualTo("Mystic Legends"));
            Assert.That(horrors[1].Title, Is.EqualTo("The Vampire's Secret"));
        }
    }
}
