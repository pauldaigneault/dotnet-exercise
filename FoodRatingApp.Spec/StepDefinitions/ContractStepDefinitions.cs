using Reqnroll;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using FoodRatingApp.Model;
using FoodRatingApp.Services;

namespace FoodRatingApp.Spec.StepDefinitions
{
    [Binding]
    [Scope(Tag = "contract")]
    public class ContractStepDefinitions
    {
        private static readonly WebApplicationFactory<Program> Factory = new ContractWebApplicationFactory();
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

        [Then("the response should contain at least 1 authority")]
        public void ThenTheResponseShouldContainAtLeast1Authority()
        {
            throw new PendingStepException();
        }

        [Then("every authority id should be a positive integer")]
        public void ThenEveryAuthorityIdShouldBeAPositiveInteger()
        {
            throw new PendingStepException();
        }

        [Then("every authority name should be a non-empty string")]
        public void ThenEveryAuthorityNameShouldBeANonEmptyString()
        {
            throw new PendingStepException();
        }

        [Then("the response content type should be {string}")]
        public void ThenTheResponseContentTypeShouldBe(string expectedContentType)
        {
            throw new PendingStepException();
        }

        [Then("the response should contain {int} rating items")]
        public void ThenTheResponseShouldContainRatingItems(int expectedCount)
        {
            throw new PendingStepException();
        }

        [Then("the sum of all rating values should equal {double}")]
        public void ThenTheSumOfAllRatingValuesShouldEqual(double expectedSum)
        {
            throw new PendingStepException();
        }

        [Then("the ratings should include the following categories:")]
        public void ThenTheRatingsShouldIncludeTheFollowingCategories(Table table)
        {
            throw new PendingStepException();
        }

        private sealed class ContractWebApplicationFactory : WebApplicationFactory<Program>
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
