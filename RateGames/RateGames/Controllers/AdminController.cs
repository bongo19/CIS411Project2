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
using RateGames.Models;

namespace RateGames.Controllers
{
    public class AdminController : ApiController
    {
        private gamevoteprojectEntities4 db = new gamevoteprojectEntities4();

        // GET api/Admin
        public IEnumerable<Game> GetGames()
        {
            return db.Games.AsEnumerable();
        }

        // GET api/Admin/5
        public Game GetGame(int id)
        {
            Game game = db.Games.Find(id);
            if (game == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return game;
        }

        // PUT api/Admin/5
        public HttpResponseMessage PutGame(int id, Game game)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != game.gameID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(game).State = EntityState.Modified;

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

        // POST api/Admin
        public HttpResponseMessage PostGame(Game game)
        {
            if (ModelState.IsValid)
            {
                db.Games.Add(game);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, game);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = game.gameID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Admin/5
        public HttpResponseMessage DeleteGame(int id)
        {
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Games.Remove(game);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, game);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}