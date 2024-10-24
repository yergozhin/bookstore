using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Bookstore.@class
{
    [Serializable]
    public class Author
    {
        private static List<Author> authors = new List<Author>();
        private string firstName;
        private string lastName;
        private string bio;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string Bio
        {
            get { return bio; }
            set { bio = value; }
        }

        public Author(string firstName, string lastName, string bio = "")
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.bio = bio;
            addAuthor(this);
        }
        private static void addAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentException("Author cannot be null");
            }
            authors.Add(author);
        }

        public static List<Author> GetAuthors()
        {
            return new List<Author>(authors);
        }

        public static void SaveAuthors(string path = "authors.xml")
        {
            StreamWriter file = File.CreateText(path);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Author>));
            using (XmlTextWriter writer = new XmlTextWriter(file))
            {
                xmlSerializer.Serialize(writer, authors);
            }
        }
        public static bool LoadAuthors(string path = "authors.xml")
        {
            StreamReader file;
            try
            {
                file = File.OpenText(path);
            }
            catch (FileNotFoundException)
            {
                authors.Clear();
                return false;
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Author>));
            using (XmlTextReader reader = new XmlTextReader(file))
            {
                try
                {
                    authors = (List<Author>)xmlSerializer.Deserialize(reader);
                }
                catch (InvalidCastException)
                {
                    authors.Clear();
                    return false;
                }
                catch (Exception)
                {
                    authors.Clear();
                    return false;
                }
            }
            return true;
        }
    }
}
