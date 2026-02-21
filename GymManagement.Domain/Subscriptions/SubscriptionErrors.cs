using ErrorOr;

namespace GymManagement.Domain.Subscriptions;

public static class SubscriptionErrors
{
    public readonly static Error CannotHaveMoreGyms =
        Error.Validation("Subscription.CannotHaveMoreGyms",
            "A subscription cannot have more gyms than the subscription allows");

    public readonly static Error GymNotFound =
        Error.Validation(
            code: "Gym.NotFound",
            description: "Gym Not Found");
}