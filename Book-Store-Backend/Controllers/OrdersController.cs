using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Book_Store_Backend.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Book_Store_Backend.Controllers
{
    [RoutePrefix("api/Orders")]
    public class OrdersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> UserManager { get; set; }

        public OrdersController()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }
        // GET: api/Orders
        public List<Order> GetOrders([FromUri] string userId = "")
        {
            IQueryable<Order> orders = db.Orders;
            if (userId != "")
            {
                orders = orders.Where(x => x.UserId == userId);
            }
            return orders.ToList();
        }

        // GET: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [ResponseType(typeof(string))]
        [Route("{id}")]
        [HttpPatch]
        public IHttpActionResult patchOrder(int id,[FromBody] OrderChangeModel orderChange)
        {
            Order order = db.Orders.Find(id);
            if (orderChange.status != null)
            {
                if (order.status > orderChange.status)
                    return BadRequest("cannot change status to lesser than current");
                order.status = orderChange.status??order.status;
                if (order.status == OrderStatus.PLACED)
                    order.setTotalPrice();
            }
            if(orderChange.address != null)
            {
                order.address = orderChange.address;
            }
            if(orderChange.couponId != null)
            {
                if (orderChange.couponId == 0)
                    order.CouponId = null;
                else
                    order.CouponId = orderChange.couponId;
            }
            db.SaveChanges();
            return Ok();
        }
        //// PUT: api/Orders/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutOrder(int id, Order order)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != order.OrderId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(order).State = System.Data.Entity.EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!OrderExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/Orders
        [Authorize]
        [ResponseType(typeof(Order))]
        public IHttpActionResult PostOrder()
        {
            Order order = new Order();
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            try
            {
                order = (from x in db.Orders where x.UserId == user.Id && x.status == OrderStatus.IN_PROGRESS select x).First();
                return Ok(order);
            }
            catch (InvalidOperationException)
            {
                ;
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            order.UserId = user.Id;
            order.User = user;
            order.BookEntries.Clear();
            db.Orders.Add(order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = order.OrderId }, order);
        }

        // DELETE: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
            db.SaveChanges();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.OrderId == id) > 0;
        }
    }
}