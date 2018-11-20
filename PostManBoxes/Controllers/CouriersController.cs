using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PostManBoxes.Data;

namespace PostManBoxes.Controllers
{
    public class CouriersController : ApiController
    {
        private postman_pack_dbEntities db = new postman_pack_dbEntities();

        // GET: api/Couriers
        public IQueryable<couriers> Getcouriers()
        {
            return db.couriers;
        }

        // GET: api/Couriers/5
        [ResponseType(typeof(couriers))]
        public async Task<IHttpActionResult> Getcouriers(long id)
        {
            couriers couriers = await db.couriers.FindAsync(id);
            if (couriers == null)
            {
                return NotFound();
            }

            return Ok(couriers);
        }

        // PUT: api/Couriers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putcouriers(long id, couriers couriers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != couriers.id)
            {
                return BadRequest();
            }

            db.Entry(couriers).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!couriersExists(id))
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

        // POST: api/Couriers
        [ResponseType(typeof(couriers))]
        public async Task<IHttpActionResult> Postcouriers(couriers couriers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.couriers.Add(couriers);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = couriers.id }, couriers);
        }

        // DELETE: api/Couriers/5
        [ResponseType(typeof(couriers))]
        public async Task<IHttpActionResult> Deletecouriers(long id)
        {
            couriers couriers = await db.couriers.FindAsync(id);
            if (couriers == null)
            {
                return NotFound();
            }

            db.couriers.Remove(couriers);
            await db.SaveChangesAsync();

            return Ok(couriers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool couriersExists(long id)
        {
            return db.couriers.Count(e => e.id == id) > 0;
        }
    }
}