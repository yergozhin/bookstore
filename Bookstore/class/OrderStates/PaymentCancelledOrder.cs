using System;

namespace Bookstore.@class
{
    [Serializable]
    public class PaymentCancelledOrder : OrderBase
    {
        public override string Status => "PaymentCancelled";

        public PaymentCancelledOrder(OrderBase oldOrder)
            : base(oldOrder) { }

        public override OrderBase NextState(string action)
        {
          
            return this;
        }
    }
}