using Ceii.Api.Data.Enums;

namespace Ceii.Api.Data.Entities;

public class User
{
    public string? Email { get; set; }

    public string? OAuthId { get; set; }

    public string? GivenName { get; set; }

    public string? FamilyName { get; set; }

    public string? GoogleImageUrl { get; set; }

    public IdentityRole Role { get; set; }
}