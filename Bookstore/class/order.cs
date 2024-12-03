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

        private List<Employee> associatedEmployees = new List<Employee>();
        public IReadOnlyList<Employee> getAssociatedEmployees() => associatedEmployees.AsReadOnly();

        private List<Book> associatedBooks = new List<Book>();
        public IReadOnlyList<Book> getAssociatedBooks() => associatedBooks.AsReadOnly();

        private DateTime orderDate;
        private string status;
        private float totalAmount;
        //private List<Book> books;

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
                foreach (var book in associatedBooks)
                {
                    total += book.Price;
                }
                return total;
            }
        }
        /*public List<Book> Books
        {
            get
            {
                return new List<Book>(books);
            }
        }*/

        public Order(DateTime orderDate, string status)
        {
            OrderDate = orderDate;
            Status = status;
            //books = new List<Book>();
            totalAmount = 0;
            orders.Add(this);
        }

        public static void ClearOrders()
        {
            orders.Clear();
        }
        public static List<Order> GetOrders()
        {
            return new List<Order>(orders);
        }
        /*public static void Add(Order order)
        {
            orders.Add(order);
        }*/
        /*public void addBookToOrder(Book book)
        {
            //List<Book> _books = Book.GetBooks();
            foreach(Book b in _books){
                if(b.Title == book.Title)
                {
                    totalAmount = totalAmount + (float)b.Price;
                    books.Add(b);
                    return;
                }
            }
            Console.WriteLine("Book with title:", book.Title, "was not found, therefore it is not added to order");
        }*/
        /*public void deleteBookFromOrder(Book book)
        {
            if (!books.Contains(book))
            {
                return;
            }
            books.Remove(book);
            totalAmount = totalAmount - (float)book.Price;
        }*/
        public void assignEmployeeWhoProcesses(Employee employee)
        {
            if (!associatedEmployees.Contains(employee))
            {
                associatedEmployees.Add(employee);
                employee.assignOrder(this);
            }
        }
        public void removeEmployeeFromProcessing(Employee employee)
        {
            if (associatedEmployees.Contains(employee))
            {
                associatedEmployees.Remove(employee);
                employee.removeAssignedOrder(this);
            }
        }
        public void removeAllEmployees()
        {
            foreach (Employee employee in associatedEmployees)
            {
                removeEmployeeFromProcessing(employee);
            }
        }

        public void addBook(Book book)
        {
            if (!associatedBooks.Contains(book))
            {
                associatedBooks.Add(book);
                book.assignToOrder(this);
            }
        }
        public void removeBook(Book book)
        {
            if (associatedBooks.Contains(book))
            {
                associatedBooks.Remove(book);
                book.removeFromOrder(this);
            }
        }
        public void removeAllBooks()
        {
            foreach (Book book in associatedBooks)
            {
                removeBook(book);
            }
        }
    }
}
