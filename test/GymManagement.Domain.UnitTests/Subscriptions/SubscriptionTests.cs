using ErrorOr;
using GymManagement.Domain.Gyms;
using GymManagement.Domain.Subscriptions;
using TestCommon.Gym;
using TestCommon.Subscriptions;

namespace GymManagement.Domain.UnitTests.Subscriptions;

public class SubscriptionsTest
{
    [Fact]
    public void AddGym_WhenMoreThanSubscriptionsAllows_ShouldFail()
    {
        // Arrange
        Subscription subscription = SubscriptionFactory.CreateSubscription();

        IEnumerable<Gym> gyms = Enumerable
            .Range(0, subscription.GetMaxRooms() + 1)
            .Select(_ => GymFactory.CreateGym(id: Guid.CreateVersion7(), subscriptionId: subscription.Id))
            .ToList();

        // Act
        List<ErrorOr<Success>> addedGymsResult = gyms.Select(gym => subscription.AddGym(gym)).ToList();
        List<ErrorOr<Success>> allAddedGymsResultBustLast = addedGymsResult[..^1];
        ErrorOr<Success> lastAddedGymResult = addedGymsResult.Last();

        // Assert
        Assert.All(allAddedGymsResultBustLast, result => Assert.True(result.Value == Result.Success));
        
        Assert.True(lastAddedGymResult.IsError);
        Assert.Equal(lastAddedGymResult.FirstError, SubscriptionErrors.CannotHaveMoreGyms);
    }
}