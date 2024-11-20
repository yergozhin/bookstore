using System;
using System.Collections.Generic;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class AuthorTests
    {
        private readonly Author author1 = new Author("Stefan", "Salvatore", "Brother of Damon Salvatore");
        private readonly Author author2 = new Author("Damon", "Salvatore", "The vampire");
        private readonly Author authorWithoutBio = new Author("Elena", "Gilbert");

        [Test]
        public void CheckAuthor()
        {
            Assert.That(author1.FirstName, Is.EqualTo("Stefan"));
            Assert.That(author1.LastName, Is.EqualTo("Salvatore"));
            Assert.That(author1.Bio, Is.EqualTo("Brother of Damon Salvatore"));
        }

        [Test]
        public void CheckNullFirstName()
        {
            Assert.Throws<ArgumentException>(() => new Author("", "Salvatore", "The vampire"));
        }

        [Test]
        public void CheckNullLastName()
        {
            Assert.Throws<ArgumentException>(() => new Author("Stefan", "", "Brother of Damon Salvatore"));
        }
       
        [Test]
        public void CheckAuthorWithoutBio()
        {
            Assert.That(authorWithoutBio.FirstName, Is.EqualTo("Elena"));
            Assert.That(authorWithoutBio.LastName, Is.EqualTo("Gilbert"));
            Assert.That(authorWithoutBio.Bio, Is.Null);
        }

        [Test]
        public void CheckAuthorExtent()
        {
            List<Author> authors = Author.GetAuthors();
            Assert.That(authors.Count, Is.EqualTo(3));
            Assert.That(authors[0].FirstName, Is.EqualTo("Stefan"));
            Assert.That(authors[1].FirstName, Is.EqualTo("Damon"));
            Assert.That(authors[2].FirstName, Is.EqualTo("Elena"));
        }

        [Test]
        public void CheckEncapsulationInExtent()
        {
            author1.FirstName = "ChangedName";
            List<Author> authors = Author.GetAuthors();
            Assert.That(authors[0].FirstName, Is.EqualTo("ChangedName"));
        }

        [Test]
        public void CheckExtentPersistency()
        {
            BookstoreFileManager.SaveBookstore();
            Author.GetAuthors().Clear();
            Assert.That(Author.GetAuthors().Count, Is.EqualTo(0));
            BookstoreFileManager.LoadBookstore();
            List<Author> authors = Author.GetAuthors();
            Assert.That(authors.Count, Is.EqualTo(3));
            Assert.That(authors[0].FirstName, Is.EqualTo("Stefan"));
            Assert.That(authors[1].FirstName, Is.EqualTo("Damon"));
            Assert.That(authors[2].FirstName, Is.EqualTo("Elena"));
        }
    }
}
