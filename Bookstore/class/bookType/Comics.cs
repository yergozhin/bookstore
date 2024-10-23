using System;
namespace Bookstore.@class
{
	public class Comics : Book
	{
		private int issueNumber;
		public Comics(string title, float price, int issueNumber) : base(title,price)
		{
			this.issueNumber = issueNumber;
		}
	}
}

