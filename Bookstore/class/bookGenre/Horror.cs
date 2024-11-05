using System;

namespace Bookstore.@class
{
    public class Horror : Book
    {
        private string _type;
        private int _levelOfScariness;
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
        public int LevelOfScariness
        {
            get => _levelOfScariness;
            set
            {
                if (value < 1 || value > 10)
                {
                    throw new ArgumentException("Value must be in range between 1 and 10");
                }
                _levelOfScariness = value;
            }
        }

        public Horror(string title, float price, string languageOfPublication, string type, int levelOfScariness) : base(title, price, languageOfPublication)
        {
            Type = type;
            LevelOfScariness = levelOfScariness;

        }
    }
}
