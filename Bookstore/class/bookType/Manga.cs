using System;

namespace Bookstore.@class
{
    public class Manga : Book
    {
        private int _volume;
        public int Volume
        {
            get => _volume;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Volume must be greater than 0.");
                }
                _volume = value;
            }
        }

        public Manga(string title, float price, int volume) : base(title, price)
        {
            Volume = volume;
        }
    }
}
