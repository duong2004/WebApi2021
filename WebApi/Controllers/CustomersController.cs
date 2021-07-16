using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;
using WebApi.ModelViews;
using System.Net;
using System.Data.Entity;

namespace WebApi.Controllers
{
    public class CustomersController : ApiController
    {
        static readonly HotelOneEntities db = new HotelOneEntities();
        [HttpGet]
        public HttpResponseMessage Get()
        {
            IList<CustomerModelViews> customers = null;
            customers = db.Customers
                .OrderByDescending(x => x.CustomerId)
                .Include("BookingRoom")
                .Select(c => new CustomerModelViews()
                {
                    CustomerId = c.CustomerId,
                    Name = c.Name,
                    Address = c.Address,
                    OrderDate = c.OrderDate,
                    Bookings = db.BookingRooms.Where(x => x.CustomerId == c.CustomerId).Select(b => new BookingRooModelViews()
                    {
                        OrderId = b.OrderId,
                        NumberRoom = b.NumberRoom,
                        Quantity = b.Quantity,
                        Price = b.Price,
                        Amount = b.Amount
                    }).ToList<BookingRooModelViews>()
                }).ToList<CustomerModelViews>();
            return Request.CreateResponse(HttpStatusCode.OK, customers);
        }
        [HttpGet]
        public HttpResponseMessage Get(System.Guid Id)
        {

            CustomerModelViews customers = null;
            customers = db.Customers.Include("BookingRoom")
                .Where(x => x.CustomerId == Id)
                .Select(c => new CustomerModelViews()
                {
                    CustomerId = c.CustomerId,
                    Name = c.Name,
                    Address = c.Address,
                    OrderDate = c.OrderDate,
                    Bookings = db.BookingRooms.Where(x => x.CustomerId == c.CustomerId).Select(b => new BookingRooModelViews()
                    {
                        OrderId = b.OrderId,
                        NumberRoom = b.NumberRoom,
                        Quantity = b.Quantity,
                        Price = b.Price,
                        Amount = b.Amount
                    }).ToList<BookingRooModelViews>()
                }).FirstOrDefault<CustomerModelViews>();
            return Request.CreateResponse(HttpStatusCode.OK, customers);
        }
        [HttpPost]
        public HttpResponseMessage Create(CustomerModelViews customer)
        {
            db.Customers.Add(new Customer()
            {
                CustomerId = System.Guid.NewGuid(),
                Name = customer.Name,
                Address = customer.Address,
                OrderDate = DateTime.Now
            });
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Add is a success");
        }
        [HttpPut]
        public HttpResponseMessage Put(CustomerModelViews customer)
        {
            var customerExit = db.Customers.Where(x => x.CustomerId == customer.CustomerId).FirstOrDefault();
            if (customerExit != null)
            {
                customerExit.Name = customer.Name;
                customerExit.Address = customer.Address;
                customerExit.OrderDate = DateTime.Now;
                db.SaveChanges();
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Edit is a success");
        }
        [HttpDelete]
        public HttpResponseMessage Delete(System.Guid Id)
        {
            var customer = db.Customers.Where(x => x.CustomerId == Id).FirstOrDefault();
            if (customer == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }            
            if (customer.BookingRooms.Count > 0)
            {
                List<BookingRoom> bookings = db.BookingRooms.Where(x => x.CustomerId == customer.CustomerId).ToList();
                foreach (var booking in bookings)
                {
                    db.BookingRooms.Remove(booking);
                    db.SaveChanges();
                }
            }
            db.Customers.Remove(customer);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Delete is a success");
        }
    }
}
