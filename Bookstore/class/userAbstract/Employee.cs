using System;

namespace Bookstore.@class
{
    public class Employee : User
    {
        private string _position;
        public string Position
        {
            get => _position;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Position cannot be empty.");
                }
                _position = value;
            }
        }

        public Employee(string name, string phoneNumber, string email, DateTime dateOfBirth, string position)
            : base(name, phoneNumber, email, dateOfBirth)
        {
            Position = position;
        }
    }
}
