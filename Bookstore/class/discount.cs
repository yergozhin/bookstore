using System;
namespace Bookstore.@class
{
	public class Discount
	{
		private string type;
		private float amountInPercentage;
		private DateTime deadlineDate;
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
		public Discount(string type, float amountInPercentage, DateTime deadlineDate)
		{
			this.type = type;
			this.amountInPercentage = amountInPercentage;
			this.deadlineDate = deadlineDate;
		}
<<<<<<< Updated upstream
=======

		//check if deadline is passed
		public bool isActive()
		{
			DateTime currentDate = DateTime.Now;
			bool active = currentDate < deadlineDate;
			return active;
		}
>>>>>>> Stashed changes
	}
}

