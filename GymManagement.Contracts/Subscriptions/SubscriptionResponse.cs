using GymManagement.SharedKernel.Enums;

namespace GymManagement.Contracts.Subscriptions;

public record SubscriptionResponse(Guid Id, SubscriptionType SubscriptionType);