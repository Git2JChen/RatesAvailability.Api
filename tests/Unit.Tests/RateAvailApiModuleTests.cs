using Nancy;
using Nancy.Testing;
using NUnit.Framework;

namespace Unit.Tests
{
    [TestFixture]
    public class RateAvailApiModuleTests
    {
        [Test]
        public void Should_return_status_ok_when_route_exists()
        {
            // Arrange
            var bootstrapper = new DefaultNancyBootstrapper();
            var browser = new Browser(bootstrapper);

            // Act
            var result = browser.Get("/", with =>
            {
                with.HttpRequest();
            });

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
