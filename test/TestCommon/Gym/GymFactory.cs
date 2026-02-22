namespace TestCommon.Gym;
using GymManagement.Domain.Gyms;
using Constants;

public static class GymFactory
{
    public static Gym CreateGym(
        string? name = null,
        Guid? subscriptionId = null,
        int? maxRooms = null,
        Guid? id = null)
    {
        Gym gym = new Gym(
            name: name ?? Constants.Gym.Name,
            subscriptionId: Constants.Subscription.Id,
            maxRooms: Constants.Subscription.MaxRoomFreeTier,
            id: id ?? Constants.Gym.Id);

        return gym;
    }
}