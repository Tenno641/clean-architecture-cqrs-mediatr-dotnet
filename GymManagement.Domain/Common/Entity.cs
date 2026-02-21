using GymManagement.Domain.Common.Events;

namespace GymManagement.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; init; }
    protected List<IDomainEvent> DomainEvents { get; init; }

    protected Entity(Guid id)
    {
        Id = id;
    }
    
    protected Entity() { }
}