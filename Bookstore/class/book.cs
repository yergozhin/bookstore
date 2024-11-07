using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Bookstore.@class
{
    [Serializable]
    public class Book
    {
        private static List<Book> books = new List<Book>();

        private string title;
        private float price;
        private string languageOfPublication;
        private List<String> listOfLanguagesToWhichTranslated;

        private int lengthMax = 50;
        private int lengthMin = 3;
        private int maxPrice = 10000;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public float Price
        {
            get { return price; }
            set { price = value; }
        }

        public string LanguageOfPublication
        {
            get { return languageOfPublication; }
            set { languageOfPublication = value; }
        }

        public List<string> ListOfLanguagesToWhichTranslated
        {
            get { return listOfLanguagesToWhichTranslated; }
            set { listOfLanguagesToWhichTranslated = value ?? new List<string>(); }
        }

        public Book(string title, float price, string languageOfPublication, List<String> listOfLanguagesToWhichTranslated = null, string addLanguage = "")
        {
            if (string.IsNullOrEmpty(title) || title.Length < lengthMin || title.Length > lengthMax)
            {
                throw new ArgumentException($"Title must be between {lengthMin} and {lengthMax} characters.");
            }

            if (price < 0 || price > maxPrice)
            {
                throw new ArgumentException($"Price must be between 0 and {maxPrice}.");
            }
            this.languageOfPublication = languageOfPublication;
            this.title = title;
            this.price = price;
            Book tempBook = findBook(title);
            if (tempBook == null)
            {
                this.listOfLanguagesToWhichTranslated = new List<string>();
            }
            else
            {
                this.listOfLanguagesToWhichTranslated = tempBook.listOfLanguagesToWhichTranslated;
                if (addLanguage.Length >= 1)
                {
                    this.listOfLanguagesToWhichTranslated.Add(addLanguage);
                }
                deleteBook(title);
            }
            addBook(this);
        }
        private static void deleteBook(string title)
        {
            foreach (var book in Book.getBooks())
            {
                if (book.title == title)
                {
                    Book.getBooks().Remove(book);
                }
            }
        }
        private static Book findBook(String title)
        {
            foreach (var book in Book.getBooks())
            {
                if (book.title == title)
                {
                    return book;
                }
            }
            return null;
        }
        private static void addBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentException("Book cannot be null");
            }
            books.Add(book);
        }

        public static List<Book> getBooks()
        {
            return new List<Book>(books);
        }
        /*public static void SaveBooks(string path = "books.xml")
        {
            StreamWriter file = File.CreateText(path);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Book>));
            using (XmlTextWriter writer = new XmlTextWriter(file))
            {
                xmlSerializer.Serialize(writer, books);
            }
        }

        public static bool LoadBooks(string path = "books.xml")
        {
            StreamReader file;
            try
            {
                file = File.OpenText(path);
            }
            catch (FileNotFoundException)
            {
                books.Clear();
                return false;
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Book>));
            using (XmlTextReader reader = new XmlTextReader(file))
            {
                try
                {
                    books = (List<Book>)xmlSerializer.Deserialize(reader);
                }
                catch (InvalidCastException)
                {
                    books.Clear();
                    return false;
                }
                catch (Exception)
                {
                    books.Clear();
                    return false;
                }
            }
            return true;
        }*/
    }
}