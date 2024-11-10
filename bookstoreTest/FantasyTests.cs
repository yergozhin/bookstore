using System;
using System.Collections.Generic;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class FantasyTests
    {
        private readonly Fantasy fantasy1 = new Fantasy("Magic in Mystic Falls", 399.99f, "English", new List<string> { "Vampires", "Witches" });
        private readonly Fantasy fantasy2 = new Fantasy("Secrets of the Supernatural", 299.99f, "Spanish", new List<string> { "Werewolves" });

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
            Assert.Throws<ArgumentException>(() => new Fantasy("Forbidden Forest", 299.99f, "French", new List<string>()));
        }

        [Test]
        public void CheckNullMagicalCreaturesException()
        {
            Assert.Throws<ArgumentException>(() => new Fantasy("Realm of the Undead", 399.99f, "German", null));
        }

        [Test]
        public void CheckAnotherFantasyBook()
        {
            Assert.That(fantasy2.Title, Is.EqualTo("Secrets of the Supernatural"));
            Assert.That(fantasy2.Price, Is.EqualTo(299.99f));
            Assert.That(fantasy2.LanguageOfPublication, Is.EqualTo("Spanish"));
            Assert.That(fantasy2.MagicalCreatures, Is.EquivalentTo(new List<string> { "Werewolves" }));
        }


    }
}
