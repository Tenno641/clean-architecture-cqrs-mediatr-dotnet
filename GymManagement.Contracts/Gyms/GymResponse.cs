namespace GymManagement.Contracts.Gyms;

public record GymResponse(Guid Id, string Name, Guid SubscriptionId, List<Guid> RoomsIds);