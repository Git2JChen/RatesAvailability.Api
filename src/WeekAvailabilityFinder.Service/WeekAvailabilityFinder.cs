using System;
using System.Collections.Generic;

namespace WeekAvailabilityFinder.Service
{
    public class WeekAvailabilityFinder : IWeekAvailabilityFinder
    {
        public IEnumerable<WeekAvailability> Get(DateTime fromDate, DateTime toDate)
        {
            var weekAvailabilities = new List<WeekAvailability>
                                {
                                    new WeekAvailability
                                        {
                                            FromDate = new DateTime(2016, 1, 1),
                                            ToDate = new DateTime(2016, 1, 31),
                                            Monday = true,
                                            Tuesday = false,
                                            Wednesday = true,
                                            Thursday = false,
                                            Friday = true,
                                            Saturday = true,
                                            Sunday = true
                                        },
                                    new WeekAvailability
                                        {
                                            FromDate = new DateTime(2016, 2, 1),
                                            ToDate = new DateTime(2016, 12, 31),
                                            Monday = true,
                                            Tuesday = false,
                                            Wednesday = true,
                                            Thursday = false,
                                            Friday = false,
                                            Saturday = true,
                                            Sunday = true
                                        }
                                };

            return weekAvailabilities;
        }
    }
}
