namespace FoodRatingApp.Services;

using FoodRatingApp.Model;

public interface IFsaClient
{
    Task<FsaAuthorityList> GetAuthorities();
}