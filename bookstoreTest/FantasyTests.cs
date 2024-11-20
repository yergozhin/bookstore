using System;
using System.Collections.Generic;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class FantasyTests
    {
        private Fantasy fantasy1;
        private Fantasy fantasy2;

        [SetUp]
        public void Setup()
        {
            Book.GetBooks().Clear();

            fantasy1 = new Fantasy("Magic in Mystic Falls", 399.99f, "English", new List<string> { "Vampires", "Witches" });
            fantasy2 = new Fantasy("Secrets of the Supernatural", 299.99f, "Spanish", new List<string> { "Werewolves" });
        }

        [Test]
        public void CheckFantasyAttributes()
        {
            Assert.That(fantasy1.Title, Is.EqualTo("Magic in Mystic Falls"));
            Assert.That(fantasy1.Price, Is.EqualTo(399.99f));
            Assert.That(fantasy1.LanguageOfPublication, Is.EqualTo("English"));
            Assert.That(fantasy1.MagicalCreatures, Is.EquivalentTo(new List<string> { "Vampires", "Witches" }));
        }

        [Test]
        public void CheckEmptyMagicalCreaturesException()
        {
            Assert.Throws<ArgumentException>(() => new Fantasy("No Creatures", 179.99f, "German", new List<string>()));
        }

        [Test]
        public void CheckInvalidMagicalCreature()
        {
            var fantasy = new Fantasy("Fantasy with Unknown Creatures", 259.99f, "English", new List<string> { "Aliens", "Unicorns" });
            Assert.That(fantasy.MagicalCreatures, Is.EquivalentTo(new List<string> { "Others", "Others" }));
        }

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
            Assert.That(fantasies[0].Title, Is.EqualTo("Changed Title"));
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
