namespace GymManagement.Application.Models;

public record CurrentUser(Guid Id, IReadOnlyCollection<string> Permissions);