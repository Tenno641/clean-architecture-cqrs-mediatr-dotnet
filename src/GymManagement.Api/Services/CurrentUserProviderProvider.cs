using GymManagement.Application.Common.Interfaces;
using GymManagement.Application.Models;

namespace GymManagement.Api.Services;

public class CurrentUserProvider: ICurrentUserProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public CurrentUserProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public CurrentUser GetCurrentUser()
    {
        Guid id = Guid.Parse(_httpContextAccessor.HttpContext?.User.Claims.First(claim => claim.Type.Equals("id")).Value ?? "");

        List<string>? permissions = _httpContextAccessor.HttpContext?.User.Claims
            .Where(claim => claim.Type.Equals("permissions"))
            .Select(claim => claim.Value)
            .ToList() ?? [];

        return new CurrentUser(
            Id: id,
            Permissions: permissions);
    }
}