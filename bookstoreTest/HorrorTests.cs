using System;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class HorrorTests
    {
        private readonly Horror horror1 = new Horror("Mystic Legends", 199.99f, "English", "Supernatural", 7);
        private readonly Horror horror2 = new Horror("The Vampire's Secret", 299.99f, "French", "Paranormal", 5);

        [Test]
        public void CheckHorrorAttributes()
        {
            Assert.That(horror1.Title, Is.EqualTo("Mystic Legends"));
            Assert.That(horror1.Price, Is.EqualTo(199.99f));
            Assert.That(horror1.LanguageOfPublication, Is.EqualTo("English"));
            Assert.That(horror1.Type, Is.EqualTo("Supernatural"));
            Assert.That(horror1.LevelOfScariness, Is.EqualTo(7));
        }

        [Test]
        public void CheckEmptyTypeException()
        {
            Assert.Throws<ArgumentException>(() => new Horror("Haunted Woods", 299.99f, "German", "", 6));
        }

        [Test]
        public void CheckLevelOutOfRangeException()
        {
            Assert.Throws<ArgumentException>(() => new Horror("Dark Shadows", 199.99f, "Spanish", "Horror", 0));
            Assert.Throws<ArgumentException>(() => new Horror("Dark Shadows", 199.99f, "Spanish", "Horror", 11));
        }

        [Test]
        public void CheckAnotherHorrorBook()
        {
            Assert.That(horror2.Title, Is.EqualTo("The Vampire's Secret"));
            Assert.That(horror2.Price, Is.EqualTo(299.99f));
            Assert.That(horror2.LanguageOfPublication, Is.EqualTo("French"));
            Assert.That(horror2.Type, Is.EqualTo("Paranormal"));
            Assert.That(horror2.LevelOfScariness, Is.EqualTo(5));
        }


    }
}
