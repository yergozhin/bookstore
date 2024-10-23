using System;
namespace Bookstore.@class
{
	public class Order
	{
		private DateTime orderDate;
		private string status;
		public Order(DateTime orderDate, string status)
		{
			this.orderDate = orderDate;
			this.status = status;
		}
	}
}

