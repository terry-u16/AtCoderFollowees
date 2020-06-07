using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AtCoderFollowees.Core.Models
{
    public class User
    {
        public int ID { get; set; }
        [MaxLength(16)]
        public string AtCoderID { get; set; }
        [MaxLength(15)]
        public string? TopCoderID { get; set; }
        [MaxLength(24)]
        public string? CodeforcesID { get; set; }
        [MaxLength(15)]
        public string? TwitterScreenName { get; set; }

        public long? TwitterID { get; set; }

        public int AtCoderRating { get; set; }
        public int? TopCoderRating { get; set; }
        public int? CodeforcesRating { get; set; }

        public User(string atCoderID, int atCoderRating)
        {
            AtCoderID = atCoderID;
            AtCoderRating = atCoderRating;
        }

        public override string ToString() => $"{AtCoderID} Rating:{AtCoderRating}";
    }
}
