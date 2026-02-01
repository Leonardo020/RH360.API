using FluentAssertions;
using FluentValidation.TestHelper;
using RH360.Application.Users.CreateUser;

namespace RH360.Tests.Users
{
    public class CreateUserCommandValidatorTests
    {
        private readonly CreateUserCommandValidator _validator;

        public CreateUserCommandValidatorTests()
        {
            _validator = new CreateUserCommandValidator();
        }

        // Using FluentAssertions

        [Fact]
        public void Should_Pass_When_Command_Is_Valid()
        {
            // Arrange
            var command = new CreateUserCommand(
                Name: "Leonardo",
                Email: "email@teste.com",
                Password: "123456"
            );

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.IsValid.Should().Be(true);
        }

        // Using FluentValidation.TestHelper

        [Fact]
        public void Should_Fail_When_Name_Is_Empty()
        {
            var command = new CreateUserCommand("", "email@test.com", "hash123");

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Fail_When_Name_Exceeds_MaxLength()
        {
            var longName = new string('A', 151);
            var command = new CreateUserCommand(longName, "email@test.com", "hash123");

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Fail_When_Email_Is_Invalid()
        {
            var command = new CreateUserCommand("User", "email_invalido", "hash123");

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Fail_When_Email_Exceeds_MaxLength()
        {
            var longEmail = new string('a', 201) + "@teste.com";
            var command = new CreateUserCommand("User", longEmail, "hash123");

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Fail_When_PasswordHash_Is_Empty()
        {
            var command = new CreateUserCommand("User", "email@test.com", "");

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Should_Fail_When_PasswordHash_Exceeds_MaxLength()
        {
            var longHash = new string('H', 256);
            var command = new CreateUserCommand("User", "email@test.com", longHash);

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Password);
        }
    }

}
