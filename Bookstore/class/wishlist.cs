using System;
using System.Collections.Generic;

namespace Bookstore.@class
{
    [Serializable]
    public class Wishlist
    {
        private List<Book> associatedBooks = new List<Book>();
        public IReadOnlyList<Book> GetAssociatedBooks() => associatedBooks.AsReadOnly();

        private int maxCapacity = 1000;
        public int MaxCapacity
        {
            get => maxCapacity;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Max capacity must be greater than zero.");
                maxCapacity = value;
            }
        }

        public Wishlist() { }

        public void AddBook(Book book)
        {
            if (!associatedBooks.Contains(book))
            {
                associatedBooks.Add(book);
                book.AssignToWishlist(this);
            }
        }

        public void RemoveBook(Book book)
        {
            if (associatedBooks.Contains(book))
            {
                associatedBooks.Remove(book);
                book.RemoveFromWishlist(this);
            }
        }

        public void RemoveAllBooks()
        {
            var temp = new List<Book>(associatedBooks);
            foreach (Book b in temp)
            {
                RemoveBook(b);
            }
        }
    }
}