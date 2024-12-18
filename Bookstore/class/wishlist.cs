using System;
using System.Collections.Generic;

namespace Bookstore.@class
{
    [Serializable]
    public class Wishlist
    {
        private List<Book> associatedBooks = new List<Book>();
        public IReadOnlyList<Book> GetAssociatedBooks() => associatedBooks.AsReadOnly();

        private Customer associatedCustomer;
        private int maxCapacity = 1000;

        public int MaxCapacity
        {
            get => maxCapacity;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Max capacity must be greater than zero.");
                }
                maxCapacity = value;
            }
        }

        public Customer AssociatedCustomer
        {
            get => associatedCustomer;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Customer cannot be null.");
                }
                associatedCustomer = value;
            }
        }

        public Wishlist() { }

        // --- Методы для управления книгами ---
        public void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentException("Book cannot be null.");

            if (associatedBooks.Count >= maxCapacity)
                throw new InvalidOperationException("Wishlist has reached its maximum capacity.");

            if (!associatedBooks.Contains(book))
            {
                associatedBooks.Add(book);
                book.assignToWishlist(this); // Обратная связь
            }
        }

        public void RemoveBook(Book book)
        {
            if (book == null)
                throw new ArgumentException("Book cannot be null.");

            if (associatedBooks.Contains(book))
            {
                associatedBooks.Remove(book);
                book.removeFromWishlist(this); // Обратная связь
            }
        }

        public void UpdateBook(Book oldBook, Book newBook)
        {
            if (oldBook == null || newBook == null)
                throw new ArgumentException("Books cannot be null.");

            if (associatedBooks.Contains(oldBook))
            {
                RemoveBook(oldBook);
                AddBook(newBook);
            }
            else
            {
                throw new InvalidOperationException("The book to be replaced does not exist in the wishlist.");
            }
        }

        // --- Методы для управления клиентом ---
        public void AssignCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentException("Customer cannot be null.");

            if (associatedCustomer != customer)
            {
                associatedCustomer = customer;
                customer.assignWishlist(this); // Обратная связь
            }
        }

        public void RemoveFromCustomer()
        {
            if (associatedCustomer != null)
            {
                var tempCustomer = associatedCustomer;
                associatedCustomer = null;
                tempCustomer.removeFromWishlist(this); // Обратная связь
            }
        }
    }
}
