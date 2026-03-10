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
    [Scope(Tag = "contract")]
    public class ContractStepDefinitions
    {
        private static readonly WebApplicationFactory<Program> Factory =
            new ContractWebApplicationFactory();
        private static readonly HttpClient _client = Factory.CreateClient();
        private static List<FsaAuthority> _stubbedAuthorities = [];

        private readonly ApiScenarioContext _context;

        public ContractStepDefinitions(ApiScenarioContext context)
        {
            _context = context;
        }

        [Given("the following authorities exist:")]
        public void GivenTheFollowingAuthoritiesExist(Table table)
        {
            _stubbedAuthorities = table
                .Rows.Select(row => new FsaAuthority
                {
                    LocalAuthorityId = int.Parse(row["id"]),
                    Name = row["name"],
                })
                .ToList();
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

        [Then(@"the response should contain at least (\d+) authorit(?:y|ies)")]
        public void ThenTheResponseShouldContainAtLeastAuthorities(int minimumCount)
        {
            throw new PendingStepException();
        }

        [Then("each authority has a valid identifier")]
        public void ThenEachAuthorityHasAValidNumericIdentifier()
        {
            throw new PendingStepException();
        }

        [Then("each authority has a displayable name")]
        public void ThenEachAuthorityHasADisplayableName()
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
                return Task.FromResult(new FsaAuthorityList { Authorities = _stubbedAuthorities });
            }
        }
    }
}
