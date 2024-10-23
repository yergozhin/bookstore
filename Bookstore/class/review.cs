using System;
namespace Bookstore.@class
{
	public class Review
	{
		private int rating;
		private string comment;
		private DateTime reviewDate;
		public Review(int rating, string comment, DateTime reviewDate)
		{
			this.rating = rating;
			this.comment = comment;
			this.reviewDate = reviewDate;
		}
	}
}

