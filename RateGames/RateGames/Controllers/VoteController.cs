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
    public class VoteController : ApiController
    {
        private gamevoteprojectEntities4 db = new gamevoteprojectEntities4();

        // GET api/Vote
        public IEnumerable<Vote> GetVotes()
        {
            var votes = db.Votes.Include(v => v.Game);
            return votes.AsEnumerable();
        }

        // GET api/Vote/5
        //public VoteDTO GetVote(int id)
        //{
        //    Vote vote = db.Votes.Include("VoteDetails.Game")
        //        .First(v => v.VoteID == id);
        //    if (vote == null)
        //    {
        //        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
        //    }

        //    return new VoteDTO()
        //    {
        //        Details = from d in vote.Vote_Listing
        //                  select new VoteDTO.Detail()
        //                  {
        //                      GameID = d.
        //                  }
        //    }


        //}

        // PUT api/Vote/5
        public HttpResponseMessage PutVote(int id, Vote vote)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != vote.VoteID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(vote).State = EntityState.Modified;

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

        // POST api/Vote
        public HttpResponseMessage PostVote(Vote vt)
        {
            if (ModelState.IsValid)
            {

                var vote = new Vote()
                {
                    VoteID = vt.VoteID,
                    gameId = vt.gameId,
                    win = vt.win
                };

                db.Votes.Add(vote);
                db.SaveChanges();
            
                

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, vote);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = vote.VoteID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Vote/5
        public HttpResponseMessage DeleteVote(int id)
        {
            Vote vote = db.Votes.Find(id);
            if (vote == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Votes.Remove(vote);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, vote);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}