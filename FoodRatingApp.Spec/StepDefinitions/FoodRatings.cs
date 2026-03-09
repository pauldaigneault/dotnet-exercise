using System.Text.Json;
using FoodRatingApp.Model;
using FoodRatingApp.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NUnit.Framework;
using Reqnroll;

namespace FoodRatingApp.Spec.StepDefinitions
{
    [Binding]
    [Scope(Tag = "integration")]
    public class FoodRatingsApiStepDefinitions
    {
        private static readonly WebApplicationFactory<Program> Factory = new CustomWebApplicationFactory();
        private static readonly HttpClient _client = Factory.CreateClient();
        private HttpResponseMessage? _response;
        private string? _apiEndpoint;

        [Given("the API endpoint is {string}")]
        public void GivenTheAPIEndpointIs(string endpoint)
        {
            _apiEndpoint = endpoint;
        }

        [When("the API is called")]
        public async Task WhenTheAPIIsCalled()
        {
            Assert.That(_apiEndpoint, Is.Not.Null.And.Not.Empty);
            var uri = new Uri(_apiEndpoint!, UriKind.Absolute);
            _response = await _client.GetAsync(uri.PathAndQuery);
        }

        [Then(@"the response HTTP status code should be (\d+) [A-Za-z ]+")]
        public void ThenTheResponseHTTPStatusCodeShouldBe(int expectedStatusCode)
        {
            Assert.That(_response, Is.Not.Null);
            Assert.That((int)_response!.StatusCode, Is.EqualTo(expectedStatusCode));
        }

        [Then("the response should contain {string}")]
        public async Task ThenTheResponseShouldContain(string expectedContent)
        {
            Assert.That(_response, Is.Not.Null);
            var content = await _response!.Content.ReadAsStringAsync();
            Assert.That(content, Does.Contain(expectedContent));
        }

        [Then("the response should contain JSON field {string}")]
        public async Task ThenTheResponseShouldContainJsonField(string fieldName)
        {
            Assert.That(_response, Is.Not.Null);
            var content = await _response!.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(content);
            var root = doc.RootElement;
            var element = root.ValueKind == JsonValueKind.Array ? root[0] : root;
            Assert.That(element.TryGetProperty(fieldName, out _), Is.True,
                $"Expected JSON field '{fieldName}' was not found in response");
        }

        private sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
        {
            protected override void ConfigureWebHost(IWebHostBuilder builder)
            {
                builder.ConfigureServices(services =>
                {
                    services.RemoveAll<IFsaClient>();
                    services.AddSingleton<IFsaClient, StubFsaClient>();
                });
            }
        }

        private sealed class StubFsaClient : IFsaClient
        {
            public Task<FsaAuthorityList> GetAuthorities()
            {
                return Task.FromResult(new FsaAuthorityList
                {
                    Authorities =
                    [
                        new FsaAuthority { LocalAuthorityId = 1, Name = "Authority 1" },
                        new FsaAuthority { LocalAuthorityId = 2, Name = "Authority 2" }
                    ]
                });
            }
        }
    }
}
