using System;
using System.Collections.Generic;

namespace WeekAvailabilityFinder.Service
{
    public class WeekAvailabilityFinder : IWeekAvailabilityFinder
    {
        private WeekAvailability _availType1DefinedInCurrentYear;
        private WeekAvailability _availType2DefinedInCurrentYear;
        private int _currentYear =  DateTime.Now.Year;

        public WeekAvailabilityFinder()
        {
            _availType1DefinedInCurrentYear = new WeekAvailability
                    {
                        FromDate = new DateTime(_currentYear, 1, 1),
                        ToDate = new DateTime(_currentYear, 1, 31),
                        Monday = true,
                        Tuesday = false,
                        Wednesday = true,
                        Thursday = false,
                        Friday = true,
                        Saturday = true,
                        Sunday = true
                    };

            _availType2DefinedInCurrentYear = new WeekAvailability
                    {
                        FromDate = new DateTime(_currentYear, 2, 1),
                        ToDate = new DateTime(_currentYear, 12, 31),
                        Monday = true,
                        Tuesday = false,
                        Wednesday = true,
                        Thursday = false,
                        Friday = false,
                        Saturday = true,
                        Sunday = true
                    };
        }

        public IEnumerable<WeekAvailability> Get(DateTime fromDate, DateTime toDate)
        {
            var weekAvailabilities = new List<WeekAvailability>();

            if (fromDate == _availType1DefinedInCurrentYear.FromDate.Value 
                && toDate <= _availType1DefinedInCurrentYear.ToDate.Value)
            {
                var weekAvailRequested = CopyValueOf(_availType1DefinedInCurrentYear);
                weekAvailRequested.FromDate = _availType1DefinedInCurrentYear.FromDate;
                weekAvailRequested.ToDate = toDate;
                weekAvailabilities.Add(weekAvailRequested);

                var weekAvailType1 = CopyValueOf(_availType1DefinedInCurrentYear);
                weekAvailType1.FromDate = toDate;
                weekAvailType1.ToDate = _availType1DefinedInCurrentYear.ToDate;
                weekAvailabilities.Add(weekAvailType1);

                var weekAvailType2 = CopyValueOf(_availType2DefinedInCurrentYear);
                weekAvailabilities.Add(weekAvailType2);
            }
            else
            {
                weekAvailabilities.Add(_availType1DefinedInCurrentYear);
                weekAvailabilities.Add(_availType2DefinedInCurrentYear);
            }
            
            return weekAvailabilities;
        }

        public IEnumerable<WeekAvailability> AvailTypesDefinedInTheYear
        {
            get
            {
                return new List<WeekAvailability>
                    {
                        _availType1DefinedInCurrentYear,
                        _availType2DefinedInCurrentYear
                    };
            }
        }

        private WeekAvailability CopyValueOf(WeekAvailability weekAvailCopied)
        {
            var copyOfObject = new WeekAvailability
            {
                FromDate = weekAvailCopied.FromDate,
                ToDate = weekAvailCopied.ToDate,
                Monday = weekAvailCopied.Monday,
                Tuesday = weekAvailCopied.Tuesday,
                Wednesday = weekAvailCopied.Wednesday,
                Thursday = weekAvailCopied.Thursday,
                Friday = weekAvailCopied.Friday,
                Saturday = weekAvailCopied.Saturday,
                Sunday = weekAvailCopied.Sunday
            };

            return copyOfObject;

        }
    }
}
