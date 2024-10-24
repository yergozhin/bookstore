using System;
namespace Bookstore.@class
{
	public class Publisher
	{
		private string name;
		private string address;
		private string email;
		private string phoneNumber;
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
		public Publisher(string name, string address, string email, string phoneNumber = "")
		{
			this.name = name;
			this.address = address;
			this.phoneNumber = phoneNumber;
			this.email = email;
		}
<<<<<<< Updated upstream
	}
=======

		//check @ in an email 
        public bool checkEmail()
        {
            for (int i = 1; i < email.Length - 1; i++)
                if (email[i].Equals('@'))
                    return true;
            return false;
        }
    }
>>>>>>> Stashed changes
}

