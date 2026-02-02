using FluentAssertions;
using FluentValidation.TestHelper;
using MediatR;
using RH360.Application.Users.CreateUser;
using RH360.Domain.DTO;
using Moq;
using RH360.Application.Users.GetUserByEmail;

namespace RH360.Tests.Users
{
    public class CreateUserCommandValidatorTests
    {
        private readonly CreateUserCommandValidator _validator;
        private readonly Mock<IMediator> _mediatorMock;

        public CreateUserCommandValidatorTests()
        {
            _mediatorMock = new Mock<IMediator>();

            _mediatorMock
                .Setup(x => x.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((GetUserDto?)null);

            _validator = new CreateUserCommandValidator(_mediatorMock.Object);
        }

        // Using FluentAssertions

        [Fact]
        public async Task Should_Pass_When_Command_Is_Valid()
        {
            // Arrange
            var command = new CreateUserCommand(
                Name: "Leonardo",
                Email: "email@teste.com",
                Password: "12345678"
            );

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.IsValid.Should().Be(true);
        }

        [Fact]
        public async Task Should_Fail_When_Name_Is_Empty()
        {
            var command = new CreateUserCommand("", "email@test.com", "hash123");

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public async Task Should_Fail_When_Name_Exceeds_MaxLength()
        {
            var longName = new string('A', 151);
            var command = new CreateUserCommand(longName, "email@test.com", "hash123");

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public async Task Should_Fail_When_Email_Is_Invalid()
        {
            var command = new CreateUserCommand("User", "email_invalido", "hash123");

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public async Task Should_Fail_When_Email_Exceeds_MaxLength()
        {
            var longEmail = new string('a', 201) + "@teste.com";
            var command = new CreateUserCommand("User", longEmail, "hash123");

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public async Task Should_Fail_When_Email_Already_Exists()
        {
            // Arrange: simula que o e-mail existe
            _mediatorMock
                .Setup(x => x.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetUserDto { Id = 1, Email = "email@test.com" });

            var command = new CreateUserCommand("User", "email@test.com", "12345678");

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public async Task Should_Fail_When_PasswordHash_Is_Empty()
        {
            var command = new CreateUserCommand("User", "email@test.com", "");

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public async Task Should_Fail_When_PasswordHash_Exceeds_MaxLength()
        {
            var longHash = new string('H', 256);
            var command = new CreateUserCommand("User", "email@test.com", longHash);

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.Password);
        }
    }

}
