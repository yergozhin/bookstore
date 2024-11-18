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
        private string type;
        private float amountInPercentage;
        private DateTime deadlineDate;

        public string Type
        {
            get => type;
            set
            {
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
            set { deadlineDate = value; }
        }

        public Discount(string type, float amountInPercentage, DateTime deadlineDate)
        {
            Type = type;
            AmountInPercentage = amountInPercentage;
            DeadlineDate = deadlineDate;
            discounts.Add(this);
        }

        public static List<Discount> GetDiscounts()
        {
            return discounts;
        }
    }
}
