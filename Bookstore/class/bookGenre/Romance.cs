using System;
namespace Bookstore.@class
{
    public class Romance : Book
    {
        private string relationshipsType;
        public Romance(string title, float price, string relationshipsType) : base(title, price)
        {
            this.relationshipsType = relationshipsType;
        }
    }
}

