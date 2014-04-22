using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RateGames.Models;

namespace RateGames.Controllers
{
    public class GameController : ApiController
    {
        private gamevoteprojectEntities4 db = new gamevoteprojectEntities4();

        //Project games to games Data Transfer objects.
        private IQueryable<GameDTO> MapGames()
        {
            return from g in db.Games
                select new GameDTO()
                {ID = g.gameID, Title = g.gameTitle, Picture = g.gamePic, Quote = g.gameQuote};
        }

        //Enumerable to get Games (maybe apply random function here)
        public IEnumerable<GameDTO> GetGames()
        {
            return MapGames().AsEnumerable();
        }

        public GameDTO GetGame(int id)
        {
            var game = (from g in MapGames()
                where g.ID == 1
                select g).FirstOrDefault();
            if (game == null)
            {
                throw new HttpRequestException(
                    Request.CreateResponse(HttpStatusCode.NotFound).ToString());
            }
            return game;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
