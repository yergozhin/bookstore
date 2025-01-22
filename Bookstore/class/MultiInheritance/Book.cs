using System;
using System.Collections.Generic;

namespace Bookstore.@class
{
    [Serializable]
    public class Book
    {
        private static List<Book> books = new List<Book>();

        private List<Wishlist> associatedWishlists = new List<Wishlist>();
        private List<OrderBase> associatedOrders = new List<OrderBase>();

        private string title;
        private double price;
        private string languageOfPublication;
        private List<string> listOfLanguagesToWhichTranslated;

        private int lengthMax = 50;
        private int lengthMin = 3;
        private int maxPrice = 10000;

        public string Title
        {
            get => title;
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < lengthMin || value.Length > lengthMax)
                    throw new ArgumentException($"Title must be between {lengthMin} and {lengthMax} characters.");
                title = value;
            }
        }

        public double Price
        {
            get => price;
            set
            {
                if (value < 0 || value > maxPrice)
                    throw new ArgumentException($"Price must be between 0 and {maxPrice}.");
                price = value;
            }
        }

        public string LanguageOfPublication
        {
            get => languageOfPublication;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Language of publication cannot be empty.");
                languageOfPublication = value;
            }
        }

        public List<string> ListOfLanguagesToWhichTranslated
        {
            get => new List<string>(listOfLanguagesToWhichTranslated);
            set
            {
                if(value != null)
                {
                    foreach (var lang in value)
                    {
                        if (string.IsNullOrEmpty(lang))
                            throw new ArgumentException("Translated language cannot be empty.");
                    }
                }
                listOfLanguagesToWhichTranslated = value ?? new List<string>();
            }
        }

        public Book()
        {
            ListOfLanguagesToWhichTranslated = new List<string>();
        }

        public Book(string title, double price, string languageOfPublication, List<string> translations = null)
        {
            Title = title;
            Price = price;
            LanguageOfPublication = languageOfPublication;
            ListOfLanguagesToWhichTranslated = translations ?? new List<string>();
            books.Add(this);
        }

        public static void ClearBooks() => books.Clear();
        public static List<Book> GetBooks() => new List<Book>(books);

        // Link to Wishlists
        public void AssignToWishlist(Wishlist wishlist)
        {
            if (!associatedWishlists.Contains(wishlist))
            {
                associatedWishlists.Add(wishlist);
                wishlist.AddBook(this);
            }
        }
        public void RemoveFromWishlist(Wishlist wishlist)
        {
            if (associatedWishlists.Contains(wishlist))
            {
                associatedWishlists.Remove(wishlist);
                wishlist.RemoveBook(this);
            }
        }
        // Link to Orders
        public void AssignToOrder(OrderBase order)
        {
            if (!associatedOrders.Contains(order))
            {
                associatedOrders.Add(order);
                order.AddBook(this);
            }
        }
        public void RemoveFromOrder(OrderBase order)
        {
            if (associatedOrders.Contains(order))
            {
                associatedOrders.Remove(order);
                order.RemoveBook(this);
            }
        }
    }
}
