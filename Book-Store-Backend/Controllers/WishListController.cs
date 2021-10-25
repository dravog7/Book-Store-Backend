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

        ////GET: api/WishList
        //public List<WishList> GetWishLists([FromUri] string userId = "")
        //{
        //    IQueryable<WishList> wishList = db.WishList;
        //    if(userId != "")
        //    {
        //        wishList = wishList.Where(x => x.WishListId == userId);
        //    }
        //    return wishList.ToList();
        //}
        // GET: api/WishList/
        [Authorize]
        [ResponseType(typeof(WishList))]
        public IHttpActionResult GetWishList()
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            WishList wishList = db.WishList.Find(user.Id);
            if (wishList == null)
            {
                return NotFound();
            }

            return Ok(wishList);
        }
        [Authorize]
        [Route("Add")]
        [HttpPatch]
        public IHttpActionResult AddBook([FromBody] WishListChangeModel bookChange)
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            WishList wishList = GetOrCreateWishList(user);
            if (wishList == null)
                return BadRequest("Wishlist does not exist :"+user.Id);
            Book book = db.Books.Find(bookChange.BookId);
            if (book == null)
                return BadRequest("Book does not exist :"+ bookChange.BookId.ToString());
            wishList.Books.Add(book);
            db.SaveChanges();
            return Ok();
        }
        [Authorize]
        [Route("Remove")]
        [HttpPatch]
        public IHttpActionResult RemoveBook([FromBody] WishListChangeModel bookChange)
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            WishList wishList = GetOrCreateWishList(user);
            if (wishList == null)
                return BadRequest("Wishlist does not exist :" + user.Id);
            Book book = db.Books.Find(bookChange.BookId);
            if (book == null)
                return BadRequest("Book does not exist :" + bookChange.BookId.ToString());
            wishList.Books.Remove(book);
            db.SaveChanges();
            return Ok();
        }

        //// PUT: api/WishList/5
        //// TODO - allow only some fields to be edited
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutWishList(string id, WishList wishList)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != wishList.WishListId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(wishList).State = System.Data.Entity.EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!WishListExists(id))
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

        //// POST: api/WishList
        //[Authorize]
        //[ResponseType(typeof(WishList))]
        //public IHttpActionResult PostWishList(WishList wishList)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
        //    wishList.WishListId = user.Id;
        //    wishList.User = user;
        //    wishList.Books.Clear();
        //    db.WishList.Add(wishList);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = wishList.WishListId }, wishList);
        //}

        //// DELETE: api/WishList/5
        //[ResponseType(typeof(WishList))]
        //public IHttpActionResult DeleteWishList(int id)
        //{
        //    WishList wishList = db.WishList.Find(id);
        //    if (wishList == null)
        //    {
        //        return NotFound();
        //    }

        //    db.WishList.Remove(wishList);
        //    db.SaveChanges();

        //    return Ok(wishList);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WishListExists(string id)
        {
            return db.WishList.Count(e => e.WishListId == id) > 0;
        }

        private WishList GetOrCreateWishList(ApplicationUser user)
        {
            if (!WishListExists(user.Id))
            {
                WishList wishList = new WishList { WishListId = user.Id };
                db.WishList.Add(wishList);
                return wishList;
            }
            return db.WishList.Find(user.Id);
        }
    }
}
