using GymManagement.Application.Gyms.Commands.CreateGym;

namespace TestCommon.Gym;

public static class GymCommandFactory
{
    public static CreateGymCommand CreateCreateGymCommand(
        Guid? subscriptionId = null,
        string? name = null)
    {
        CreateGymCommand command = new CreateGymCommand(
            SubscriptionId: subscriptionId ?? Constants.Constants.Subscription.Id,
            Name: name ?? Constants.Constants.Gym.Name);

        return command;
    }
}