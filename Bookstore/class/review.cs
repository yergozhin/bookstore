using System;
using System.Collections.Generic;

namespace Bookstore.@class
{
    [Serializable]
    public class Review
    {
        private static List<Review> reviews = new List<Review>();

        private Book associatedBook;
        private Customer associatedCustomer;

        private int rating;
        private string comment;
        private DateTime reviewDate;

        public int Rating
        {
            get => rating;
            set
            {
                if (value < 1 || value > 5)
                {
                    throw new ArgumentException("Rating must be between 1 and 5.");
                }
                rating = value;
            }
        }

        public string Comment
        {
            get => comment;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Comment cannot be empty.");
                }
                comment = value;
            }
        }

        public DateTime ReviewDate
        {
            get => reviewDate;
            set
            {
                if (DateTime.Today < value)
                {
                    throw new ArgumentException("Invalid review date.");
                }
                reviewDate = value;
            }
        }

        public Book AssociatedBook
        {
            get => associatedBook;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Book cannot be null.");
                }
                associatedBook = value;
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

        public Review(int rating, string comment, DateTime reviewDate)
        {
            Rating = rating;
            Comment = comment;
            ReviewDate = reviewDate;
            reviews.Add(this);
        }

        public static void ClearReviews()
        {
            reviews.Clear();
        }

        public static List<Review> GetReviews()
        {
            return new List<Review>(reviews);
        }

        public void assignBook(Book book)
        {
            if (book == null)
                throw new ArgumentException("Book cannot be null.");

            if (associatedBook != book)
            {
                associatedBook = book;
                if (!book.getAssociatedReviews().Contains(this))
                {
                    book.addReview(this);
                }
            }
        }

        public void removeFromBook(Book book)
        {
            if (book == null)
                throw new ArgumentException("Book cannot be null.");

            if (associatedBook == book)
            {
                associatedBook = null;
                if (book.getAssociatedReviews().Contains(this))
                {
                    book.removeReview(this);
                }
            }
        }

        public void assignCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentException("Customer cannot be null.");

            if (associatedCustomer == null)
            {
                associatedCustomer = customer;
                customer.addReview(this);
            }
        }

        public void removeFromCustomer()
        {
            if (associatedCustomer != null)
            {
                var tempCustomer = associatedCustomer;
                associatedCustomer = null;
                tempCustomer.removeReview(this);
            }
        }

        // --- Метод обновления книги ---
        public void updateBook(Book newBook)
        {
            if (newBook == null)
                throw new ArgumentException("New book cannot be null.");

            if (associatedBook != null)
            {
                removeFromBook(associatedBook);
            }
            assignBook(newBook);
        }
    }
}
