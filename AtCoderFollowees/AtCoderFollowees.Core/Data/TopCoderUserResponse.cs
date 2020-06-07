using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AtCoderFollowees.Core.Data
{
    public class TopCoderUserResponse
    {
        public string? Handle { get; set; }
        public string? Country { get; set; }
        public DateTime MemberSince { get; set; }
        public string? Quote { get; set; }
        public string? PhotoLink { get; set; }
        public bool Copilot { get; set; }
        public Ratingsummary[]? RatingSummary { get; set; }
        [JsonPropertyName("Achievements")]
        public Achievement[]? Achievements { get; set; }
        public Serverinformation? ServerInformation { get; set; }
        public Requesterinformation? RequesterInformation { get; set; }
    }

    public class Serverinformation
    {
        public string? ServerName { get; set; }
        public string? ApiVersion { get; set; }
        public int RequestDuration { get; set; }
        public long CurrentTime { get; set; }
    }

    public class Requesterinformation
    {
        public string? Id { get; set; }
        public string? RemoteIP { get; set; }
        public Receivedparams? ReceivedParams { get; set; }
    }

    public class Receivedparams
    {
        public string? ApiVersion { get; set; }
        public string? Handle { get; set; }
        public string? Action { get; set; }
    }

    public class Ratingsummary
    {
        public string? Name { get; set; }
        public int Rating { get; set; }
        public string? ColorStyle { get; set; }
    }

    public class Achievement
    {
        public DateTime Date { get; set; }
        public string? Description { get; set; }
    }
}
