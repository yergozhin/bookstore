using System;
using System.Xml.Serialization;

namespace Bookstore.@class
{
    [Serializable]
    [XmlRoot("Bookstore")]
    public class BookstoreContainer
    {
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Author> Authors { get; set; } = new List<Author>();
        public List<Discount> Discounts { get; set; } = new List<Discount>();
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<Publisher> Publishers { get; set; } = new List<Publisher>();
        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<User> Users { get; set; } = new List<User>();
    }
}
