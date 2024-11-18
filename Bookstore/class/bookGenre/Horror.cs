using System;

namespace Bookstore.@class
{
    public class Horror : Book
    {
        private int _levelOfScariness;
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

        public Horror(string title, float price, string languageOfPublication, int levelOfScariness) : base(title, price, languageOfPublication)
        {
            LevelOfScariness = levelOfScariness;

        }
    }
}
