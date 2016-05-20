using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace RateAvail.Api.Response
{
    [DataContract]
    public class Availability
    {
        [DataMember]
        [JsonProperty("startDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime StartDate { get; set; }

        [DataMember]
        [JsonProperty("endDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime EndDate { get; set; }
    }
}