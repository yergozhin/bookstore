using System;
using System.Collections.Generic;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class MangaTests
    {
        private Manga manga1;
        private Manga manga2;

        [SetUp]
        public void Setup()
        {
            Book.ClearBooks();

            manga1 = new Manga("Mystic Warriors", 79.99f, "Japanese", 3);
            manga2 = new Manga("Vampire Chronicles", 89.99f, "English", 5);
        }

        [Test]
        public void CheckMangaAttributes()
        {
            Assert.That(manga1.Title, Is.EqualTo("Mystic Warriors"));
            Assert.That(manga1.Price, Is.EqualTo(79.99f));
            Assert.That(manga1.LanguageOfPublication, Is.EqualTo("Japanese"));
            Assert.That(manga1.Volume, Is.EqualTo(3));
        }

        [Test]
        public void CheckInvalidVolumeException()
        {
            Assert.Throws<ArgumentException>(() => new Manga("Mystic Shadows", 69.99f, "Korean", 0));
        }

        [Test]
        public void CheckAnotherMangaBook()
        {
            Assert.That(manga2.Title, Is.EqualTo("Vampire Chronicles"));
            Assert.That(manga2.Price, Is.EqualTo(89.99f));
            Assert.That(manga2.LanguageOfPublication, Is.EqualTo("English"));
            Assert.That(manga2.Volume, Is.EqualTo(5));
        }

        [Test]
        public void CheckMangaExtent()
        {
            List<Manga> mangas = Book.GetBooks().ConvertAll(book => book as Manga).FindAll(b => b != null);
            Assert.That(mangas.Count, Is.EqualTo(2));
            Assert.That(mangas[0].Title, Is.EqualTo("Mystic Warriors"));
            Assert.That(mangas[1].Title, Is.EqualTo("Vampire Chronicles"));
        }

        [Test]
        public void CheckEncapsulationInExtent()
        {
            manga1.Title = "Changed Title";
            List<Manga> mangas = Book.GetBooks().ConvertAll(book => book as Manga).FindAll(b => b != null);
            Assert.That(mangas[0].Title, Is.EqualTo("Changed Title"));
        }

        [Test]
        public void CheckExtentPersistency()
        {
            BookstoreFileManager.SaveBookstore();
            Book.ClearBooks();
            Assert.That(Book.GetBooks().Count, Is.EqualTo(0));
            BookstoreFileManager.LoadBookstore();
            List<Manga> mangas = Book.GetBooks().ConvertAll(book => book as Manga).FindAll(b => b != null);
            Assert.That(mangas.Count, Is.EqualTo(2));
            Assert.That(mangas[0].Title, Is.EqualTo("Mystic Warriors"));
            Assert.That(mangas[1].Title, Is.EqualTo("Vampire Chronicles"));
        }
    }
}
