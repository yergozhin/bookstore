using System;

namespace Bookstore.@class
{
    [Serializable]
    public class DeliveringOrder : OrderBase
    {
        public override string Status => "Delivering";

        public DeliveringOrder(OrderBase oldOrder)
            : base(oldOrder) { }

        public override OrderBase NextState(string action)
        {
            if (action == "Delivered")
            {
                return new CompletedOrder(this);
            }
            return this;
        }
    }
}