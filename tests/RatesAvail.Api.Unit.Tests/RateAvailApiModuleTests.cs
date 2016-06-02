using System;
using System.Linq;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using RateAvail.Api;
using RateAvail.Api.Response;

namespace RatesAvail.Api.Unit.Tests
{
    [TestFixture]
    public class RateAvailApiModuleTests
    {
        [Test]
        public void Should_return_status_ok_when_route_exists()
        {
            // Arrange
            var browser = new Browser(with => with.Module(new RatesAvailModule()));

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
            var browser = new Browser(with => with.Module(new RatesAvailModule()));

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
            var browser = new Browser(with => with.Module(new RatesAvailModule()));

            // Act
            const string StartDateExpected = "2016-01-01";
            const string EndDateExpected = "2016-12-31";
            var result = browser.Get("/RatesAvail", with =>
            {
                with.Query("sDate", StartDateExpected);
                with.Query("eDate", EndDateExpected);
            });

            var ratesResponse = result.Body.DeserializeJson<RatesResponse>();
            var startDate = ratesResponse.Availabilities[0].StartDate;
            var endDate = ratesResponse.Availabilities[0].EndDate;

            // Assert
            Assert.That(startDate, Is.EqualTo(Convert.ToDateTime(StartDateExpected)));
            Assert.That(endDate, Is.EqualTo(Convert.ToDateTime(EndDateExpected)));
        }

        [Test]
        public void Should_return_Availablity_with_days_in_week_expected()
        {
            // Arrange
            var browser = new Browser(with => with.Module(new RatesAvailModule()));

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
            var browser = new Browser(with => with.Module(new RatesAvailModule()));

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
        public void EndDate_in_Availablity_will_default_to_today_if_it_is_unknown(string dateQuery)
        {
            // Arrange
            var browser = new Browser(with => with.Module(new RatesAvailModule()));

            // Act
            var endDateExpected = DateTime.Today.ToString("yyyy-MM-dd");
            var result = browser.Get("/RatesAvail", with =>
            {
                with.Query("eDate", dateQuery);
            });

            var ratesResponse = result.Body.DeserializeJson<RatesResponse>();
            var endDate = ratesResponse.Availabilities[0].EndDate;

            // Assert
            Assert.That(endDate, Is.EqualTo(Convert.ToDateTime(endDateExpected)));
        }

        [TestCase("1,0,1,0,1,1,1", "True,False,True,False,True,True,True")]
        [TestCase("0,0,1,0,1,0,1", "False,False,True,False,True,False,True")]
        [TestCase("1,1,1,0,1,1,0", "True,True,True,False,True,True,False")]
        public void Should_Map_Availablity_for_each_day_in_the_week(string inputWeekAvail, string expectedWeekAvail)
        {
            // Arrange
            var expectedResults = expectedWeekAvail.Split(',')
                                    .Select(Convert.ToBoolean).ToList();
            var browser = new Browser(with => with.Module(new RatesAvailModule()));

            // Act
            var result = browser.Get("/RatesAvail", with =>
            {
                with.Query("weekAvail", inputWeekAvail);
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
