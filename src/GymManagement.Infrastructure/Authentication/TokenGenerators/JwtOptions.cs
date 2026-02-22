namespace GymManagement.Infrastructure.Authentication.TokenGenerators;

public class JwtOptions
{
    public readonly static string Section = "JwtOptions";
    
    public string? Audience { get; init; }
    public string? Secret { get; init; }
    public string? Issuer { get; init; }
    public int ExpirationTimeInSeconds { get; init; }
}