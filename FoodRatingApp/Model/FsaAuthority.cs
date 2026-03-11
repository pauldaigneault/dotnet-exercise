namespace FoodRatingApp.Model;

public class FsaAuthority
{
    public int LocalAuthorityId { get; set; }

    public required string Name { get; set; }

    public override string ToString()
    {
        return $"FsaAuthority[id={LocalAuthorityId}, name='{Name}']";
    }
}
