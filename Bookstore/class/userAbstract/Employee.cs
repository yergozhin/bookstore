using System;
namespace Bookstore.@class
{
	public class Employee : User
	{
		private string position;
<<<<<<< Updated upstream
		public Employee(string position, string name, string phoneNumber, string email, DateTime dateOfBirth)
=======
		public Employee(string name, string phoneNumber, string email, DateTime dateOfBirth, string position)
>>>>>>> Stashed changes
			: base(name,phoneNumber,email,dateOfBirth)
		{
			this.position = position;
		}
	}
}

