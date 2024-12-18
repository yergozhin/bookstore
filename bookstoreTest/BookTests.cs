using System;
using System.Collections.Generic;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class BookTests
    {
        private Book book1;
        private Book book2;
        private Author author;

        [SetUp]
        public void SetUp()
        {
            Book.ClearBooks();
            Author.ClearAuthors();

            book1 = new Book("How to Spot a Vampire", 299.90, "English", new List<string> { "Latin", "Italian" });
            book2 = new Book("The Bennett Book of Spells", 155, "English");

            author = new Author("Bonnie", "Bennett", "Powerful witch");
        }

        [Test]
        public void CheckBookAttributes()
        {
            Assert.That(book1.Title, Is.EqualTo("How to Spot a Vampire"));
            Assert.That(book1.Price, Is.EqualTo(299.90));
            Assert.That(book1.LanguageOfPublication, Is.EqualTo("English"));
            Assert.That(book1.ListOfLanguagesToWhichTranslated,
                Is.EquivalentTo(new List<string> { "Latin", "Italian" }));
        }

        [Test]
        public void CheckTitleLengthException()
        {
            Assert.Throws<ArgumentException>(() => new Book("Hi", 199.99, "English"));
            Assert.Throws<ArgumentException>(() => new Book(new string('A', 51), 199.99, "English"));
        }

        [Test]
        public void CheckPriceOutOfRangeException()
        {
            Assert.Throws<ArgumentException>(() => new Book("How to Spot a Vampire", -5, "English"));
            Assert.Throws<ArgumentException>(() => new Book("How to Spot a Vampire", 10001, "English"));
        }

        [Test]
        public void CheckEmptyLanguageException()
        {
            Assert.Throws<ArgumentException>(() => new Book("How to Spot a Vampire", 299.90, ""));
        }

        [Test]
        public void CheckNullLanguageException()
        {
            Assert.Throws<ArgumentException>(() => new Book("How to Spot a Vampire", 299.90, null));
        }

        [Test]
        public void CheckBookWithoutTranslations()
        {
            Assert.That(book2.Title, Is.EqualTo("The Bennett Book of Spells"));
            Assert.That(book2.Price, Is.EqualTo(155));
            Assert.That(book2.LanguageOfPublication, Is.EqualTo("English"));
            Assert.That(book2.ListOfLanguagesToWhichTranslated, Is.Empty);
        }

        [Test]
        public void CheckBookExtent()
        {
            List<Book> books = Book.GetBooks();
            Assert.That(books.Count, Is.EqualTo(2));
            Assert.That(books[0].Title, Is.EqualTo("How to Spot a Vampire"));
            Assert.That(books[1].Title, Is.EqualTo("The Bennett Book of Spells"));
        }

        [Test]
        public void CheckEncapsulationInExtent()
        {
            book1.Title = "Changed Title";
            List<Book> books = Book.GetBooks();
            Assert.That(books[0].Title, Is.EqualTo("Changed Title"));
        }

        [Test]
        public void CheckAssignAuthor_ShouldSetReverseConnection()
        {
            book1.AssignAuthor(author);

            Assert.That(book1.AssignedAuthor, Is.EqualTo(author));
            Assert.That(author.AuthoredBooks.Contains(book1), Is.True);
        }

        [Test]
        public void CheckRemoveAuthor_ShouldClearReverseConnection()
        {
            book1.AssignAuthor(author);
            book1.RemoveAuthor();

            Assert.That(book1.AssignedAuthor, Is.Null);
            Assert.That(author.AuthoredBooks.Contains(book1), Is.False);
        }

        [Test]
        public void CheckExtentPersistency()
        {
            BookstoreFileManager.SaveBookstore();
            Book.ClearBooks();
            Assert.That(Book.GetBooks().Count, Is.EqualTo(0));
            BookstoreFileManager.LoadBookstore();
            List<Book> books = Book.GetBooks();
            Assert.That(books.Count, Is.EqualTo(2));
            Assert.That(books[0].Title, Is.EqualTo("How to Spot a Vampire"));
            Assert.That(books[1].Title, Is.EqualTo("The Bennett Book of Spells"));
        }

        [Test]
        public void CheckDuplicateBookNotAdded()
        {
            Book duplicateBook = new Book("How to Spot a Vampire", 299.90, "English");
            List<Book> books = Book.GetBooks();
            Assert.That(books.Count, Is.EqualTo(3)); // book1, book2, duplicate
        }
    }
}