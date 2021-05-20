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
using Ecommerce.Models;

namespace Ecommerce.Controllers
{
    public class OrdersController : ApiController
    {
        private EcommerceEntities db = new EcommerceEntities();

        // GET: api/Orders
        public IQueryable<tbl_Orders> Gettbl_Orders()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tbl_Orders;
        }

        // GET: api/Orders/5
        [ResponseType(typeof(tbl_Orders))]
        public IHttpActionResult Gettbl_Orders(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbl_Orders tbl_Orders = db.tbl_Orders.Find(id);
            if (tbl_Orders == null)
            {
                return NotFound();
            }

            return Ok(tbl_Orders);
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Orders(int id, tbl_Orders tbl_Orders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Orders.OrderID)
            {
                return BadRequest();
            }

            db.Entry(tbl_Orders).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_OrdersExists(id))
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

        // POST: api/Orders
        [ResponseType(typeof(tbl_Orders))]
        public IHttpActionResult Posttbl_Orders(tbl_Orders tbl_Orders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Orders.Add(tbl_Orders);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Orders.OrderID }, tbl_Orders);
        }

        // DELETE: api/Orders/5
        [ResponseType(typeof(tbl_Orders))]
        public IHttpActionResult Deletetbl_Orders(int id)
        {
            tbl_Orders tbl_Orders = db.tbl_Orders.Find(id);
            if (tbl_Orders == null)
            {
                return NotFound();
            }

            db.tbl_Orders.Remove(tbl_Orders);
            db.SaveChanges();

            return Ok(tbl_Orders);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_OrdersExists(int id)
        {
            return db.tbl_Orders.Count(e => e.OrderID == id) > 0;
        }
    }
}