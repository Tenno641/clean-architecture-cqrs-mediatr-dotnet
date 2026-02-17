namespace GymManagement.Application.Common;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}