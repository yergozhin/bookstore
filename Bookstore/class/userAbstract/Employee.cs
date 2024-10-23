using System;
namespace Bookstore.@class
{
	public class Employee : User
	{
		private string position;
		public Employee(string position, string name, string phoneNumber, string email, DateTime dateOfBirth)
			: base(name,phoneNumber,email,dateOfBirth)
		{
			this.position = position;
		}
	}
}

