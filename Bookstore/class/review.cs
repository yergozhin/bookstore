using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

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
            //To check with team
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
            //To check with team
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
            //To check with team
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
        /*public static void Add(Review review)
        {
            reviews.Add(review);
        }*/
        public void assignBook(Book book)
        {
            if (associatedBook != null)
            {
                AssociatedBook = book;
                book.addReview(this);
            }
        }
        public void removeFromBook(Book book)
        {
            if (associatedBook != null)
            {
                AssociatedBook = null;
                book.removeReview(this);
            }
        }
        
        public void assignCustomer(Customer customer)
        {
            if (associatedCustomer == null)
            {
                AssociatedCustomer = customer;
                customer.addReview(this);
            }
        }
        public void removeFromCustomer(Customer customer)
        {
            if (associatedCustomer != null)
            {
                associatedCustomer = null;
                customer.removeReview(this);
            }
        }
    }
}
