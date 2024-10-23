using System;
namespace Bookstore.@class
{
	public class User
	{
		private string name;
		private string phoneNumber;
		private string email;
		private DateTime dateOfBirth;
		public User(string name, string phoneNumber, string email, DateTime dateOfBirth)
		{
			this.name = name;
			this.phoneNumber = phoneNumber;
			this.email = email;
			this.dateOfBirth = dateOfBirth;
		}
	}
}

