using System.Text.Json.Serialization;

namespace GymManagement.Contracts.Subscriptions;

public record CreateSubscription(SubscriptionType SubscriptionType, Guid AdminId);