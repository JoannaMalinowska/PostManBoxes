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
    public class CheckPointsController : ApiController
    {
        private postman_pack_dbEntities db = new postman_pack_dbEntities();

        // GET: api/CheckPoints
        public IQueryable<check_points> Getcheck_points()
        {
            return db.check_points;
        }

        // GET: api/CheckPoints/5
        [ResponseType(typeof(check_points))]
        public async Task<IHttpActionResult> Getcheck_points(int id)
        {
            check_points check_points = await db.check_points.FindAsync(id);
            if (check_points == null)
            {
                return NotFound();
            }

            return Ok(check_points);
        }

        // PUT: api/CheckPoints/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putcheck_points(int id, check_points check_points)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (id != check_points.id)
            {
                return BadRequest();
            }

            db.Entry(check_points).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!check_pointsExists(id))
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

        // POST: api/CheckPoints
        [ResponseType(typeof(check_points))]
        public async Task<IHttpActionResult> Postcheck_points(check_points check_points)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.check_points.Add(check_points);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = check_points.id }, check_points);
        }

        // DELETE: api/CheckPoints/5
        [ResponseType(typeof(check_points))]
        public async Task<IHttpActionResult> Deletecheck_points(int id)
        {
            check_points check_points = await db.check_points.FindAsync(id);
            if (check_points == null)
            {
                return NotFound();
            }

            db.check_points.Remove(check_points);
            await db.SaveChangesAsync();

            return Ok(check_points);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool check_pointsExists(int id)
        {
            return db.check_points.Count(e => e.id == id) > 0;
        }
    }
}