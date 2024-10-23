using System;
namespace Bookstore.@class
{
	public class Customer : User
	{
		private string address;

		public Customer(string address, string name, string phoneNumber, string email, DateTime dateOfBirth)
			: base(name, phoneNumber, email, dateOfBirth)
		{
			this.address = address;
		}
	}
}

