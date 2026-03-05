using Reqnroll;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NGE.TechTest.Model;
using NGE.TechTest.Services;

namespace FoodRatings.StepDefinitions
{
    [Binding]
    public class FoodRatingsApiStepDefinitions
    {
        private static readonly WebApplicationFactory<Program> Factory = new CustomWebApplicationFactory();
        private static readonly HttpClient client = Factory.CreateClient();
        private HttpResponseMessage? response;
        private string? apiEndpoint;

        [Given("the API endpoint is {string}")]
        public void GivenTheAPIEndpointIs(string endpoint)
        {
            apiEndpoint = endpoint;
        }

        [When("the API is called")]
        public async Task WhenTheAPIIsCalled()
        {
            Assert.That(apiEndpoint, Is.Not.Null.And.Not.Empty);
            var uri = new Uri(apiEndpoint!, UriKind.Absolute);
            response = await client.GetAsync(uri.PathAndQuery);
        }

        [Then("the response status code should be {int}")]
        public void ThenTheResponseStatusCodeShouldBe(int expectedStatusCode)
        {
            Assert.That(response, Is.Not.Null);
            Assert.AreEqual(expectedStatusCode, (int)response!.StatusCode);
        }

        [Then("the response should contain {string}")]
        public async Task ThenTheResponseShouldContain(string expectedContent)
        {
            Assert.That(response, Is.Not.Null);
            var content = await response!.Content.ReadAsStringAsync();
            Assert.IsTrue(content.Contains(expectedContent));
        }

        [Then("the response should contain JSON field {string}")]
        public async Task ThenTheResponseShouldContainJsonField(string fieldName)
        {
            Assert.That(response, Is.Not.Null);
            var content = await response!.Content.ReadAsStringAsync();
            Assert.IsTrue(content.Contains($"\"{fieldName}\""));
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