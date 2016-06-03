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

            // Act
            var actual = weekAvailabilityFinder.Get(startDate, endtDate).ToList();

            // Assert
            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void will_return_first_WeekAvailability_element_for_weeks_ranging_from_2016_01_01_to_2016_01_31()
        {
            // Arrange
            var startDate = new DateTime(2016, 1, 1);
            var endtDate = new DateTime(2016, 12, 31);
            var weekAvailabilityFinder = new Service.WeekAvailabilityFinder();
            var expected = new List<WeekAvailability>
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
                                        }
                                };

            // Act
            var actual = weekAvailabilityFinder.Get(startDate, endtDate).ToList();

            // Assert
            actual[0].ShouldBeEquivalentTo(expected[0]);
        }

        [Test]
        public void will_return_second_WeekAvailability_element_for_weeks_ranging_from_2016_02_01_to_2016_12_31()
        {
            // Arrange
            var startDate = new DateTime(2016, 1, 1);
            var endtDate = new DateTime(2016, 12, 31);
            var weekAvailabilityFinder = new Service.WeekAvailabilityFinder();
            var expected = new List<WeekAvailability>
                                {
                                    new WeekAvailability(),
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

            // Act
            var actual = weekAvailabilityFinder.Get(startDate, endtDate).ToList();

            // Assert
            actual[1].ShouldBeEquivalentTo(expected[1]);
        }
    }
}
