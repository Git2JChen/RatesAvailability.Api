using System;
using System.Collections.Generic;

namespace WeekAvailabilityFinder.Service
{
    public interface IWeekAvailabilityFinder
    {
        IEnumerable<WeekAvailability> Get(DateTime fromDate, DateTime toDate);
        IEnumerable<WeekAvailability> AvailTypesDefinedInTheYear { get; }
    }
}