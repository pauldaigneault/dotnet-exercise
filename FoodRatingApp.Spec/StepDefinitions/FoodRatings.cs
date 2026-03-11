using System.Text.Json;
using FoodRatingApp.Model;
using FoodRatingApp.Services;
using FoodRatingApp.Spec;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Reqnroll;

namespace FoodRatingApp.Spec.StepDefinitions
{
    [Binding]
    [Scope(Tag = "integration")]
    public class FoodRatingsApiStepDefinitions
    {
        private static readonly WebApplicationFactory<Program> Factory =
            new CustomWebApplicationFactory();
        private static readonly HttpClient _client = Factory.CreateClient();

        private readonly ApiScenarioContext _context;

        public FoodRatingsApiStepDefinitions(ApiScenarioContext context)
        {
            _context = context;
        }

        [Given("the API endpoint is {string}")]
        public void GivenTheAPIEndpointIs(string endpoint)
        {
            _context.ApiEndpoint = endpoint;
        }

        [When("the API is called")]
        public async Task WhenTheAPIIsCalled()
        {
            _context.ApiEndpoint.Should().NotBeNullOrEmpty();
            var uri = new Uri(_context.ApiEndpoint!, UriKind.Absolute);
            _context.Response = await _client.GetAsync(uri.PathAndQuery);
        }

        [Then(@"the response HTTP status code should be (\d+) [A-Za-z ]+")]
        public void ThenTheResponseHTTPStatusCodeShouldBe(int expectedStatusCode)
        {
            _context.Response.Should().NotBeNull();
            ((int)_context.Response!.StatusCode).Should().Be(expectedStatusCode);
        }

        [Then("the response should contain {string}")]
        public async Task ThenTheResponseShouldContain(string expectedContent)
        {
            _context.Response.Should().NotBeNull();
            var content = await _context.Response!.Content.ReadAsStringAsync();
            content.Should().Contain(expectedContent);
        }

        [Then("the response body should contain JSON field {string}")]
        public async Task ThenTheResponseBodyShouldContainJsonField(string fieldName)
        {
            _context.Response.Should().NotBeNull();
            var content = await _context.Response!.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(content);
            var root = doc.RootElement;
            var element = root.ValueKind == JsonValueKind.Array ? root[0] : root;
            element.TryGetProperty(fieldName, out _).Should().BeTrue($"Expected JSON field '{fieldName}' was not found in response");
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
                return Task.FromResult(
                    new FsaAuthorityList
                    {
                        Authorities =
                        [
                            new FsaAuthority { LocalAuthorityId = 1, Name = "Authority 1" },
                            new FsaAuthority { LocalAuthorityId = 2, Name = "Authority 2" },
                        ],
                    }
                );
            }
        }
    }
}
