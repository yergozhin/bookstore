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

        private int lengthMax = 50;
        private int lengthMin = 3;
        private int maxPrice = 10000;

        public Book(string title, float price)
        {
            if (string.IsNullOrEmpty(title) || title.Length < lengthMin || title.Length > lengthMax)
            {
                throw new ArgumentException($"Title must be between {lengthMin} and {lengthMax} characters.");
            }

            if (price < 0 || price > maxPrice)
            {
                throw new ArgumentException($"Price must be between 0 and {maxPrice}.");
            }

            this.title = title;
            this.price = price;
            addBook(this);
        }

        private static void addBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentException("Book cannot be null");
            }
            books.Add(book);
        }

        public string GetTitle()
        {
            return this.title;
        }

        public float GetPrice()
        {
            return this.price;
        }

        public void SetPrice(float price)
        {
            if (price < 0 || price > maxPrice)
            {
                throw new ArgumentException($"Price must be between 0 and {maxPrice}.");
            }
            this.price = price;
        }

        public bool CheckTitle()
        {
            return title.Length >= lengthMin && title.Length <= lengthMax;
        }

        public bool CheckPrice()
        {
            return price >= 0 && price <= maxPrice;
        }

        // Метод для сохранения списка книг в файл (по примеру из лекции)
        public static void SaveBooks(string path = "books.xml")
        {
            StreamWriter file = File.CreateText(path);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Book>));
            using (XmlTextWriter writer = new XmlTextWriter(file))
            {
                xmlSerializer.Serialize(writer, books);
            }
        }

        // Метод для загрузки списка книг из файла (по примеру из лекции)
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
        }
    }
}
