using System;
using System.Collections.Generic;

namespace Bookstore.@class
{
    [Serializable]
    public abstract class OrderBase
    {
        private static List<OrderBase> allOrders = new List<OrderBase>();
        public static void ClearOrders() => allOrders.Clear();
        public static List<OrderBase> GetAllOrders() => new List<OrderBase>(allOrders);

        protected List<OverlappingUser> associatedEmployees = new List<OverlappingUser>();
        protected List<Book> associatedBooks = new List<Book>();

        public IReadOnlyList<OverlappingUser> GetAssociatedEmployees() => associatedEmployees.AsReadOnly();
        public IReadOnlyList<Book> GetAssociatedBooks() => associatedBooks.AsReadOnly();

        protected DateTime orderDate;
        public DateTime OrderDate
        {
            get { return orderDate; }
            protected set { orderDate = value; }
        }
        public abstract string Status { get; }
        
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

        protected OrderBase(DateTime orderDate)
        {
            if (DateTime.Today < orderDate)
                throw new ArgumentException("Invalid order date.");
            this.orderDate = orderDate;

            allOrders.Add(this);
        }
        
        protected OrderBase(OrderBase oldOrder)
        {
            this.orderDate = oldOrder.orderDate;
            
            foreach(var emp in oldOrder.associatedEmployees)
            {
                this.AssignUserAsEmployee(emp);
            }
            foreach(var b in oldOrder.associatedBooks)
            {
                this.AddBook(b);
            }

            
            allOrders.Add(this);
            allOrders.Remove(oldOrder);
        }
        
        public abstract OrderBase NextState(string action);
        
        public void AssignUserAsEmployee(OverlappingUser user)
        {
            if (!user.Roles.HasFlag(UserRole.Employee))
                throw new InvalidOperationException("User does not have Employee role.");

            if (!associatedEmployees.Contains(user))
            {
                associatedEmployees.Add(user);
                user.AssignOrder(this);
            }
        }
        public void RemoveUserAsEmployee(OverlappingUser user)
        {
            if (associatedEmployees.Contains(user))
            {
                associatedEmployees.Remove(user);
                user.RemoveAssignedOrder(this);
            }
        }
        
        public void AddBook(Book book)
        {
            if (!associatedBooks.Contains(book))
            {
                associatedBooks.Add(book);
                book.AssignToOrder(this);
            }
        }
        public void RemoveBook(Book book)
        {
            if (associatedBooks.Contains(book))
            {
                associatedBooks.Remove(book);
                book.RemoveFromOrder(this);
            }
        }
    }
}
