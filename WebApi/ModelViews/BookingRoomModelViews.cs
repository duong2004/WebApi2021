using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.ModelViews
{
    public class BookingRoomModelViews
    {
        public System.Guid OrderId { get; set; }
        public string NumberRoom { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public System.Guid CustomerId { get; set; }
    }
}