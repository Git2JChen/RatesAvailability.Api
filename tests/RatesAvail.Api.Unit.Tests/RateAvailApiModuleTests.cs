using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using RateAvail.Api;
using RateAvail.Api.Response;
using Rhino.Mocks;
using WeekAvailabilityFinder.Service;

namespace RatesAvail.Api.Unit.Tests
{
    [TestFixture]
    public class RateAvailApiModuleTests
    {
        private IWeekAvailabilityFinder _weekAvailabilityFinder;

        [SetUp]
        public void SetUp()
        {
            var weekAvailabilities = new List<WeekAvailability>
            {
                new WeekAvailability
                {
                    FromDate = new DateTime(2016, 1, 1),
                    ToDate = new DateTime(2016, 1, 31)
                },
                new WeekAvailability
                {
                    FromDate = new DateTime(2016, 2, 1),
                    ToDate = new DateTime(2016, 12, 31)
                }
            };
            _weekAvailabilityFinder = MockRepository.GenerateMock<IWeekAvailabilityFinder>();
            _weekAvailabilityFinder.Stub(x => x.Get(Arg<DateTime>.Is.Anything, Arg<DateTime>.Is.Anything))
                .Return(weekAvailabilities);
        }

        [Test]
        public void Should_return_status_ok_when_route_exists()
        {
            // Arrange

            var browser = new Browser(with => with.Module(new RatesAvailModule(_weekAvailabilityFinder)));

            // Act
            var result = browser.Get("/", with =>
            {
                with.HttpRequest();
            });

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void Should_return_Response_type()
        {
            // Arrange
            var browser = new Browser(with => with.Module(new RatesAvailModule(_weekAvailabilityFinder)));

            // Act
            var result = browser.Get("/RatesAvail", with =>
            {
                with.HttpRequest();
            });

            var response = result.Body.DeserializeJson<RatesResponse>();

            // Assert
            Assert.That(response, Is.TypeOf<RatesResponse>());
        }

        [Test]
        public void Should_return_Availablity_with_startDate_and_endDate_expected()
        {
            // Arrange
            var browser = new Browser(with => with.Module(new RatesAvailModule(_weekAvailabilityFinder)));

            // Act
            const string StartDateRequested = "2016-01-01";
            const string EndDateRequested = "2016-12-31";
            const string StartDateExpected1 = "2016-01-01";
            const string EndDateExpected1 = "2016-01-31";
            var result = browser.Get("/RatesAvail", with =>
            {
                with.Query("sDate", StartDateRequested);
                with.Query("eDate", EndDateRequested);
            });

            var ratesResponse = result.Body.DeserializeJson<RatesResponse>();
            var startDate1 = ratesResponse.Availabilities[0].StartDate;
            var endDate1 = ratesResponse.Availabilities[0].EndDate;

            // Assert
            Assert.That(startDate1, Is.EqualTo(Convert.ToDateTime(StartDateExpected1)));
            Assert.That(endDate1, Is.EqualTo(Convert.ToDateTime(EndDateExpected1)));
        }

        [Test]
        public void Should_return_Availablity_with_days_in_week_expected()
        {
            // Arrange
            var browser = new Browser(with => with.Module(new RatesAvailModule(_weekAvailabilityFinder)));

            // Act
            const string StartDateExpected = "2016-01-01";
            const string EndDateExpected = "2016-12-31";
            var result = browser.Get("/RatesAvail", with =>
            {
                with.Query("sDate", StartDateExpected);
                with.Query("eDate", EndDateExpected);
            });

            var ratesResponse = result.Body.DeserializeJson<RatesResponse>();
            var mon = ratesResponse.Availabilities[0].Mon;
            var tue = ratesResponse.Availabilities[0].Tue;
            var wed = ratesResponse.Availabilities[0].Wed;
            var thu = ratesResponse.Availabilities[0].Thu;
            var fri = ratesResponse.Availabilities[0].Fri;
            var sat = ratesResponse.Availabilities[0].Sat;
            var sun = ratesResponse.Availabilities[0].Sun;

            // Assert
            Assert.That(mon, Is.False);
            Assert.That(tue, Is.False);
            Assert.That(wed, Is.False);
            Assert.That(thu, Is.False);
            Assert.That(fri, Is.False);
            Assert.That(sat, Is.False);
            Assert.That(sun, Is.False);
        }

        [TestCase("")]
        [TestCase(null)]
        public void StartDate_in_Availablity_will_default_to_today_if_it_is_unknown(string dateQuery)
        {
            // Arrange
            var browser = new Browser(with => with.Module(new RatesAvailModule(_weekAvailabilityFinder)));

            // Act
            var startDateExpected = DateTime.Today.ToString("yyyy-MM-dd");
            var result = browser.Get("/RatesAvail", with =>
            {
                with.Query("sDate", dateQuery);
            });

            var ratesResponse = result.Body.DeserializeJson<RatesResponse>();
            var startDate = ratesResponse.Availabilities[0].StartDate;

            // Assert
            Assert.That(startDate, Is.EqualTo(Convert.ToDateTime(startDateExpected)));
        }

        [TestCase("")]
        [TestCase(null)]
        public void EndDate_in_Availablity_will_default_to_2016_01_31_if_it_is_unknown(string dateQuery)
        {
            // Arrange
            var browser = new Browser(with => with.Module(new RatesAvailModule(_weekAvailabilityFinder)));

            // Act
            var endDateExpected = new DateTime(2016, 1, 31);
            var result = browser.Get("/RatesAvail", with =>
            {
                with.Query("eDate", dateQuery);
            });

            var ratesResponse = result.Body.DeserializeJson<RatesResponse>();
            var endDate = ratesResponse.Availabilities[0].EndDate;

            // Assert
            Assert.That(endDate, Is.EqualTo(Convert.ToDateTime(endDateExpected)));
        }

        /* Predefined WeekAvailability  ||-------------|---------------------------||    */
        /* WeekAvailability requested   |--------|                                       */
        /* Resultant WeekAvailability   ||-------|-----|---------------------------||    */
        [TestCase("1,0,1,0,1,1,1", "True,False,True,False,True,True,True")]
        [TestCase("0,0,1,0,1,0,1", "False,False,True,False,True,False,True")]
        [TestCase("1,1,1,0,1,1,0", "True,True,True,False,True,True,False")]
        public void Should_return_availability_requested_in_first_availability_block_when_request_has_no_date_range(string inputWeekAvail, string expectedWeekAvail)
        {
            // Arrange
            var expectedResults = expectedWeekAvail.Split(',')
                                    .Select(Convert.ToBoolean).ToList();
            var browser = new Browser(with => with.Module(new RatesAvailModule(_weekAvailabilityFinder)));

            // Act
            var result = browser.Get("/RatesAvail", with =>
            {
                with.Query("weekAvail", inputWeekAvail);
            });

            var ratesResponse = result.Body.DeserializeJson<RatesResponse>();

            var availabilityBlock1 = ratesResponse.Availabilities[0];
            var mon = availabilityBlock1.Mon;
            var tue = availabilityBlock1.Tue;
            var wed = availabilityBlock1.Wed;
            var thu = availabilityBlock1.Thu;
            var fri = availabilityBlock1.Fri;
            var sat = availabilityBlock1.Sat;
            var sun = availabilityBlock1.Sun;

            // Assert
            Assert.That(mon, Is.EqualTo(expectedResults[0]));
            Assert.That(tue, Is.EqualTo(expectedResults[1]));
            Assert.That(wed, Is.EqualTo(expectedResults[2]));
            Assert.That(thu, Is.EqualTo(expectedResults[3]));
            Assert.That(fri, Is.EqualTo(expectedResults[4]));
            Assert.That(sat, Is.EqualTo(expectedResults[5]));
            Assert.That(sun, Is.EqualTo(expectedResults[6]));
        }
    }
}
