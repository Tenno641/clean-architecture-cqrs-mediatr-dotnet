namespace GymManagement.Application.Authorization;

[AttributeUsage(validOn: AttributeTargets.Class, AllowMultiple = true)]
public class AuthorizeAttribute : Attribute
{
    public string? Permissions { get; set; }
}