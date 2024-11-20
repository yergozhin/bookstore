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
                Book.ClearBooks();
                Author.ClearAuthors();
                Discount.ClearDiscounts();
                Order.ClearOrders();
                Publisher.ClearPublishers();
                Review.ClearReviews();
                User.ClearUsers();
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
                        Book.ClearBooks();
                        Author.ClearAuthors();
                        Discount.ClearDiscounts();
                        Order.ClearOrders();
                        Publisher.ClearPublishers();
                        Review.ClearReviews();
                        User.ClearUsers();

                        // Adding loaded books, authors and etc. to static lists
                        foreach (var book in bookstore.Books)
                        {
                            Book.Add(book);
                        }
                        foreach (var author in bookstore.Authors)
                        {
                            Author.Add(author);
                        }
                        foreach (var discount in bookstore.Discounts)
                        {
                            Discount.Add(discount);
                        }
                        foreach (var order in bookstore.Orders)
                        {
                            Order.Add(order);
                        }
                        foreach (var publisher in bookstore.Publishers)
                        {
                            Publisher.Add(publisher);
                        }
                        foreach (var review in bookstore.Reviews)
                        {
                            Review.Add(review);
                        }
                        foreach (var user in bookstore.Users)
                        {
                            User.Add(user);
                        }

                        return true;
                    }
                    catch (Exception)
                    {
                        Book.ClearBooks();
                        Author.ClearAuthors();
                        Discount.ClearDiscounts();
                        Order.ClearOrders();
                        Publisher.ClearPublishers();
                        Review.ClearReviews();
                        User.ClearUsers();
                        return false;
                    }
                }
            }
        }
    }
}

