using System;
using System.Collections.Generic;

namespace Bookstore.@class
{
    [Serializable]
    public class User
    {
        private static List<User> users = new List<User>();

        private string name;
        private string phoneNumber;
        private string email;
        private DateTime dateOfBirth;

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Name cannot be empty.");
                }

                name = value;
            }
        }

        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Phone number cannot be empty.");
                }

                phoneNumber = value;
            }
        }

        public string Email
        {
            get => email;
            set
            {
                if (string.IsNullOrEmpty(value) || !value.Contains("@"))
                {
                    throw new ArgumentException("Invalid email address.");
                }

                if (users.Exists(u => u.email == value && u != this))
                {
                    throw new InvalidOperationException("Email must be unique.");
                }

                email = value;
            }
        }

        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                if (DateTime.Today < value)
                {
                    throw new ArgumentException("Invalid date of birth.");
                }

                dateOfBirth = value;
            }
        }

        public User(string name, string phoneNumber, string email, DateTime dateOfBirth)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            DateOfBirth = dateOfBirth;
            users.Add(this);
        }

        public static void ClearUsers()
        {
            users.Clear();
        }

        public static List<User> GetUsers()
        {
            return new List<User>(users);
        }

        public static User FindUser(string searchTerm)
        {
            return users.Find(user =>
                user.Name.Equals(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                user.Email.Equals(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                user.PhoneNumber.Equals(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        public void UpdateUser(string newName = null, string newPhoneNumber = null, string newEmail = null,
            DateTime? newDateOfBirth = null)
        {
            if (!string.IsNullOrEmpty(newName))
            {
                Name = newName;
            }

            if (!string.IsNullOrEmpty(newPhoneNumber))
            {
                PhoneNumber = newPhoneNumber;
            }

            if (!string.IsNullOrEmpty(newEmail))
            {
                Email = newEmail;
            }

            if (newDateOfBirth.HasValue)
            {
                DateOfBirth = newDateOfBirth.Value;
            }
        }
    }
}