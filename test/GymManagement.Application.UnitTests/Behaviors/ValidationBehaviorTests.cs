using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using GymManagement.Application.Behaviors;
using GymManagement.Application.Gyms.Commands.CreateGym;
using MediatR;
using Moq;
using TestCommon.Constants;
using TestCommon.Gym;

namespace GymManagement.Application.UnitTests.Behaviors;

public class ValidationBehaviorTests
{
    private readonly Mock<RequestHandlerDelegate<ErrorOr<Guid>>> _requestHandlerMock;
    private readonly Mock<IValidator<CreateGymCommand>> _validatorMock;
    
    public ValidationBehaviorTests()
    {
        _requestHandlerMock = new Mock<RequestHandlerDelegate<ErrorOr<Guid>>>();

        _validatorMock = new Mock<IValidator<CreateGymCommand>>();
    }
    
    [Fact]
    public async Task Handle_ValidationResultHasNoErrors_ShouldCallNextHandler()
    {
        // Arrange 
        CreateGymCommand command = GymCommandFactory.CreateCreateGymCommand();
        Guid behaviorResult = Constants.Gym.Id;

        _validatorMock.Setup(validator => validator.Validate(command))
            .Returns(new ValidationResult());
        
        _requestHandlerMock.Setup(handler => handler.Invoke(CancellationToken.None))
                    .ReturnsAsync(behaviorResult);
        
        ValidationBehavior<CreateGymCommand, ErrorOr<Guid>> validationBehavior = new ValidationBehavior<CreateGymCommand, ErrorOr<Guid>>();

        // Act
        ErrorOr<Guid> result =await validationBehavior.Handle(command, _requestHandlerMock.Object, CancellationToken.None);

        // Assert
        Assert.False(result.IsError);
        Assert.Equal(result.Value, behaviorResult);
    }
}