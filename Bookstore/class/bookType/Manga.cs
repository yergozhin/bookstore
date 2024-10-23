using System;
namespace Bookstore.@class
{
    public class Manga : Book
    {
        private int volume;
        public Manga(string title, float price, int volume) : base(title, price)
        {
            this.volume = volume;
        }
    }
}

