using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using WeekAvailabilityFinder.Service;

namespace WeekAvailabilityFinder.Unit.Tests
{
    [TestFixture]
    public class WeekAvailabilityFinderTests
    {
        [Test]
        public void will_return_availability_of_each_week_through_the_year()
        {
            // Arrange
            var startDate = new DateTime(2016, 1, 1);
            var endtDate = new DateTime(2016, 12, 31);
            var weekAvailabilityFinder = new Service.WeekAvailabilityFinder();
            var expected = new List<WeekAvailability>
                                {
                                    new WeekAvailability
                                        {
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
                                            Monday = true,
                                            Tuesday = false,
                                            Wednesday = true,
                                            Thursday = false,
                                            Friday = false,
                                            Saturday = true,
                                            Sunday = true
                                        }
                                };

            // Act
            var actual = weekAvailabilityFinder.Get(startDate, endtDate).ToList();

            // Assert
            actual.ShouldBeEquivalentTo(expected);
        }
    }
}
