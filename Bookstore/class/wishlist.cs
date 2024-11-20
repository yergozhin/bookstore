using System;
using System.Collections.Generic;

namespace Bookstore.@class
{
    [Serializable]
    public class Wishlist
    {
        private List<Book> booksInWishlist;
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

        public List<Book> BooksInWishlist
        {
            get => booksInWishlist ?? new List<Book>();
            set => booksInWishlist = value ?? new List<Book>();
        }

        public Wishlist()
        {
            booksInWishlist = new List<Book>();
        }
    }
}
