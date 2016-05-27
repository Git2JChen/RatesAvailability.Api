using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using RateAvail.Api.Model;

namespace RateAvail.Api.Response
{
    [DataContract]
    public class RatesResponse
    {
        [DataMember]
        [JsonProperty("availabilities", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Availability> Availabilities { get; set; }
    }
}