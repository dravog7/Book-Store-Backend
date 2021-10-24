using System;
using System.Collections.Generic;
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
    [RoutePrefix("api/WishList")]
    public class WishListController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> UserManager { get; set; }

        public WishListController()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        //GET: api/WishList
        public List<WishList> GetWishLists([FromUri] string userId = "")
        {
            IQueryable<WishList> wishList = db.WishList;
            if(userId != "")
            {
                wishList = wishList.Where(x => x.UserId == userId);
            }
            return wishList.ToList();
        }
        // GET: api/WishList/5
        [ResponseType(typeof(WishList))]
        public IHttpActionResult GetWishList(int id)
        {
            WishList wishList = db.WishList.Find(id);
            if (wishList == null)
            {
                return NotFound();
            }

            return Ok(wishList);
        }

        [Route("{id}/Add")]
        [HttpPatch]
        public IHttpActionResult AddBook(int id,[FromBody] WishListChangeModel bookChange)
        {
            WishList wishList = db.WishList.Find(id);
            if (wishList == null)
                return BadRequest("Wishlist does not exist :"+id.ToString());
            Book book = db.Books.Find(bookChange.BookId);
            if (book == null)
                return BadRequest("Book does not exist :"+ bookChange.BookId.ToString());
            wishList.Books.Add(book);
            db.SaveChanges();
            return Ok();
        }

        [Route("{id}/Remove")]
        [HttpPatch]
        public IHttpActionResult RemoveBook(int id,[FromBody] WishListChangeModel bookChange)
        {
            WishList wishList = db.WishList.Find(id);
            if (wishList == null)
                return BadRequest("Wishlist does not exist :" + id.ToString());
            Book book = db.Books.Find(bookChange.BookId);
            if (book == null)
                return BadRequest("Book does not exist :" + bookChange.BookId.ToString());
            wishList.Books.Remove(book);
            db.SaveChanges();
            return Ok();
        }

        // PUT: api/WishList/5
        // TODO - allow only some fields to be edited
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWishList(int id, WishList wishList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != wishList.WishListId)
            {
                return BadRequest();
            }

            db.Entry(wishList).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WishListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/WishList
        [Authorize]
        [ResponseType(typeof(WishList))]
        public IHttpActionResult PostWishList(WishList wishList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            wishList.UserId = user.Id;
            wishList.User = user;
            wishList.Books.Clear();
            db.WishList.Add(wishList);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = wishList.WishListId }, wishList);
        }

        // DELETE: api/WishList/5
        [ResponseType(typeof(WishList))]
        public IHttpActionResult DeleteWishList(int id)
        {
            WishList wishList = db.WishList.Find(id);
            if (wishList == null)
            {
                return NotFound();
            }

            db.WishList.Remove(wishList);
            db.SaveChanges();

            return Ok(wishList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WishListExists(int id)
        {
            return db.WishList.Count(e => e.WishListId == id) > 0;
        }
    }
}
