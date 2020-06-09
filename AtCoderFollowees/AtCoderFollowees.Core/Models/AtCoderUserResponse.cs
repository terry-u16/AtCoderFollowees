using System;
using System.Collections.Generic;
using System.Text;

namespace AtCoderFollowees.Core.Models
{
    public class AtCoderUserResponse
    {
        public string UserID { get; }
        public int Rating { get; }
        public string? TopCoderID { get; }
        public string? CodeforcesID { get; }
        public string? TwitterID { get; }

        internal AtCoderUserResponse(string userID, int rating, string? topCoderID, string? codeforcesID, string? twitterID)
        {
            UserID = userID;
            Rating = rating;
            TopCoderID = topCoderID;
            CodeforcesID = codeforcesID;
            TwitterID = twitterID?.Replace("@", "");
        }

        public override string ToString() => UserID.ToString();
    }
}
