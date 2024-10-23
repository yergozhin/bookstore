using System;
namespace Bookstore.@class
{
	public class Discount
	{
		private string type;
		private float amountInPercentage;
		private DateTime deadlineDate;
		public Discount(string type, float amountInPercentage, DateTime deadlineDate)
		{
			this.type = type;
			this.amountInPercentage = amountInPercentage;
			this.deadlineDate = deadlineDate;
		}
	}
}

