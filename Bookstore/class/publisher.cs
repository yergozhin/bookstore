using System;
namespace Bookstore.@class
{
	public class Publisher
	{
		private string name;
		private string address;
		private string email;
		private string phoneNumber;
		public Publisher(string name, string address, string email, string phoneNumber = "")
		{
			this.name = name;
			this.address = address;
			this.phoneNumber = phoneNumber;
			this.email = email;
		}
	}
}

