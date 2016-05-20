using System;
using FluentAssertions;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using RateAvail.Api;
using RateAvail.Api.Response;
using Newtonsoft.Json;

namespace Unit.Tests
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
    }
}
