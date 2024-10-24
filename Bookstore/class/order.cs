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

        public Order(DateTime orderDate, string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                throw new ArgumentException("Status cannot be empty.");
            }

            this.orderDate = orderDate;
            this.status = status;
            addOrder(this);
        }

        private static void addOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentException("Order cannot be null");
            }
            orders.Add(order);
        }

        public static List<Order> GetOrders()
        {
            return new List<Order>(orders);
        }

        public static void SaveOrders(string path = "orders.xml")
        {
            StreamWriter file = File.CreateText(path);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));
            using (XmlTextWriter writer = new XmlTextWriter(file))
            {
                xmlSerializer.Serialize(writer, orders);
            }
        }

      
        public static bool LoadOrders(string path = "orders.xml")
        {
            StreamReader file;
            try
            {
                file = File.OpenText(path);
            }
            catch (FileNotFoundException)
            {
                orders.Clear();
                return false;
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));
            using (XmlTextReader reader = new XmlTextReader(file))
            {
                try
                {
                    orders = (List<Order>)xmlSerializer.Deserialize(reader);
                }
                catch (InvalidCastException)
                {
                    orders.Clear();
                    return false;
                }
                catch (Exception)
                {
                    orders.Clear();
                    return false;
                }
            }
            return true;
        }
    }
}
