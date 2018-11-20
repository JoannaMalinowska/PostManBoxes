using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using PostManBoxes.Data;
using PostManBoxes.Helpers;

namespace PostManBoxes.Controllers
{
    public class DestinationsController : ApiController
    {
        private postman_pack_dbEntities db = new postman_pack_dbEntities();

        // GET: api/Destinations
        public IQueryable<destinations> Getdestinations()
        {
            Logger.CreateLog(HttpContext.Current.Request.Url.PathAndQuery, HttpMethod.Get.Method, null);
            return db.destinations;
        }

        // GET: api/Destinations/5
        [ResponseType(typeof(destinations))]
        public async Task<IHttpActionResult> Getdestinations(long id)
        {
            destinations destinations = await db.destinations.FindAsync(id);
            if (destinations == null)
            {
                return NotFound();
            }
            Logger.CreateLog(HttpContext.Current.Request.Url.PathAndQuery, HttpMethod.Get.Method, null);
            return Ok(destinations);
        }

        // PUT: api/Destinations/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putdestinations(long id, destinations destinations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != destinations.id)
            {
                return BadRequest();
            }

            db.Entry(destinations).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!destinationsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Logger.CreateLog(HttpContext.Current.Request.Url.PathAndQuery, HttpMethod.Put.Method, null);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Destinations
        [ResponseType(typeof(destinations))]
        public async Task<IHttpActionResult> Postdestinations(destinations destinations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.destinations.Add(destinations);
            await db.SaveChangesAsync();
            Logger.CreateLog(HttpContext.Current.Request.Url.PathAndQuery, HttpMethod.Post.Method, null);
            return CreatedAtRoute("DefaultApi", new { id = destinations.id }, destinations);
        }

        // DELETE: api/Destinations/5
        [ResponseType(typeof(destinations))]
        public async Task<IHttpActionResult> Deletedestinations(long id)
        {
            destinations destinations = await db.destinations.FindAsync(id);
            if (destinations == null)
            {
                return NotFound();
            }

            db.destinations.Remove(destinations);
            await db.SaveChangesAsync();
            Logger.CreateLog(HttpContext.Current.Request.Url.PathAndQuery, HttpMethod.Delete.Method, null);
            return Ok(destinations);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool destinationsExists(long id)
        {
            return db.destinations.Count(e => e.id == id) > 0;
        }
    }
}