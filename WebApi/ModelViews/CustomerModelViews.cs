using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.ModelViews
{
    public class CustomerModelViews
    {
        public System.Guid CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public System.DateTime OrderDate { get; set; }
        public List<BookingRooModelViews> Bookings { get; set; }
    }
    public class BookingRooModelViews
    {
        public System.Guid OrderId { get; set; }
        public string NumberRoom { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Nullable<decimal> Amount { get; set; }
    }
}