using System;
using System.Collections.Generic;

namespace Bookstore.@class
{
    [Serializable]
    public class OverlappingUser
    {
        private static List<OverlappingUser> allUsers = new List<OverlappingUser>();

        // ------------------
        // Overlapping Roles
        // ------------------
        private UserRole roles;
        public UserRole Roles => roles;
        
        private string name;
        private string phoneNumber;
        private string email;
        private DateTime dateOfBirth;
        
        private string address;
        private List<Discount> associatedDiscounts = new List<Discount>();
        
        private string position;
        private List<OrderBase> assignedOrders = new List<OrderBase>();
        
        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Name cannot be empty.");
                name = value;
            }
        }

        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Phone number cannot be empty.");
                phoneNumber = value;
            }
        }

        public string Email
        {
            get => email;
            set
            {
                if (string.IsNullOrEmpty(value) || !value.Contains("@"))
                    throw new ArgumentException("Invalid email address.");
                email = value;
            }
        }

        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                if (DateTime.Today < value)
                    throw new ArgumentException("Invalid date of birth.");
                dateOfBirth = value;
            }
        }
        
        public string Address
        {
            get
            {
                if (!roles.HasFlag(UserRole.Customer))
                    throw new InvalidOperationException("This user is not a Customer.");
                return address;
            }
            set
            {
                if (!roles.HasFlag(UserRole.Customer))
                    throw new InvalidOperationException("This user is not a Customer.");
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Address cannot be empty.");
                address = value;
            }
        }

        public IReadOnlyList<Discount> GetAssociatedDiscounts()
        {
            if (!roles.HasFlag(UserRole.Customer))
                throw new InvalidOperationException("Not a Customer role.");
            return associatedDiscounts.AsReadOnly();
        }
        
        public string Position
        {
            get
            {
                if (!roles.HasFlag(UserRole.Employee))
                    throw new InvalidOperationException("This user is not an Employee.");
                return position;
            }
            set
            {
                if (!roles.HasFlag(UserRole.Employee))
                    throw new InvalidOperationException("This user is not an Employee.");
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Position cannot be empty.");
                position = value;
            }
        }

        public IReadOnlyList<OrderBase> GetAssignedOrders()
        {
            if (!roles.HasFlag(UserRole.Employee))
                throw new InvalidOperationException("Not an Employee role.");
            return assignedOrders.AsReadOnly();
        }

       
        public OverlappingUser(
            string name,
            string phoneNumber,
            string email,
            DateTime dateOfBirth,
            UserRole initialRoles
        )
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            DateOfBirth = dateOfBirth;
            roles = initialRoles;
            
            allUsers.Add(this);
        }

        
        public void AddRole(UserRole newRole)
        {
            roles |= newRole;
        }
        public void RemoveRole(UserRole oldRole)
        {
            roles &= ~oldRole;
        }
        
        public void AddDiscount(Discount discount)
        {
            if (!roles.HasFlag(UserRole.Customer))
                throw new InvalidOperationException("Not a Customer role.");
            if (!associatedDiscounts.Contains(discount))
            {
                associatedDiscounts.Add(discount);
                discount.AddUserAsCustomer(this); // mutual linkage
            }
        }
        public void RemoveDiscount(Discount discount)
        {
            if (!roles.HasFlag(UserRole.Customer))
                throw new InvalidOperationException("Not a Customer role.");
            if (associatedDiscounts.Contains(discount))
            {
                associatedDiscounts.Remove(discount);
                discount.RemoveUserAsCustomer(this);
            }
        }
        
        public void AssignOrder(OrderBase order)
        {
            if (!roles.HasFlag(UserRole.Employee))
                throw new InvalidOperationException("Not an Employee role.");
            if (!assignedOrders.Contains(order))
            {
                assignedOrders.Add(order);
                order.AssignUserAsEmployee(this); // mutual linkage
            }
        }
        public void RemoveAssignedOrder(OrderBase order)
        {
            if (!roles.HasFlag(UserRole.Employee))
                throw new InvalidOperationException("Not an Employee role.");
            if (assignedOrders.Contains(order))
            {
                assignedOrders.Remove(order);
                order.RemoveUserAsEmployee(this);
            }
        }
        
        public static void ClearUsers()
        {
            allUsers.Clear();
        }
        public static List<OverlappingUser> GetUsers()
        {
            return new List<OverlappingUser>(allUsers);
        }
    }
}
