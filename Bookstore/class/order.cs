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

        private Customer associatedCustomer;

        private DateTime orderDate;
        private string status;

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

        public Order(DateTime orderDate, string status)
        {
            OrderDate = orderDate;
            Status = status;
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

        public void updateBook(Book oldBook, Book newBook)
        {
            removeBook(oldBook);
            addBook(newBook);
        }

        public void assignCustomer(Customer customer)
        {
            if (associatedCustomer == null)
            {
                AssociatedCustomer = customer;
                customer.addOrder(this);
            }
        }

        public void removeFromCustomer()
        {
            if (associatedCustomer != null)
            {
                var tempCustomer = associatedCustomer;
                associatedCustomer = null;
                tempCustomer.removeOrder(this);
            }
        }
    }
}