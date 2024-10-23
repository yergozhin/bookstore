using System;
namespace Bookstore.@class
{
    public class Horror : Book
    {
        private string type;
        public Horror(string title, float price, string type) : base(title, price)
        {
            this.type = type;
        }
    }
}

