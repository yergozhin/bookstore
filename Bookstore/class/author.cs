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
            get => firstName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("First name cannot be empty.");
                }
                firstName = value;
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Last name cannot be empty.");
                }
                lastName = value;
            }
        }

        public string Bio
        {
            get => bio;
            set { bio = value; }
        }

        public Author(string firstName, string lastName, string bio = null)
        {
            FirstName = firstName;
            LastName = lastName;
            Bio = bio; 
            authors.Add(this);
        }

        public static void ClearAuthors()
        {
            authors.Clear();
        }
        public static List<Author> GetAuthors()
        {
            return new List<Author>(authors);
        }
        public static void Add(Author author)
        {
            authors.Add(author);
        }
    }
}