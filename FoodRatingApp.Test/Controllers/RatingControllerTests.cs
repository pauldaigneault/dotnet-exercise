using FoodRatingApp.Controllers;
using FoodRatingApp.Model;
using FoodRatingApp.Services;

namespace FoodRatingApp.Test.Controllers;

public class RatingControllerTests
{
    [Fact]
    public async Task GetAsync_ReturnsAllAuthorities()
    {
        var authorityList = new FsaAuthorityList
        {
            Authorities = new List<FsaAuthority>
            {
                new FsaAuthority { Name = "authority1", LocalAuthorityId = 1 },
                new FsaAuthority { Name = "authority2", LocalAuthorityId = 2 },
            },
        };

        var fsaClient = Substitute.For<IFsaClient>();
        fsaClient.GetAuthorities().Returns(Task.FromResult<FsaAuthorityList>(authorityList));
        var controller = new RatingController(fsaClient);

        var jsonResult = await controller.GetAsync();

        jsonResult.Value.Should().BeAssignableTo<IEnumerable<Authority>>();
        var authorities = ((IEnumerable<Authority>)jsonResult.Value).ToArray();
        authorities.Length.Should().Be(2);
        authorities[0].Name.Should().Be("authority1");
        authorities[0].Id.Should().Be(1);
        authorities[1].Name.Should().Be("authority2");
        authorities[1].Id.Should().Be(2);
    }
}
