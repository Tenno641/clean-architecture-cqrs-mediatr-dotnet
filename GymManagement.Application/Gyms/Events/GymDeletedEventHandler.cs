using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using GymManagement.Domain.Subscriptions.Events;
using MediatR;

namespace GymManagement.Application.Gyms.Events;

public class GymDeletedEventHandler : INotificationHandler<GymDeletedEvent>
{
    private readonly IGymsRepository _gymsRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    
    public GymDeletedEventHandler(IGymsRepository gymsRepository, IUnitOfWork unitOfWork)
    {
        _gymsRepository = gymsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(GymDeletedEvent notification, CancellationToken cancellationToken)
    {
        Gym? gym = await _gymsRepository.GetGymByIdAsync(notification.GymId);

        if (gym is null)
            throw new InvalidOperationException(); // resilient error handling
        
        _gymsRepository.RemoveGym(gym);
        
        await _unitOfWork.CommitChangesAsync();
    }
}