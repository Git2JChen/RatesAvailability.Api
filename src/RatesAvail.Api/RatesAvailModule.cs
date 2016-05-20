using System;
using System.Collections.Generic;
using Nancy;
using RateAvail.Api.Response;

namespace RateAvail.Api
{
    public class RatesAvailModule : NancyModule
    {
        public RatesAvailModule()
        {
            Get["/"] = parameters =>
            {
                return "Nancy.HttpStatusCode is OK";
            };

            Get["/RatesAvail"] = with =>
            {
                return BuildResponse();
            };
        }

        private Nancy.Response BuildResponse()
        {
            var sDate = Convert.ToDateTime(Request.Query["sDate"].Value);
            var eDate = Convert.ToDateTime(Request.Query["eDate"].Value);
            var rateAvailabilities = new List<Availability>
                {
                    new Availability
                    {
                        StartDate = sDate,
                        EndDate = eDate,
                    },
                };

            var response = new RatesResponse
            {
                Availabilities = rateAvailabilities
            };

            return Response.AsJson(response);
        }
    }
}