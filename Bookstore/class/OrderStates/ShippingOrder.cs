using System;

namespace Bookstore.@class
{
    [Serializable]
    public class ShippingOrder : OrderBase
    {
        public override string Status => "Shipping";

        public ShippingOrder(OrderBase oldOrder)
            : base(oldOrder) { }

        public override OrderBase NextState(string action)
        {
            if (action == "Shipped")
            {
                return new DeliveringOrder(this);
            }
            return this;
        }
    }
}