using GymManagement.SharedKernel.Enums;

namespace TestCommon.Constants;

public static partial class Constants
{
    public static class Subscription
    {
        public const SubscriptionType DefaultSubscriptionType = SubscriptionType.Free;
        public readonly static Guid Id = Guid.CreateVersion7();
        public const int MaxDailySessionFreeTier = 3;
        public const int MaxRoomFreeTier = 1;
        public const int MaxGymFreeTier = 1;
    }
}