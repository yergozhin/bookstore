using System;

namespace Bookstore.@class
{
    public class Employee : User
    {
        private List<Order> associatedOrders = new List<Order>();
        public IReadOnlyList<Order> getAssociatedOrders() => associatedOrders.AsReadOnly();

        private string _position;
        public string Position
        {
            get => _position;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Position cannot be empty.");
                }
                _position = value;
            }
        }

        public Employee(string name, string phoneNumber, string email, DateTime dateOfBirth, string position)
            : base(name, phoneNumber, email, dateOfBirth)
        {
            Position = position;
        }
        public void assignOrder(Order order)
        {
            if (!associatedOrders.Contains(order))
            {
                associatedOrders.Add(order);
                order.assignEmployeeWhoProcesses(this);
            }
        }
        public void removeAssignedOrder(Order order)
        {
            if (associatedOrders.Contains(order))
            {
                associatedOrders.Remove(order);
                order.removeEmployeeFromProcessing(this);
            }
        }
        public void removeAllOrders()
        {
            foreach (Order order in associatedOrders)
            {
                removeAssignedOrder(order);
            }
        }
    }
}
