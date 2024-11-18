using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Bookstore.@class
{
    [Serializable]
    public class Publisher
    {
        private static List<Publisher> publishers = new List<Publisher>();

        private string name;
        private string address;
        private string email;
        private string phoneNumber;

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Name cannot be empty.");
                name = value;
            }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (string.IsNullOrEmpty(value) || !value.Contains('@'))
                    throw new ArgumentException("Invalid email address.");
                email = value;
            }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public Publisher(string name, string address, string email, string phoneNumber = null)
        {
            Name = name;
            Address = address;
            Email = email;
            PhoneNumber = phoneNumber;
            publishers.Add(this);
        }

        public static List<Publisher> GetPublishers()
        {
            return publishers;
        }
    }
}
