using FluentValidation.TestHelper;
using RH360.Application.Users.UpdateUser;

namespace RH360.Tests.Users
{
    public class UpdateUserCommandValidatorTests
    {
        private readonly UpdateUserCommandValidator _validator;

        public UpdateUserCommandValidatorTests()
        {
            _validator = new UpdateUserCommandValidator();
        }

        // Using Xunit
        [Fact]
        public void Should_Pass_When_Command_Is_Valid()
        {
            var command = new UpdateUserCommand(
                Id: 1,
                Name: "Novo Nome",
                Email: "email@teste.com"
            );

            var result = _validator.TestValidate(command);

            Assert.True(result.IsValid);
        }

        // Using FluentValidation.TestHelper
        [Fact]
        public void Should_Fail_When_Id_Is_Invalid()
        {
            var command = new UpdateUserCommand(
                Id: 0,
                Name: "Nome",
                Email: "email@teste.com"
            );

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void Should_Fail_When_Name_Is_Empty()
        {
            var command = new UpdateUserCommand(
                Id: 1,
                Name: "",
                Email: "email@teste.com"
            );

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Fail_When_Name_Exceeds_MaxLength()
        {
            var longName = new string('A', 151);
            var command = new UpdateUserCommand(
                1, longName, "email@teste.com"
            );

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Fail_When_Email_Is_Invalid()
        {
            var command = new UpdateUserCommand(
                Id: 1,
                Name: "User",
                Email: "email_invalido"
            );

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Fail_When_Email_Exceeds_MaxLength()
        {
            var longEmail = new string('a', 201) + "@test.com";

            var command = new UpdateUserCommand(
                Id: 1,
                Name: "User",
                Email: longEmail
            );

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }
    }
}
