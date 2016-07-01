using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using RateAvail.Api.Model;
using RateAvail.Api.Response;
using WeekAvailabilityFinder.Service;

namespace RateAvail.Api
{
    public class RatesAvailModule : NancyModule
    {
        private readonly int _currentYear = DateTime.Now.Year;
        private dynamic _startDateRequested;
        private dynamic _endDateRequested;

        public RatesAvailModule(IWeekAvailabilityFinder weekAvailabilityFinder)
        {
            Get["/"] = parameters =>
            {
                return "Nancy.HttpStatusCode is OK";
            };

            Get["/RatesAvail"] = with =>
            {
                var weekAvailsRequested = GetWeekAvailabilities();
                _startDateRequested = GetDateFromQuery(Request.Query["sDate"]);
                _endDateRequested = GetDateFromQuery(Request.Query["eDate"]);

                var weekAvailsInWholeYear = weekAvailabilityFinder.Get(_startDateRequested, _endDateRequested);

                return BuildResponse(weekAvailsRequested, weekAvailsInWholeYear);
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

        private Nancy.Response BuildResponse(IEnumerable<string> avails, IEnumerable<WeekAvailability> weekAvailabilities)
        {
            var rateAvailabilities = new List<Availability>();
            var weekweekAvailabilityInWeek1 = weekAvailabilities.ToList()[0];
            var weekweekAvailabilityInWeek2 = weekAvailabilities.ToList()[1];
            var availsRequested = avails.Select(StringToBoolean).ToList();

            if (_endDateRequested >= weekweekAvailabilityInWeek1.ToDate 
                    && _endDateRequested <= weekweekAvailabilityInWeek2.ToDate)
            {
                var availabilityInWeek1 = new Availability
                {
                    StartDate = _startDateRequested,
                    EndDate = (DateTime) weekweekAvailabilityInWeek1.ToDate,
                    Mon = availsRequested[0],
                    Tue = availsRequested[1],
                    Wed = availsRequested[2],
                    Thu = availsRequested[3],
                    Fri = availsRequested[4],
                    Sat = availsRequested[5],
                    Sun = availsRequested[6]
                };

                rateAvailabilities.Add(availabilityInWeek1);
            }

            var response = new RatesResponse
            {
                Availabilities = rateAvailabilities
            };

            return Response.AsJson(response);
        }

        private static bool StringToBoolean(string s)
        {
            return s == "1";
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