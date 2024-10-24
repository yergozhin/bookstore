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

        public Publisher(string name, string address, string email, string phoneNumber = "")
        {
            this.name = name;
            this.address = address;
            this.phoneNumber = phoneNumber;
            this.email = email;
            addPublisher(this);
        }

        private static void addPublisher(Publisher publisher)
        {
            if (publisher == null)
            {
                throw new ArgumentException("Publisher cannot be null");
            }
            publishers.Add(publisher);
        }

        public static List<Publisher> GetPublishers()
        {
            return new List<Publisher>(publishers);
        }
        public static void SavePublishers(string path = "publishers.xml")
        {
            StreamWriter file = File.CreateText(path);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Publisher>));
            using (XmlTextWriter writer = new XmlTextWriter(file))
            {
                xmlSerializer.Serialize(writer, publishers);
            }
        }
        public static bool LoadPublishers(string path = "publishers.xml")
        {
            StreamReader file;
            try
            {
                file = File.OpenText(path);
            }
            catch (FileNotFoundException)
            {
                publishers.Clear();
                return false;
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Publisher>));
            using (XmlTextReader reader = new XmlTextReader(file))
            {
                try
                {
                    publishers = (List<Publisher>)xmlSerializer.Deserialize(reader);
                }
                catch (InvalidCastException)
                {
                    publishers.Clear();
                    return false;
                }
                catch (Exception)
                {
                    publishers.Clear();
                    return false;
                }
            }
            return true;
        }
        public bool CheckEmail()
        {
            return email.Contains('@');
        }
    }
}
