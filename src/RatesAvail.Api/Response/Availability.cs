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

        [DataMember]
        [JsonProperty("mon", NullValueHandling = NullValueHandling.Ignore)]
        public bool Mon { get; set; }

        [DataMember]
        [JsonProperty("tue", NullValueHandling = NullValueHandling.Ignore)]
        public bool Tue { get; set; }

        [DataMember]
        [JsonProperty("wed", NullValueHandling = NullValueHandling.Ignore)]
        public bool Wed { get; set; }

        [DataMember]
        [JsonProperty("thu", NullValueHandling = NullValueHandling.Ignore)]
        public bool Thu { get; set; }

        [DataMember]
        [JsonProperty("fri", NullValueHandling = NullValueHandling.Ignore)]
        public bool Fri { get; set; }

        [DataMember]
        [JsonProperty("sat", NullValueHandling = NullValueHandling.Ignore)]
        public bool Sat { get; set; }

        [DataMember]
        [JsonProperty("sun", NullValueHandling = NullValueHandling.Ignore)]
        public bool Sun { get; set; }
    }
}