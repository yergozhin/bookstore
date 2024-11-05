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

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Name cannot be empty.");
                }
                _name = value;
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrEmpty(value) || !value.Contains("@"))
                {
                    throw new ArgumentException("Invalid email address.");
                }
                _email = value;
            }
        }

        private DateTime _dateOfBirth;
        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set => _dateOfBirth = value;
        }

        public User(string name, string phoneNumber, string email, DateTime dateOfBirth)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            DateOfBirth = dateOfBirth;
            addUser(this);
        }
        private static void addUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null");
            }
            users.Add(user);
        }

        public static List<User> GetUsers()
        {
            return new List<User>(users);
        }
        /*public static void SaveUsers(string path = "users.xml")
        {
            StreamWriter file = File.CreateText(path);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<User>));
            using (XmlTextWriter writer = new XmlTextWriter(file))
            {
                xmlSerializer.Serialize(writer, users);
            }
        }
        public static bool LoadUsers(string path = "users.xml")
        {
            StreamReader file;
            try
            {
                file = File.OpenText(path);
            }
            catch (FileNotFoundException)
            {
                users.Clear();
                return false;
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<User>));
            using (XmlTextReader reader = new XmlTextReader(file))
            {
                try
                {
                    users = (List<User>)xmlSerializer.Deserialize(reader);
                }
                catch (InvalidCastException)
                {
                    users.Clear();
                    return false;
                }
                catch (Exception)
                {
                    users.Clear();
                    return false;
                }
            }
            return true;
        }*/

        public bool CheckAge()
        {
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - this.DateOfBirth.Year;

            if (this.DateOfBirth > currentDate.AddYears(-age))
                age--;
            return age > 8;
        }
    }
}
