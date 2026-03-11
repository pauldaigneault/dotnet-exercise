using FoodRatingApp.Model;

namespace FoodRatingApp.Services;

public interface IFsaClient
{
    Task<FsaAuthorityList> GetAuthorities();
}
