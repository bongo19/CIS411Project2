using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RateGames.Models
{
    public class VoteDTO
    {
        public class Detail
        {
            public int VoteID { get; set; }
            public int Placement_Left { get; set; }
            public int Placement_Right { get; set; }
            public int GameID { get; set; }

        }

        public IEnumerable<Detail> Details { get; set; } 
    }
}