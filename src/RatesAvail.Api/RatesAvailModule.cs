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
            var rateAvailabilities = new List<Availability>
                {
                    new Availability
                    {
                        StartDate = GetDateFromQuery(Request.Query["sDate"]),
                        EndDate = GetDateFromQuery(Request.Query["eDate"]),
                    },
                };

            var response = new RatesResponse
            {
                Availabilities = rateAvailabilities
            };

            return Response.AsJson(response);
        }

        private DateTime GetDateFromQuery(dynamic dateQury)
        {
            var date = !dateQury.HasValue || string.IsNullOrEmpty(dateQury)
                ? DateTime.Today
                : Convert.ToDateTime(dateQury.Value);

            return date;
        }
    }
}