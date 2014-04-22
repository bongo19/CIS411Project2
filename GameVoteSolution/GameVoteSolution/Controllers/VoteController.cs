using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;
using GameVoteSolution.Models;

namespace GameVoteSolution.Controllers
{
    public class VoteController : ApiController
    {
        private gamevoteprojectEntities1 db = new gamevoteprojectEntities1();

        private IQueryable<GameDTO> MapGame()
        {
            return from g in db.Games
                select new GameDTO()
                {Id = g.gameID, Title = g.gameTitle, Picture = g.gamePic, Quote = g.gameQuote};
        }

        public IEnumerable<GameDTO> GetGames()
        {
            return MapGame().AsEnumerable();
        }

        public GameDTO GetGame(int id)
        {
            var game = (from g in MapGame()
                where g.Id == 1
                select g).FirstOrDefault();
            if (game == null)
            {
                throw new HttpResponseException(
                    Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return game;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public HttpResponseMessage PostVote(GameDTO dto)
        {
            if (ModelState.IsValid)
            {
                var gamevoted = new Vote
                {
                    gameId = 1,
                    Placement_Left = 1,
                    Placement_Right = 1,
                    Win_Lose = 1
                };

                db.Votes.Add(gamevoted);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, gamevoted);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new {id = gamevoted.VoteID}));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


    }
}
