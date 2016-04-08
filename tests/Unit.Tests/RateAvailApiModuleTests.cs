using FluentAssertions;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using RateAvail.Api;
using RateAvail.Api.Response;
using Newtonsoft.Json;
//using Response = Nancy.Response;

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
        public void Should_return_Availablity_with_JSon_schema_expected()
        {
            // Arrange
            var browser = new Browser(with => with.Module(new RatesAvailModule()));
            const string jsonStringExpected =
                "{" +
                    "\"availability\":[" +
                        "{" +
                        "}" +
                    "]" +
                "}";

            // Act
            var result = browser.Get("/RatesAvail", with =>
            {
                ;
            });

            var actualJsonString = JsonConvert.SerializeObject(result.Body.DeserializeJson<RatesResponse>());

            // Assert
            actualJsonString.Should().Be(jsonStringExpected);
        }
    }
}
