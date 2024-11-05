using System;
using System.Xml;
using System.Xml.Serialization;

namespace Bookstore.@class
{
	public class BookstoreFileManager
	{
		public BookstoreFileManager()
		{
		}
        private static string bookstoreFilePath = "bookstore.xml";
        public static void SaveBookstore()
        {
            BookstoreContainer bookstore = new BookstoreContainer
            {
                Books = Book.GetBooks(),
                Authors = Author.GetAuthors(),
                Discounts = Discount.GetDiscounts(),
                Orders = Order.GetOrders(),
                Publishers = Publisher.GetPublishers(),
                Reviews = Review.GetReviews(),
                Users = User.GetUsers()
            };

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(BookstoreContainer));
            using (StreamWriter file = File.CreateText(bookstoreFilePath))
            {
                using (XmlTextWriter writer = new XmlTextWriter(file))
                {
                    xmlSerializer.Serialize(writer, bookstore);
                }
            }
        }
        public static bool LoadBookstore()
        {
            if (!File.Exists(bookstoreFilePath))
            {
                Book.GetBooks().Clear();
                Author.GetAuthors().Clear();
                Discount.GetDiscounts().Clear();
                Order.GetOrders().Clear();
                Publisher.GetPublishers().Clear();
                Review.GetReviews().Clear();
                User.GetUsers().Clear();
                return false;
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(BookstoreContainer));
            using (StreamReader file = File.OpenText(bookstoreFilePath))
            {
                using (XmlTextReader reader = new XmlTextReader(file))
                {
                    try
                    {
                        BookstoreContainer bookstore = (BookstoreContainer)xmlSerializer.Deserialize(reader);
                        Book.GetBooks().Clear();
                        Author.GetAuthors().Clear();
                        Discount.GetDiscounts().Clear();
                        Order.GetOrders().Clear();
                        Publisher.GetPublishers().Clear();
                        Review.GetReviews().Clear();
                        User.GetUsers().Clear();

                        // Adding loaded books, authors and etc. to static lists
                        foreach (var book in bookstore.Books)
                        {
                            Book.GetBooks().Add(book);
                        }
                        foreach (var author in bookstore.Authors)
                        {
                            Author.GetAuthors().Add(author);
                        }
                        foreach (var discount in bookstore.Discounts)
                        {
                            Discount.GetDiscounts().Add(discount);
                        }
                        foreach (var order in bookstore.Orders)
                        {
                            Order.GetOrders().Add(order);
                        }
                        foreach (var publisher in bookstore.Publishers)
                        {
                            Publisher.GetPublishers().Add(publisher);
                        }
                        foreach (var review in bookstore.Reviews)
                        {
                            Review.GetReviews().Add(review);
                        }
                        foreach (var user in bookstore.Users)
                        {
                            User.GetUsers().Add(user);
                        }

                        return true;
                    }
                    catch (Exception)
                    {
                        Book.GetBooks().Clear();
                        Author.GetAuthors().Clear();
                        Discount.GetDiscounts().Clear();
                        Order.GetOrders().Clear();
                        Publisher.GetPublishers().Clear();
                        Review.GetReviews().Clear();
                        User.GetUsers().Clear();
                        return false;
                    }
                }
            }
        }
    }
}

