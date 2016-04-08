using System.ComponentModel;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using RateAvail.Api;

namespace Unit.Tests
{
    [TestFixture]
    public class RateAvailApiModuleTests
    {
        [Test]
        public void Should_return_status_ok_when_route_exists()
        {
            // Arrange
            var browser = new Browser(with => with.Module(new IndexModule()));

            // Act
            var result = browser.Get("/", with =>
            {
                with.HttpRequest();
            });

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void Should_return_Availablity_type()
        {
            // Arrange
            var browser = new Browser(with => with.Module(new IndexModule()));

            // Act
            var result = browser.Get("/RatesAvail", with =>
            {
                with.HttpRequest();
            });

            var availJson = result.Body.DeserializeJson<Availability>();

            // Assert
            Assert.That(availJson, Is.TypeOf<Availability>());
        }
    }

    public class Availability
    {
        
    }
}
