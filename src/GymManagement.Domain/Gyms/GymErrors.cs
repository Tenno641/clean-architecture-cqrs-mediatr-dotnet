using ErrorOr;

namespace GymManagement.Domain.Gyms;

public static class GymErrors
{
    public readonly static Error CannotHaveMoreRooms = Error.Validation(
        code: "Gym.CannotHaveMoreRooms",
        description: "Subscription Doesn't allow adding more rooms");
}