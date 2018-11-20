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
using PostManBoxes.Helpers;
using PostManBoxes.Models;

namespace PostManBoxes.Controllers
{
    public class BoxesController : ApiController
    {
        private postman_pack_dbEntities db = new postman_pack_dbEntities();

        // GET: api/Boxes
        public IQueryable<boxes> Getboxes()
        {
            return db.boxes;
        }

        // GET: api/Boxes/5
        [ResponseType(typeof(boxes))]
        public async Task<IHttpActionResult> Getboxes(long id)
        {
            boxes boxes = await db.boxes.FindAsync(id);

            var ip = IP.GetIPAddress();

            if (boxes == null)
            {
                return NotFound();
            }

            return Ok(boxes);
        }

        // GET: api/Boxes/abc
        [ResponseType(typeof(boxes))]
        public async Task<IHttpActionResult> GetBoxUsingKey(string key)
        {
            boxes boxes = await db.boxes.Where(x => x.pack_number == key).FirstOrDefaultAsync();
            if (boxes == null)
            {
                return NotFound();
            }

            return Ok(boxes);
        }

        
        [ResponseType(typeof(TransportInformation))]
        public async Task<IHttpActionResult> GetTransportInformation(string key)
        {
            TransportInformation transportInformation = await db.boxes.Where(x => x.pack_number == key).Select(x=> new TransportInformation() {
                    Name= x.check_points.name,
                    Latitude = x.check_points.latitude,
                    Longitude = x.check_points.longitude,
                    Delivery_status = x.delivery_status
            }).FirstOrDefaultAsync();



            if (transportInformation == null)
            {
                return NotFound();
            }

            return Ok(transportInformation);
        }

        // PUT: api/Boxes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putboxes(long id, boxes boxes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != boxes.id)
            {
                return BadRequest();
            }

            db.Entry(boxes).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!boxesExists(id))
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

        // POST: api/Boxes
        [ResponseType(typeof(OrderModel))]
        [Route("api/Boxes/Postboxes")]
        public async Task<IHttpActionResult> Postboxes(OrderModel order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Database.BeginTransaction();

           
            
            var destination = new destinations()
            {
                country = order.Country,
                city = order.City,
                post_code = order.Post_code,
                street = order.Street,
                house_number = order.House_number,
                apartment_number = order.Apartment_number,
                recipient_name = order.Recipient_name,
                recipient_surname = order.Recipient_surname
            };

            db.destinations.Add(destination);
            var addDestinationResult = await db.SaveChangesAsync();

            if (addDestinationResult <= 0)
            {
                db.Database.CurrentTransaction.Rollback();
                return BadRequest();
            }

            var pack = new boxes()
            {
                customer_key = order.CustomerKey,
                pack_number = order.PackNumber,
                dimensions = order.Dimensions,
                weight = order.Weight,
                to_pay = order.ToPay,
                delivery_status = 1,
                destination_id = destination.id,
                current_check_point = order.CheckPointID
            };

            db.boxes.Add(pack);
            var addPackResult = await db.SaveChangesAsync();

            if (addPackResult <= 0)
            {
                db.Database.CurrentTransaction.Rollback();
                return BadRequest();
            }

            var courier = new couriers()
            {
                courier_id = 1,
                pack_id = pack.id,
                is_available = true
            };

            db.couriers.Add(courier);
            var addCourierRangeResult = await db.SaveChangesAsync();

            if (addCourierRangeResult <= 0)
            {
                db.Database.CurrentTransaction.Rollback();
                return BadRequest();
            }

            db.Database.CurrentTransaction.Commit();

            return Ok();
        }

        // DELETE: api/Boxes/5
        [ResponseType(typeof(boxes))]
        public async Task<IHttpActionResult> Deleteboxes(long id)
        {
            boxes boxes = await db.boxes.FindAsync(id);

            if(boxes == null)
            {
                return NotFound();
            }

            try
            {
                db.boxes.Remove(boxes);
                await db.SaveChangesAsync();
            }
            catch ( Exception ex)
            {

              
                return BadRequest();
            }

            
            return Ok(boxes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool boxesExists(long id)
        {
            return db.boxes.Count(e => e.id == id) > 0;
        }
    }
}