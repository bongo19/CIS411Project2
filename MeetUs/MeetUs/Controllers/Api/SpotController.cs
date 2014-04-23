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

        ///<summary>
        /// Gets all Spots
        /// </summary>
        // GET api/Spot
        [HttpGet]//Specify it's a get
        public IEnumerable<Spot> Index()
        {
            return db.Spots.AsEnumerable();
        }

        ///<summary>
        /// Gets a spot
        /// </summary>
        /// <param name="id">
        /// The ID of the Spot
        /// </param>
        // GET api/Spot/Details/5
        [HttpGet]//Specify that it's a get
        public Spot Details(int id)
        {
            Spot spot = db.Spots.Find(id);
            if (spot == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return spot;
        }

        ///<summary>
        /// Edits spot
        /// </summary>
        // PUT api/Spot/Edit/5
        [Authorize(Roles = "Administrator")]
        [HttpPut]//Specify that it's a put
        public HttpResponseMessage Edit(int id, Spot spot)
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


        ///<summary>
        /// User Joins Spot
        /// </summary>
        // PUT api/Spot/Join/5
        [HttpPut]//Specify that it's a put
        public HttpResponseMessage Join(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            Spot spot = db.Spots.Find(id);
            if (spot == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            var userProfile = db.UserProfiles.Where(user => user.UserName == User.Identity.Name).Single();
            spot.UserProfiles.Add(userProfile);

            db.Entry(spot).State = EntityState.Modified;

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

        ///<summary>
        /// Create Spot
        /// </summary>
        // POST api/Spot/Create
        [HttpPost]//Specify it's a post
        public HttpResponseMessage Create(Spot spot)
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


        ///<summary>
        /// Delete Spot
        /// </summary>
        // DELETE api/Spot/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete]//Specify it's a delete
        public HttpResponseMessage Delete(int id)
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