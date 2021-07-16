using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ModelViews;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class PagesController : ApiController
    {
        static readonly HotelOneEntities db = new HotelOneEntities();
        [HttpGet]
        public HttpResponseMessage GetPageCustomer(int? pageNo, int? pageSize)
        {
            Pager pager = null;
            var totalCustomer = db.Customers.ToList().Count();
            pager = new Pager(totalCustomer, pageNo.Value, pageSize.Value);
            return Request.CreateResponse(HttpStatusCode.OK, pager);
        }
    }
}
