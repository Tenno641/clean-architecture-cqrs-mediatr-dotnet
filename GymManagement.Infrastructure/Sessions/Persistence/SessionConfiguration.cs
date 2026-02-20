using GymManagement.Domain.Sessions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Infrastructure.Sessions.Persistence;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder
            .Property(s => s.StartTime)
            .HasColumnType("time(0)");

        builder
            .Property(s => s.EndTime)
            .HasColumnType("time(0)");
    }
}