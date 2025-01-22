using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Bookstore.@class
{
    [Serializable]
    [XmlRoot("Bookstore")]
    public class BookstoreContainer
    {
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Author> Authors { get; set; } = new List<Author>();
        public List<Discount> Discounts { get; set; } = new List<Discount>();
        
        // Storing the abstract orders as a single list is tricky for XML serialization:
        // For simplicity,we store them as "CreatedOrder" . with [XmlElement(...)?].
        [XmlArray("Orders")]
        [XmlArrayItem("CreatedOrder", typeof(CreatedOrder))]
        [XmlArrayItem("PendingPaymentOrder", typeof(PendingPaymentOrder))]
        [XmlArrayItem("PaymentCancelledOrder", typeof(PaymentCancelledOrder))]
        [XmlArrayItem("ShippingOrder", typeof(ShippingOrder))]
        [XmlArrayItem("DeliveringOrder", typeof(DeliveringOrder))]
        [XmlArrayItem("CompletedOrder", typeof(CompletedOrder))]
        public List<OrderBase> Orders { get; set; } = new List<OrderBase>();

        public List<Publisher> Publishers { get; set; } = new List<Publisher>();
        public List<Review> Reviews { get; set; } = new List<Review>();

        // Overlapping user
        public List<OverlappingUser> Users { get; set; } = new List<OverlappingUser>();
    }
}

