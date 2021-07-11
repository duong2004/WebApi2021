using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;
using WebApi.ModelViews;

namespace WebApi.Controllers
{
    public class BookingsController : ApiController
    {
        static readonly HotelOneEntities db = new HotelOneEntities();
        [HttpGet]
        public HttpResponseMessage Get(System.Guid Id)
        {
            BookingRoomModelViews booking = null;
            booking = db.BookingRooms
                .Where(x => x.OrderId == Id)
                .Select(b => new BookingRoomModelViews()
                {
                    OrderId = Id,
                    NumberRoom = b.NumberRoom,
                    Quantity = b.Quantity,
                    Price = b.Price,
                    Amount = b.Amount,
                    CustomerId = b.CustomerId
                }).FirstOrDefault<BookingRoomModelViews>();
            if (booking == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, booking);
        }
        [HttpPost]
        public HttpResponseMessage Post(BookingRoomModelViews booking)
        {
            db.BookingRooms.Add(new BookingRoom()
            {
                OrderId = System.Guid.NewGuid(),
                NumberRoom = booking.NumberRoom,
                Quantity = booking.Quantity,
                Price = booking.Price,
                Amount = booking.Amount,
                CustomerId = booking.CustomerId
            });
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Add is a success!");
        }
        [HttpPut]
        public HttpResponseMessage Put(BookingRoomModelViews booking)
        {
            BookingRoom bookingExit = db.BookingRooms.Where(x => x.OrderId == booking.OrderId).FirstOrDefault();
            if (bookingExit != null)
            {
                bookingExit.CustomerId = booking.CustomerId;
                bookingExit.NumberRoom = booking.NumberRoom;
                bookingExit.Quantity = booking.Quantity;
                bookingExit.Price = booking.Price;
                bookingExit.Amount = booking.Amount;
                db.SaveChanges();
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Edit is success!");
        }
        [HttpDelete]
        public HttpResponseMessage Delete(System.Guid Id)
        {
            BookingRoom booking = db.BookingRooms.FirstOrDefault(x => x.OrderId == Id);
            if (booking != null)
            {
                db.BookingRooms.Remove(booking);
                db.SaveChanges();
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Deleted is a success!");
        }
    }
}
