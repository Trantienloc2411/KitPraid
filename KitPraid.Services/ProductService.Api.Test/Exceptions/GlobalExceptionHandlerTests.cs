using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Http.Json;

namespace ProductService.Api.Test.Exceptions
{
    [TestFixture]
    public class GlobalExceptionHandlerTests
    {
        private WebApplication? _app;
        private HttpClient? _client;

        [SetUp]
        public void Setup()
        {
            _app = TestApplication.CreateApp();
            _app.Start();

            // This is IDisposable
            _client = _app.GetTestClient();
        }

        [TearDown]
        public async Task TearDown()
        {
            if (_client != null)
            {
                _client.Dispose();
                _client = null;
            }

            if (_app != null)
            {
                await _app.DisposeAsync();
                _app = null;
            }
        }

        [TestCase("/test-error/notfound", HttpStatusCode.NotFound, "Not Found")]
        [TestCase("/test-error/bad", HttpStatusCode.BadRequest, "Bad Request")]
        [TestCase("/test-error/unauth", HttpStatusCode.Unauthorized, "Unauthorized")]
        [TestCase("/test-error/forbidden", HttpStatusCode.Forbidden, "Forbidden")]
        [TestCase("/test-error/conflict", HttpStatusCode.Conflict, "Conflict")]
        public async Task Should_Map_Custom_Exceptions_Correctly(
            string url, HttpStatusCode expectedStatus, string expectedTitle)
        {
            var response = await _client!.GetAsync(url);
            var body = await response.Content.ReadFromJsonAsync<ProblemDetails>();

            Assert.That(response.StatusCode, Is.EqualTo(expectedStatus));
            Assert.That(body!.Title, Is.EqualTo(expectedTitle));
        }

        [Test]
        public async Task Should_Map_Unknown_Exception_To_500()
        {
            var response = await _client!.GetAsync("/test-error/unknown");
            var body = await response.Content.ReadFromJsonAsync<ProblemDetails>();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));
            Assert.That(body!.Title, Is.EqualTo("Internal Server Error"));
        }
    }
}