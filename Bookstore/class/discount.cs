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
            get { return type; }
            set { type = value; }
        }

        public float AmountInPercentage
        {
            get { return amountInPercentage; }
            set { amountInPercentage = value; }
        }

        public DateTime DeadlineDate
        {
            get { return deadlineDate; }
            set { deadlineDate = value; }
        }

        public Discount(string type, float amountInPercentage, DateTime deadlineDate)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentException("Type cannot be empty.");
            }

            if (amountInPercentage < 0 || amountInPercentage > 100)
            {
                throw new ArgumentException("Amount in percentage must be between 0 and 100.");
            }

            this.type = type;
            this.amountInPercentage = amountInPercentage;
            this.deadlineDate = deadlineDate;
            addDiscount(this);
        }
        private static void addDiscount(Discount discount)
        {
            if (discount == null)
            {
                throw new ArgumentException("Discount cannot be null");
            }
            discounts.Add(discount);
        }

        public static List<Discount> GetDiscounts()
        {
            return new List<Discount>(discounts);
        }
        public static void SaveDiscounts(string path = "discounts.xml")
        {
            StreamWriter file = File.CreateText(path);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Discount>));
            using (XmlTextWriter writer = new XmlTextWriter(file))
            {
                xmlSerializer.Serialize(writer, discounts);
            }
        }
        public static bool LoadDiscounts(string path = "discounts.xml")
        {
            StreamReader file;
            try
            {
                file = File.OpenText(path);
            }
            catch (FileNotFoundException)
            {
                discounts.Clear();
                return false;
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Discount>));
            using (XmlTextReader reader = new XmlTextReader(file))
            {
                try
                {
                    discounts = (List<Discount>)xmlSerializer.Deserialize(reader);
                }
                catch (InvalidCastException)
                {
                    discounts.Clear();
                    return false;
                }
                catch (Exception)
                {
                    discounts.Clear();
                    return false;
                }
            }
            return true;
        }

        public bool IsActive()
        {
            return DateTime.Now < deadlineDate;
        }
    }
}
