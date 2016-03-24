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
    }
}
