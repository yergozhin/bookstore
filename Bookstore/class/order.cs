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

        public DateTime OrderDate
        {
            get => orderDate;
            set { orderDate = value; }
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

        public Order(DateTime orderDate, string status)
        {
            OrderDate = orderDate;
            Status = status;
            orders.Add(this);
        }

        public static List<Order> GetOrders()
        {
            return new List<Order>(orders);
        }
    }
}
