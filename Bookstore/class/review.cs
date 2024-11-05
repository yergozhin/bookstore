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

        private int _rating;
        public int Rating
        {
            get => _rating;
            set
            {
                if (value < 1 || value > 5)
                {
                    throw new ArgumentException("Rating must be between 1 and 5.");
                }
                _rating = value;
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Comment cannot be empty.");
                }
                _comment = value;
            }
        }

        private DateTime _reviewDate;
        public DateTime ReviewDate
        {
            get => _reviewDate;
            set => _reviewDate = value;
        }

        public Review(int rating, string comment, DateTime reviewDate)
        {
            Rating = rating;
            Comment = comment;
            ReviewDate = reviewDate;
            addReview(this);
        }
        private static void addReview(Review review)
        {
            if (review == null)
            {
                throw new ArgumentException("Review cannot be null");
            }
            reviews.Add(review);
        }

        public static List<Review> GetReviews()
        {
            return new List<Review>(reviews);
        }
        /*public static void SaveReviews(string path = "reviews.xml")
        {
            StreamWriter file = File.CreateText(path);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Review>));
            using (XmlTextWriter writer = new XmlTextWriter(file))
            {
                xmlSerializer.Serialize(writer, reviews);
            }
        }
        public static bool LoadReviews(string path = "reviews.xml")
        {
            StreamReader file;
            try
            {
                file = File.OpenText(path);
            }
            catch (FileNotFoundException)
            {
                reviews.Clear();
                return false;
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Review>));
            using (XmlTextReader reader = new XmlTextReader(file))
            {
                try
                {
                    reviews = (List<Review>)xmlSerializer.Deserialize(reader);
                }
                catch (InvalidCastException)
                {
                    reviews.Clear();
                    return false;
                }
                catch (Exception)
                {
                    reviews.Clear();
                    return false;
                }
            }
            return true;
        }*/
    }
}
