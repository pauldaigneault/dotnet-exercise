using System.Text.Json.Serialization;

namespace FoodRatingApp.Model;

public class FsaAuthorityList
{
    [JsonPropertyName("authorities")]
    public required List<FsaAuthority> Authorities { get; set; }

    public override string ToString()
    {
        return $"FsaAuthorityList[{nameof(Authorities)}={Authorities}]";
    }
}
