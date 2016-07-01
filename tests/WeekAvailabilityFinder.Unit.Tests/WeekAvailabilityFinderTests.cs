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
        private int _currentYear;

        [SetUp]
        public void SetUp()
        {
            _currentYear = DateTime.Now.Year;
        }

        [Test]
        public void will_return_availability_of_each_week_through_the_year()
        {
            // Arrange
            var startDate = new DateTime(_currentYear, 1, 1);
            var endtDate = new DateTime(_currentYear, 12, 31);
            var weekAvailabilityFinder = new Service.WeekAvailabilityFinder();
            var expected = new List<WeekAvailability>
                                {
                                    new WeekAvailability
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
                                        },
                                    new WeekAvailability
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
                                        }
                                };

            // Act
            var actual = weekAvailabilityFinder.Get(startDate, endtDate).ToList();

            // Assert
            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void will_return_first_WeekAvailability_element_for_weeks_ranging_from_01_01_to_01_31()
        {
            // Arrange
            var startDate = new DateTime(_currentYear, 1, 1);
            var endtDate = new DateTime(_currentYear, 12, 31);
            var weekAvailabilityFinder = new Service.WeekAvailabilityFinder();
            var expected = new List<WeekAvailability>
                                {
                                    new WeekAvailability
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
                                        }
                                };

            // Act
            var actual = weekAvailabilityFinder.Get(startDate, endtDate).ToList();

            // Assert
            actual[0].ShouldBeEquivalentTo(expected[0]);
        }

        [Test]
        public void will_return_second_WeekAvailability_element_for_weeks_ranging_from_02_01_to_12_31()
        {
            // Arrange
            var startDate = new DateTime(_currentYear, 1, 1);
            var endtDate = new DateTime(_currentYear, 12, 31);
            var weekAvailabilityFinder = new Service.WeekAvailabilityFinder();
            var expected = new List<WeekAvailability>
                                {
                                    new WeekAvailability(),
                                    new WeekAvailability
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
                                        }
                                };

            // Act
            var actual = weekAvailabilityFinder.Get(startDate, endtDate).ToList();

            // Assert
            actual[1].ShouldBeEquivalentTo(expected[1]);
        }

        /* Predefined WeekAvailability  ||-------------|---------------------------||    */
        /* WeekAvailability requested   |--------|                                       */
        /* Resultant WeekAvailability   ||-------|-----|---------------------------||    */
        [Test]
        public void will_return_WeekAvailability_element_when_availability_request_is_at_start_of_year()
        {
            // Arrange
            var startDate = new DateTime(_currentYear, 1, 1);
            var endtDate = new DateTime(_currentYear, 1, 10);
            var weekAvailabilityFinder = new Service.WeekAvailabilityFinder();

            // Act
            var actual = weekAvailabilityFinder.Get(startDate, endtDate).ToList();

            // Assert
            actual.Count.ShouldBeEquivalentTo(3);
        }
    }
}
