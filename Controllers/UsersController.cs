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
    public class UsersController : ApiController
    {
        private EcommerceEntities db = new EcommerceEntities();

        // GET: api/Users
        public IQueryable<tbl_Users> Gettbl_Users()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tbl_Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(tbl_Users))]
        public IHttpActionResult Gettbl_Users(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbl_Users tbl_Users = db.tbl_Users.Find(id);
            if (tbl_Users == null)
            {
                return NotFound();
            }

            return Ok(tbl_Users);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Users(int id, tbl_Users tbl_Users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Users.UserID)
            {
                return BadRequest();
            }

            db.Entry(tbl_Users).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_UsersExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(tbl_Users))]
        public IHttpActionResult Posttbl_Users(tbl_Users tbl_Users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Users.Add(tbl_Users);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Users.UserID }, tbl_Users);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(tbl_Users))]
        public IHttpActionResult Deletetbl_Users(int id)
        {
            tbl_Users tbl_Users = db.tbl_Users.Find(id);
            if (tbl_Users == null)
            {
                return NotFound();
            }

            db.tbl_Users.Remove(tbl_Users);
            db.SaveChanges();

            return Ok(tbl_Users);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_UsersExists(int id)
        {
            return db.tbl_Users.Count(e => e.UserID == id) > 0;
        }
    }
}