using System;

namespace Bookstore.@class
{
    public class Customer : User
    {
        private List<Discount> associatedDiscounts = new List<Discount>();
        public IReadOnlyList<Discount> getAssociatedDiscounts() => associatedDiscounts.AsReadOnly();
        
        private List<Review> associatedReviews = new List<Review>();
        public IReadOnlyList<Review> getAssociatedReviews() => associatedReviews.AsReadOnly();
        
        private List<Order> associatedOrders = new List<Order>();
        public IReadOnlyList<Order> GetAssociatedOrders() => associatedOrders.AsReadOnly();


        private string _address;
        public string Address
        {
            get => _address;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Address cannot be empty.");
                }
                _address = value;
            }
        }

        public Customer(string name, string phoneNumber, string email, DateTime dateOfBirth, string address)
            : base(name, phoneNumber, email, dateOfBirth)
        {
            Address = address;
        }

        public void addDiscount(Discount discount)
        {
            if (!associatedDiscounts.Contains(discount))
            {
                associatedDiscounts.Add(discount);
                discount.addCustomer(this);
            }
        }
        public void removeDiscount(Discount discount)
        {
            if (associatedDiscounts.Contains(discount))
            {
                associatedDiscounts.Remove(discount);
                discount.removeCustomer(this);
            }
        }
        public void removeAllDiscounts()
        {
            foreach (Discount discount in associatedDiscounts)
            {
                removeDiscount(discount);
            }
        }
        
        public void addReview(Review review)
        {
            if (!associatedReviews.Contains(review))
            {
                associatedReviews.Add(review);
                review.assignCustomer(this);
            }
        }
        public void removeReview(Review review)
        {
            if (associatedReviews.Contains(review))
            {
                associatedReviews.Remove(review);
                review.removeFromCustomer(this);
            }
        }
        public void removeAllReviews()
        {
            foreach (Review review in associatedReviews)
            {
                removeReview(review);
            }
        }
        
        public void addOrder(Order order)
        {
            if (!associatedOrders.Contains(order))
            {
                associatedOrders.Add(order);
                order.assignCustomer(this);
            }
        }
        public void removeOrder(Order order)
        {
            if (associatedOrders.Contains(order))
            {
                associatedOrders.Remove(order);
                order.removeFromCustomer(this);
            }
        }
        public void removeAllOrders()
        {
            foreach (Order order in associatedOrders)
            {
                removeOrder(order);
            }
        }
    }
}
