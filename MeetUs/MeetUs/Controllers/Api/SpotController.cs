using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MeetUs.Models;
using MeetUs.Context;

namespace MeetUs.Controllers.Api
{
    public class SpotController : ApiController
    {
        private DataContext db = new DataContext();

        // GET api/Spot
        public IEnumerable<Spot> GetSpots()
        {
            return db.Spots.AsEnumerable();
        }

        // GET api/Spot/5
        public Spot GetSpot(int id)
        {
            Spot spot = db.Spots.Find(id);
            if (spot == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return spot;
        }

        // PUT api/Spot/5
        public HttpResponseMessage PutSpot(int id, Spot spot)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != spot.SpotId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(spot).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Spot
        public HttpResponseMessage PostSpot(Spot spot)
        {
            if (ModelState.IsValid)
            {
                db.Spots.Add(spot);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, spot);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = spot.SpotId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Spot/5
        public HttpResponseMessage DeleteSpot(int id)
        {
            Spot spot = db.Spots.Find(id);
            if (spot == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Spots.Remove(spot);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, spot);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}