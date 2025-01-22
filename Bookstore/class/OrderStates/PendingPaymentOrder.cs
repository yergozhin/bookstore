using System;

namespace Bookstore.@class
{
    [Serializable]
    public class PendingPaymentOrder : OrderBase
    {
        public override string Status => "PendingPayment";
        
        public PendingPaymentOrder(DateTime orderDate)
            : base(orderDate) { }
        
        public PendingPaymentOrder(OrderBase oldOrder)
            : base(oldOrder) { }

        public override OrderBase NextState(string action)
        {
            if (action == "PaymentSuccessful")
            {
                return new ShippingOrder(this);
            }
            else if (action == "PaymentFailed")
            {
                return new PaymentCancelledOrder(this);
            }
            return this;
        }
    }
}