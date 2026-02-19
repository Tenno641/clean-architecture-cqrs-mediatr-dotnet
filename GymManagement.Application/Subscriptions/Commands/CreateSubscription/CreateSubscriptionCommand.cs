using ErrorOr;
using GymManagement.Domain.Subscriptions;
using GymManagement.SharedKernel.Enums;
using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.CreateSubscription;

public record CreateSubscriptionCommand(SubscriptionType SubscriptionType, Guid AdminId) : IRequest<ErrorOr<Subscription>>;