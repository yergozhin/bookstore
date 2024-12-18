using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Bookstore.@class
{
    [Serializable]
    public class Discount
    {
        private static List<Discount> discounts = new List<Discount>();

        private List<Customer> associatedCustomers = new List<Customer>();
        public IReadOnlyList<Customer> getAssociatedCustomers() => associatedCustomers.AsReadOnly();

        private string type;
        private float amountInPercentage;
        private DateTime deadlineDate;

        public string Type
        {
            get => type;
            set
            {
                if (value != "Single Use" && value != "Multiple Use")
                {
                    throw new ArgumentException("Type must be either Single Use or Multiple Use");
                }
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Type cannot be empty.");
                }
                type = value;
            }
        }

        public float AmountInPercentage
        {
            get => amountInPercentage;
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("Amount in percentage must be between 0 and 100.");
                }
                amountInPercentage = value;
            }
        }

        public DateTime DeadlineDate
        {
            get => deadlineDate;
            set
            {
                if (DateTime.Today > value)
                {
                    throw new ArgumentException("Invalid date of deadline.");
                }
                deadlineDate = value;
            }
        }

        public Discount(string type, float amountInPercentage, DateTime deadlineDate)
        {
            Type = type;
            AmountInPercentage = amountInPercentage;
            DeadlineDate = deadlineDate;
            discounts.Add(this);
        }

        public static void ClearDiscounts()
        {
            discounts.Clear();
        }
        public static List<Discount> GetDiscounts()
        {
            return new List<Discount>(discounts);
        }
        public void addCustomer(Customer customer)
        {
            if (!associatedCustomers.Contains(customer))
            {
                associatedCustomers.Add(customer);
                customer.addDiscount(this);
            }
        }
        public void removeCustomer(Customer customer)
        {
            if (associatedCustomers.Contains(customer))
            {
                associatedCustomers.Remove(customer);
                customer.removeDiscount(this);
            }
        }
    }
}
