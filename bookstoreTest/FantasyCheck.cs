using System;
using System.Collections.Generic;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class FantasyCheck
    {
        private readonly Fantasy fantasy1 = new Fantasy("Magic in Mystic Falls", 399.99f, "English", new List<string> { "Vampires", "Witches" });
        private readonly Fantasy fantasy2 = new Fantasy("Secrets of the Supernatural", 299.99f, "Spanish", new List<string> { "Werewolves" });

        [Test]
        public void CheckFantasyExtent()
        {
            List<Fantasy> fantasies = Book.GetBooks().ConvertAll(book => book as Fantasy).FindAll(b => b != null);
            Assert.That(fantasies.Count, Is.EqualTo(2));
            Assert.That(fantasies[0].Title, Is.EqualTo("Magic in Mystic Falls"));
            Assert.That(fantasies[1].Title, Is.EqualTo("Secrets of the Supernatural"));
        }

        [Test]
        public void CheckEncapsulationInExtent()
        {
            fantasy1.Title = "Changed Title";
            List<Fantasy> fantasies = Book.GetBooks().ConvertAll(book => book as Fantasy).FindAll(b => b != null);
            Assert.That(fantasies[0].Title, Is.EqualTo("Magic in Mystic Falls"));
        }

        [Test]
        public void CheckExtentPersistency()
        {
            BookstoreFileManager.SaveBookstore();
            Book.GetBooks().Clear();
            Assert.That(Book.GetBooks().Count, Is.EqualTo(0));
            BookstoreFileManager.LoadBookstore();
            List<Fantasy> fantasies = Book.GetBooks().ConvertAll(book => book as Fantasy).FindAll(b => b != null);
            Assert.That(fantasies.Count, Is.EqualTo(2));
            Assert.That(fantasies[0].Title, Is.EqualTo("Magic in Mystic Falls"));
            Assert.That(fantasies[1].Title, Is.EqualTo("Secrets of the Supernatural"));
        }
    }
}
