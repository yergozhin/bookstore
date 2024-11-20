using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Bookstore.@class
{
    [Serializable]
    public class Order
    {
        private static List<Order> orders = new List<Order>();

        private DateTime orderDate;
        private string status;
        private float totalAmount;
        private List<Book> books;

        public DateTime OrderDate
        {
            get => orderDate;
            set
            {
                if (DateTime.Today < value)
                {
                    throw new ArgumentException("Invalid order date.");
                }
                orderDate = value;
            }
        }

        public string Status
        {
            get => status;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Status cannot be empty.");
                }
                status = value;
            }
        }

        public double TotalAmount
        {
            get
            {
                double total = 0;
                foreach (var book in books)
                {
                    total += book.Price;
                }
                return total;
            }
        }


        public Order(DateTime orderDate, string status)
        {
            OrderDate = orderDate;
            Status = status;
            books = new List<Book>();
            totalAmount = 0;
            orders.Add(this);
        }

        public static List<Order> GetOrders()
        {
            return orders;
        }

        public void addBookToOrder(Book book)
        {
            totalAmount = totalAmount + (float)book.Price;
            books.Add(book);
        }
        public void deleteBookFromOrder(Book book)
        {
            if (!books.Contains(book))
            {
                return;
            }
            books.Remove(book);
            totalAmount = totalAmount - (float)book.Price;
        }
    }
}
