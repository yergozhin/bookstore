using System;
using System.Collections.Generic;

namespace Bookstore.@class
{
    [Serializable]
    public class Author
    {
        private static List<Author> authors = new List<Author>();

        private string firstName;
        private string lastName;
        private string bio;

        private List<Book> authoredBooks = new List<Book>(); // Ассоциация с книгами

        public IReadOnlyList<Book> AuthoredBooks => authoredBooks.AsReadOnly();

        public string FirstName
        {
            get => firstName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("First name cannot be empty.");
                }

                firstName = value;
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Last name cannot be empty.");
                }

                lastName = value;
            }
        }

        public string Bio
        {
            get => bio;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Bio cannot be empty.");
                }

                bio = value;
            }
        }

        public Author(string firstName, string lastName, string bio = null)
        {
            FirstName = firstName;
            LastName = lastName;
            Bio = bio;
            authors.Add(this);
        }

        public static void ClearAuthors()
        {
            authors.Clear();
        }

        public static List<Author> GetAuthors()
        {
            return new List<Author>(authors);
        }

        // --- Методы для ассоциации с книгами ---
        public void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentException("Book cannot be null.");

            if (!authoredBooks.Contains(book))
            {
                authoredBooks.Add(book);
                book.AssignAuthor(this); // Обратная связь
            }
        }

        public void RemoveBook(Book book)
        {
            if (book == null)
                throw new ArgumentException("Book cannot be null.");

            if (authoredBooks.Contains(book))
            {
                authoredBooks.Remove(book);
                book.RemoveAuthor(); // Обратная связь
            }
        }

        public void UpdateBook(Book oldBook, Book newBook)
        {
            RemoveBook(oldBook);
            AddBook(newBook);
        }
    }
}