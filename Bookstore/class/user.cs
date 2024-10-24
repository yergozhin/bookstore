using System;
namespace Bookstore.@class
{
	public class User
	{
		private string name;
		private string phoneNumber;
		private string email;
		private DateTime dateOfBirth;
<<<<<<< Updated upstream
		public User(string name, string phoneNumber, string email, DateTime dateOfBirth)
=======

        public User(string name, string phoneNumber, string email, DateTime dateOfBirth)
>>>>>>> Stashed changes
		{
			this.name = name;
			this.phoneNumber = phoneNumber;
			this.email = email;
			this.dateOfBirth = dateOfBirth;
		}
<<<<<<< Updated upstream
=======

		public string getName()
		{
			return name;
		}

		public string getPhoneNumber()
		{
			return phoneNumber;
		}

		public string getEmail()
		{
			return email;
		}

		public DateTime getDateOfBirth()
		{
			return dateOfBirth;
		}

		public string setPhoneNumber(string phoneNumber)
		{
			this.phoneNumber = phoneNumber;
		}

		public string setEmail (string email)
		{
			this.email = email;
		}

		public bool checkEmail()
		{
			for (int i = 1; i < email.Length - 1; i++)
				if (email[i].Equals('@'))
					return true;
			return false;
		}

		public bool checkAge()
		{
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - this.dateOfBirth.Year;

            if (this.dateOfBirth > currentDate.AddYears(-age))
                age--;
			bool check = age > 8;

            return check;
        }
>>>>>>> Stashed changes
	}
}

