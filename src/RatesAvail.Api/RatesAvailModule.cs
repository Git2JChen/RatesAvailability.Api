using System;
using System.Collections.Generic;
using System.Linq;
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
                var weekAvail = Request.Query["weekAvail"];
                var weekAvails = weekAvail.HasValue ? weekAvail.ToString().Split(new [] {','}) : new string[] {};

                return BuildResponse(weekAvails);
            };
        }

        private Nancy.Response BuildResponse(IEnumerable<string> avails)
        {
            if (avails == null || !avails.Any())
            {
                avails = new [] {"0", "0", "0", "0", "0", "0", "0"};
            }

            var rateAvailabilities = new List<Availability>
                {
                    new Availability
                    {
                        StartDate = GetDateFromQuery(Request.Query["sDate"]),
                        EndDate = GetDateFromQuery(Request.Query["eDate"]),
                        Mon = avails.ElementAt(0) == "1",
                        Tue = avails.ElementAt(1) == "1",
                        Wed = avails.ElementAt(2) == "1",
                        Thu = avails.ElementAt(3) == "1",
                        Fri = avails.ElementAt(4) == "1" ,
                        Sat = avails.ElementAt(5) == "1",
                        Sun = avails.ElementAt(6) == "1"
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