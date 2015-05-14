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
using CompresJSON;

namespace CompresJSON.Controllers
{
    [EncryptAndCompressAsNecessaryWebApi]
    [DecryptAndDecompressAsNecessaryWebApi]
    public class CardDesignItemsController : ApiController
    {
        private AlexDbEntities db = AlexDbEntities.JsonDB();

        // GET: api/CardDesignItems
        public IHttpActionResult GetCardDesignItems()
        {
            var items = new List<CardDesignItem>()
            {

            };

            for (int i = 0; i < 5; i++)
            {
                items.Add(new CardDesignItem
                {
                    ItemText = "hello"
                });
            }

            //return Json(items);

            return Json(db.CardDesignItems.Take(5));
        }

        // GET: api/CardDesignItems/5
        [ResponseType(typeof(CardDesignItem))]
        public IHttpActionResult GetCardDesignItem(int id)
        {
            //return Json(new CardDesignItem
                //{
                  //  ItemText = "hello"
                //});

            CardDesignItem cardDesignItem = db.CardDesignItems.Find(id);
            if (cardDesignItem == null)
            {
                return NotFound();
            }

            return Json(cardDesignItem);
            //return Ok(cardDesignItem);
        }

        // PUT: api/CardDesignItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCardDesignItem(int id, CardDesignItem cardDesignItem)
        {
            //return Json(cardDesignItem);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cardDesignItem.CardDesignItemID)
            {
                return BadRequest();
            }

            db.Entry(cardDesignItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardDesignItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Json(cardDesignItem);
            //return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CardDesignItems
        [ResponseType(typeof(CardDesignItem))]
        public IHttpActionResult PostCardDesignItem(CardDesignItem cardDesignItem)
        {
            //return Json(cardDesignItem);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CardDesignItems.Add(cardDesignItem);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                string error = ex.Message;
                if (CardDesignItemExists(cardDesignItem.CardDesignItemID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Json(cardDesignItem);
            //return CreatedAtRoute("DefaultApi", new { id = cardDesignItem.CardDesignItemID }, cardDesignItem);
        }

        // DELETE: api/CardDesignItems/5
        [ResponseType(typeof(CardDesignItem))]
        public IHttpActionResult DeleteCardDesignItem(int id)
        {
            CardDesignItem cardDesignItem = db.CardDesignItems.Find(id);
            if (cardDesignItem == null)
            {
                return NotFound();
            }

            db.CardDesignItems.Remove(cardDesignItem);
            db.SaveChanges();

            return Ok(cardDesignItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CardDesignItemExists(int id)
        {
            return db.CardDesignItems.Count(e => e.CardDesignItemID == id) > 0;
        }
    }
}