using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using RateAvail.Api.Model;
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
                var weekAvails = GetWeekAvailabilities();

                return BuildResponse(weekAvails);
            };
        }

        private dynamic GetWeekAvailabilities()
        {
            var weekAvail = Request.Query["weekAvail"];
            var weekAvails = weekAvail.HasValue
                ? weekAvail.ToString().Split(new[] {','})
                : new[] {"0", "0", "0", "0", "0", "0", "0"};

            return weekAvails;
        }

        private Nancy.Response BuildResponse(IEnumerable<string> avails)
        {
            var rateAvailabilities = new List<Availability>
                {
                    new Availability
                    {
                        StartDate = GetDateFromQuery(Request.Query["sDate"]),
                        EndDate = GetDateFromQuery(Request.Query["eDate"]),
                        Mon = GetAvailabilityByDay(avails, WeekDays.Monday),
                        Tue = GetAvailabilityByDay(avails, WeekDays.Tuesday),
                        Wed = GetAvailabilityByDay(avails, WeekDays.Wednesday),
                        Thu = GetAvailabilityByDay(avails, WeekDays.Thursday),
                        Fri = GetAvailabilityByDay(avails, WeekDays.Friday),
                        Sat = GetAvailabilityByDay(avails, WeekDays.Saturday),
                        Sun = GetAvailabilityByDay(avails, WeekDays.Sunday)
                    },
                };

            var response = new RatesResponse
            {
                Availabilities = rateAvailabilities
            };

            return Response.AsJson(response);
        }

        private static bool GetAvailabilityByDay(IEnumerable<string> avails, WeekDays day)
        {
            var index = Convert.ToInt32(day);
            return avails.ElementAt(index) == "1";
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