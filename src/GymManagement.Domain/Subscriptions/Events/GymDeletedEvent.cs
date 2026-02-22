using GymManagement.Domain.Common.Events;

namespace GymManagement.Domain.Subscriptions.Events;

public record GymDeletedEvent(Guid GymId) : IDomainEvent;