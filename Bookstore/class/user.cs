using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

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
            set => phoneNumber = value;
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
                email = value;
            }
        }

        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set => dateOfBirth = value;
        }

        public User(string name, string phoneNumber, string email, DateTime dateOfBirth)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            DateOfBirth = dateOfBirth;
            users.Add(this);
        }

        public static List<User> GetUsers()
        {
            return new List<User>(users);
        }
    }
}
