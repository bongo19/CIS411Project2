using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetUs.Models
{
    public class Spot
    {
        public int SpotId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }

        //Adds collection of users to spot. (This will link tables together)
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }

    //Deals with create api vulnerability
    public class SpotCreateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }


    public class SpotEditRequest
    {
        public int SpotId { get; set; }
        public string Description { get; set; }

    }

    public class SpotDetailsResponse
    {
        public  Spot Spot { get; set; }
        public string FbLikeButtonData { get; set; }

    }


}