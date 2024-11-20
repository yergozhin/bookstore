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
                            //Book.Add(book);
                            Book newBook = new Book(book.Title,book.Price,book.LanguageOfPublication,book.ListOfLanguagesToWhichTranslated);
                        }
                        foreach (var author in bookstore.Authors)
                        {
                            Author newAuthor = new Author(author.FirstName, author.LastName, author.Bio);
                            //Author.Add(author);
                        }
                        foreach (var discount in bookstore.Discounts)
                        {
                            Discount newDiscount = new Discount(discount.Type, discount.AmountInPercentage, discount.DeadlineDate);
                            //Discount.Add(discount);
                        }
                        foreach (var order in bookstore.Orders)
                        {
                            Order newOrder = new Order(order.OrderDate, order.Status);
                            for(int i = 0; i < order.Books.Count(); i++)
                            {
                                newOrder.addBookToOrder(order.Books[i]);
                            }
                            //Order.Add(order);
                        }
                        foreach (var publisher in bookstore.Publishers)
                        {
                            Publisher newPublisher = new Publisher(publisher.Name, publisher.Address, publisher.Email, publisher.PhoneNumber);
                            //Publisher.Add(publisher);
                        }
                        foreach (var review in bookstore.Reviews)
                        {
                            Review newReview = new Review(review.Rating, review.Comment, review.ReviewDate);
                            //Review.Add(review);
                        }
                        foreach (var user in bookstore.Users)
                        {
                            User newUser = new User(user.Name, user.PhoneNumber, user.Email, user.DateOfBirth);
                            //User.Add(user);
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

