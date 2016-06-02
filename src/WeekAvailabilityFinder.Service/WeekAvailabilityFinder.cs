using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekAvailabilityFinder.Service
{
    public class WeekAvailabilityFinder : IWeekAvailabilityFinder
    {
        public IList<WeekAvailability> Get(DateTime fromDate, DateTime toDate)
        {
            throw new NotImplementedException();
        }
    }
}
