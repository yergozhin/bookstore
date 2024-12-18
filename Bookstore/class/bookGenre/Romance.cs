using System;

namespace Bookstore.@class
{
    public class Romance : Book
    {
        private string _relationshipsType;

        public string RelationshipsType
        {
            get => _relationshipsType;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Relationships type cannot be empty.");
                }

                _relationshipsType = value;
            }
        }

        public Romance(string title, float price, string languageOfPublication, string relationshipsType) : base(title,
            price, languageOfPublication)
        {
            RelationshipsType = relationshipsType;
        }
    }
}