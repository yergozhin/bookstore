using System;
using System.Collections.Generic;

namespace Bookstore.@class
{
    [Serializable]
    public class Discount
    {
        private static List<Discount> discounts = new List<Discount>();

        private List<OverlappingUser> associatedUsers = new List<OverlappingUser>();
        public IReadOnlyList<OverlappingUser> GetAssociatedUsers() => associatedUsers.AsReadOnly();

        private string type;
        private float amountInPercentage;
        private DateTime deadlineDate;

        public string Type
        {
            get => type;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Type cannot be empty.");
                if(value != "Single Use" && value != "Multiple Use")
                    throw new ArgumentException("Type must be 'Single Use' or 'Multiple Use'.");

                type = value;
            }
        }

        public float AmountInPercentage
        {
            get => amountInPercentage;
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException("Amount in percentage must be between 0 and 100.");
                amountInPercentage = value;
            }
        }

        public DateTime DeadlineDate
        {
            get => deadlineDate;
            set
            {
                if (DateTime.Today > value)
                    throw new ArgumentException("Invalid date of deadline.");
                deadlineDate = value;
            }
        }

        public Discount(string type, float amountInPercentage, DateTime deadlineDate)
        {
            Type = type;
            AmountInPercentage = amountInPercentage;
            DeadlineDate = deadlineDate;
            discounts.Add(this);
        }

        // Methods for static list
        public static void ClearDiscounts()
        {
            discounts.Clear();
        }
        public static List<Discount> GetDiscounts()
        {
            return new List<Discount>(discounts);
        }

        // -----------------------------------
        // Replaces "addCustomer" with checking:
        // user must have Customer role
        // -----------------------------------
        public void AddUserAsCustomer(OverlappingUser user)
        {
            if (!user.Roles.HasFlag(UserRole.Customer))
                throw new InvalidOperationException("User does not have Customer role.");

            if (!associatedUsers.Contains(user))
            {
                associatedUsers.Add(user);
                // user.AddDiscount(this); 
                // Typically we rely on the user to call user.AddDiscount(this)
                // to avoid circular calls. 
                // But if we want mutual linkage always, we could call it here.
            }
        }

        public void RemoveUserAsCustomer(OverlappingUser user)
        {
            if (associatedUsers.Contains(user))
            {
                associatedUsers.Remove(user);
                // user.RemoveDiscount(this);
            }
        }
    }
}
