using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Bookstore.@class
{
    [Serializable]
    public class Review
    {
        private static List<Review> reviews = new List<Review>();

        private int rating;
        private string comment;
        private DateTime reviewDate;

        public int Rating
        {
            get => rating;
            set
            {
                if (value < 1 || value > 5)
                {
                    throw new ArgumentException("Rating must be between 1 and 5.");
                }
                rating = value;
            }
        }

        public string Comment
        {
            get => comment;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Comment cannot be empty.");
                }
                comment = value;
            }
        }

        public DateTime ReviewDate
        {
            get => reviewDate;
            set
            {
                if (DateTime.Today < value)
                {
                    throw new ArgumentException("Invalid review date.");
                }
                reviewDate = value;
            }
        }

        public Review(int rating, string comment, DateTime reviewDate)
        {
            Rating = rating;
            Comment = comment;
            ReviewDate = reviewDate;
            reviews.Add(this);
        }

        public static List<Review> GetReviews()
        {
            return reviews;
        }
    }
}
