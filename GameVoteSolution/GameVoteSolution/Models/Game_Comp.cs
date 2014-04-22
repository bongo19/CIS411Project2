using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace GameVoteSolution.Models
{
    public class Game_Comp
    {
        public int id { get; set; }
        public string Name { get; set;}
        public string Quote { get; set; }
        public string Pic { get; set; }
    }

    public class Game_CompDBContext : DbContext
    {
        public DbSet<Game> Game_CompDB { get; set;}

        public System.Data.Entity.DbSet<GameVoteSolution.Models.Game_Comp> Game_Comp { get; set; }
    }
}