using System;
namespace Bookstore.@class
{
	public class Author
	{
		private string firstName;
		private string lastName;
		private string bio;
		public Author(string firstName, string lastName, string bio = "")
		{
			this.firstName = firstName;
			this.lastName = lastName;
			this.bio = bio;
		}
	}
}

