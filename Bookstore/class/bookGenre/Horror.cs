using System;

namespace Bookstore.@class
{
    public class Horror : Book
    {
        private string _type;
        public string Type
        {
            get => _type;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Type cannot be empty.");
                }
                _type = value;
            }
        }

        public Horror(string title, float price, string type) : base(title, price)
        {
            Type = type;
        }
    }
}
