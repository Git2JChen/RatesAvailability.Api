using System;
using System.Collections.Generic;

namespace WeekAvailabilityFinder.Service
{
    public class WeekAvailabilityFinder : IWeekAvailabilityFinder
    {
        public IEnumerable<WeekAvailability> Get(DateTime fromDate, DateTime toDate)
        {
            var currentYear = DateTime.Now.Year;
            var availType1_DefinedInCurrentYear =
                new WeekAvailability
                {
                    FromDate = new DateTime(currentYear, 1, 1),
                    ToDate = new DateTime(currentYear, 1, 31),
                    Monday = true,
                    Tuesday = false,
                    Wednesday = true,
                    Thursday = false,
                    Friday = true,
                    Saturday = true,
                    Sunday = true
                };
            var availType2_DefinedInCurrentYear =
                new WeekAvailability
                {
                    FromDate = new DateTime(currentYear, 2, 1),
                    ToDate = new DateTime(currentYear, 12, 31),
                    Monday = true,
                    Tuesday = false,
                    Wednesday = true,
                    Thursday = false,
                    Friday = false,
                    Saturday = true,
                    Sunday = true
                };

            var weekAvailabilities = new List<WeekAvailability>
                    {
                        availType1_DefinedInCurrentYear,
                        availType2_DefinedInCurrentYear
                    };

            return weekAvailabilities;
        }
    }
}
