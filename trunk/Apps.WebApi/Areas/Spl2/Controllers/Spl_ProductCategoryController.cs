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
using Apps.Models;

namespace Apps.WebApi.Areas.Spl2.Controllers
{
    public class Spl_ProductCategoryController : ApiController
    {
        private DBContainer db = new DBContainer();

        // GET: api/Spl_ProductCategory
        public IQueryable<Spl_ProductCategory> GetSpl_ProductCategory()
        {
            return db.Spl_ProductCategory;
        }

        // GET: api/Spl_ProductCategory/5
        [ResponseType(typeof(Spl_ProductCategory))]
        public IHttpActionResult GetSpl_ProductCategory(string id)
        {
            Spl_ProductCategory spl_ProductCategory = db.Spl_ProductCategory.Find(id);
            if (spl_ProductCategory == null)
            {
                return NotFound();
            }

            return Ok(spl_ProductCategory);
        }

        // PUT: api/Spl_ProductCategory/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSpl_ProductCategory(string id, Spl_ProductCategory spl_ProductCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != spl_ProductCategory.Id)
            {
                return BadRequest();
            }

            db.Entry(spl_ProductCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Spl_ProductCategoryExists(id))
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

        // POST: api/Spl_ProductCategory
        [ResponseType(typeof(Spl_ProductCategory))]
        public IHttpActionResult PostSpl_ProductCategory(Spl_ProductCategory spl_ProductCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Spl_ProductCategory.Add(spl_ProductCategory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Spl_ProductCategoryExists(spl_ProductCategory.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = spl_ProductCategory.Id }, spl_ProductCategory);
        }

        // DELETE: api/Spl_ProductCategory/5
        [ResponseType(typeof(Spl_ProductCategory))]
        public IHttpActionResult DeleteSpl_ProductCategory(string id)
        {
            Spl_ProductCategory spl_ProductCategory = db.Spl_ProductCategory.Find(id);
            if (spl_ProductCategory == null)
            {
                return NotFound();
            }

            db.Spl_ProductCategory.Remove(spl_ProductCategory);
            db.SaveChanges();

            return Ok(spl_ProductCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Spl_ProductCategoryExists(string id)
        {
            return db.Spl_ProductCategory.Count(e => e.Id == id) > 0;
        }
    }
}