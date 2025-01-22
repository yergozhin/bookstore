using System;

namespace Bookstore.@class
{
    [Serializable]
    public class CompletedOrder : OrderBase
    {
        public override string Status => "Completed";

        public CompletedOrder(OrderBase oldOrder)
            : base(oldOrder) { }

        public override OrderBase NextState(string action)
        {
            // Completed is final; no transitions
            return this;
        }
    }
}