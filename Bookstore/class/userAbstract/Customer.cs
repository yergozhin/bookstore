using System;
namespace Bookstore.@class
{
	public class Customer : User
	{
		private string address;

<<<<<<< Updated upstream
		public Customer(string address, string name, string phoneNumber, string email, DateTime dateOfBirth)
=======
		public Customer(string name, string phoneNumber, string email, DateTime dateOfBirth, string address)
>>>>>>> Stashed changes
			: base(name, phoneNumber, email, dateOfBirth)
		{
			this.address = address;
		}
	}
}

