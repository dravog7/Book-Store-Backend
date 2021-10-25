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

namespace Book_Store_Backend.Controllers
{
    [RoutePrefix("api/Cart")]
    public class BookEntriesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Cart
        public IQueryable<BookEntry> GetBookEntries([FromUri] int? OrderId = null)
        {
            IQueryable<BookEntry> bookEntries = db.BookEntries;
            if(OrderId != null)
            {
                bookEntries = bookEntries.Where(x => x.OrderId == OrderId);
            }
            return bookEntries;
        }

        // GET: api/Cart/5
        [ResponseType(typeof(BookEntry))]
        public IHttpActionResult GetBookEntry(int id)
        {
            BookEntry bookEntry = db.BookEntries.Find(id);
            if (bookEntry == null)
            {
                return NotFound();
            }

            return Ok(bookEntry);
        }

        // PUT: api/Cart/5/1
        [ResponseType(typeof(void))]
        [Route("{OrderId}/{BookId}")]
        [HttpPut]
        public IHttpActionResult PutBookEntry(int OrderId,int BookId, BookEntry bookEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (BookId != bookEntry.BookId || OrderId != bookEntry.OrderId)
            {
                return BadRequest();
            }

            db.Entry(bookEntry).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookEntryExists(BookId,OrderId))
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

        // POST: api/Cart
        [ResponseType(typeof(BookEntry))]
        public IHttpActionResult PostBookEntry(BookEntry bookEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BookEntries.Add(bookEntry);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BookEntryExists(bookEntry.BookId,bookEntry.OrderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = bookEntry.BookId }, bookEntry);
        }

        // DELETE: api/Cart/5/1
        [ResponseType(typeof(BookEntry))]
        [Route("{OrderId}/{BookId}")]
        [HttpDelete]
        public IHttpActionResult DeleteBookEntry(int OrderId, int BookId)
        {
            BookEntry bookEntry = db.BookEntries.Find(BookId,OrderId);
            if (bookEntry == null)
            {
                return NotFound();
            }

            db.BookEntries.Remove(bookEntry);
            db.SaveChanges();

            return Ok(bookEntry);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookEntryExists(int BookId,int OrderId)
        {
            return db.BookEntries.Count(e => (e.BookId == BookId) && (e.OrderId == OrderId)) > 0;
        }
    }
}