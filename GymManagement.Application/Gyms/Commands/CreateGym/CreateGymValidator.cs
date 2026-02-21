using FluentValidation;

namespace GymManagement.Application.Gyms.Commands.CreateGym;

public class CreateGymValidator : AbstractValidator<CreateGymCommand>
{
    public CreateGymValidator()
    {
        RuleFor(gym => gym.Name)
            .MinimumLength(3)
            .MaximumLength(80);
    }
}