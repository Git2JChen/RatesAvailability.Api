using System;
using System.Collections.Generic;

namespace WeekAvailabilityFinder.Service
{
    public interface IWeekAvailabilityFinder
    {
        IList<WeekAvailability> Get(DateTime fromDate, DateTime toDate);
    }
}