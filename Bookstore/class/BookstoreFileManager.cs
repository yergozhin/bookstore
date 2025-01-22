using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Bookstore.@class
{
    public class BookstoreFileManager
    {
        private static string bookstoreFilePath = "bookstore.xml";

        public static void SaveBookstore()
        {
            BookstoreContainer container = new BookstoreContainer
            {
                Books = Book.GetBooks(),
                Authors = Author.GetAuthors(),
                Discounts = Discount.GetDiscounts(),
                Orders = OrderBase.GetAllOrders(),
                Publishers = Publisher.GetPublishers(),
                Reviews = Review.GetReviews(),
                Users = OverlappingUser.GetUsers()
            };

            XmlSerializer serializer = new XmlSerializer(typeof(BookstoreContainer));
            using (StreamWriter file = File.CreateText(bookstoreFilePath))
            {
                using (XmlTextWriter writer = new XmlTextWriter(file))
                {
                    serializer.Serialize(writer, container);
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
                OrderBase.ClearOrders();
                Publisher.ClearPublishers();
                Review.ClearReviews();
                OverlappingUser.ClearUsers();
                return false;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(BookstoreContainer));
            using (StreamReader file = File.OpenText(bookstoreFilePath))
            {
                using (XmlTextReader reader = new XmlTextReader(file))
                {
                    try
                    {
                        BookstoreContainer container = (BookstoreContainer)serializer.Deserialize(reader);
                        
                        Book.ClearBooks();
                        Author.ClearAuthors();
                        Discount.ClearDiscounts();
                        OrderBase.ClearOrders();
                        Publisher.ClearPublishers();
                        Review.ClearReviews();
                        OverlappingUser.ClearUsers();
                        
                        foreach (var b in container.Books)
                        {
                            new Book(b.Title, b.Price, b.LanguageOfPublication, b.ListOfLanguagesToWhichTranslated);
                        }
                        foreach (var a in container.Authors)
                        {
                            new Author(a.FirstName, a.LastName, a.Bio);
                        }
                        foreach (var d in container.Discounts)
                        {
                            new Discount(d.Type, d.AmountInPercentage, d.DeadlineDate);
                        }
                        foreach (var o in container.Orders)
                        {
                            
                            if (o is CreatedOrder co)
                            {
                                new CreatedOrder(o.OrderDate);
                                
                            }
                            else if (o is PendingPaymentOrder ppo)
                            {
                                new PendingPaymentOrder(o.OrderDate);
                            }
                            else if (o is PaymentCancelledOrder pco)
                            {
                                new PaymentCancelledOrder(pco); 
                            }
                            else if (o is ShippingOrder so)
                            {
                                new ShippingOrder(so);
                            }
                            else if (o is DeliveringOrder doo)
                            {
                                new DeliveringOrder(doo);
                            }
                            else if (o is CompletedOrder cpo)
                            {
                                new CompletedOrder(cpo);
                            }
                        }
                        foreach (var pub in container.Publishers)
                        {
                            new Publisher(pub.Name, pub.Address, pub.Email, pub.PhoneNumber);
                        }
                        foreach (var r in container.Reviews)
                        {
                            new Review(r.Rating, r.Comment, r.ReviewDate);
                        }
                        foreach (var u in container.Users)
                        {
                            OverlappingUser newUser = new OverlappingUser(
                                u.Name,
                                u.PhoneNumber,
                                u.Email,
                                u.DateOfBirth,
                                u.Roles
                            );
                            
                        }

                        return true;
                    }
                    catch
                    {
                        Book.ClearBooks();
                        Author.ClearAuthors();
                        Discount.ClearDiscounts();
                        OrderBase.ClearOrders();
                        Publisher.ClearPublishers();
                        Review.ClearReviews();
                        OverlappingUser.ClearUsers();
                        return false;
                    }
                }
            }
        }
    }
}
