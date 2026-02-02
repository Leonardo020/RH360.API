using FluentValidation.TestHelper;
using RH360.Application.Users.UpdateUser;

namespace RH360.Tests.Users
{
    public class UpdateUserPasswordCommandValidatorTests
    {
        private readonly UpdateUserPasswordCommandValidator _validator;

        public UpdateUserPasswordCommandValidatorTests()
        {
            _validator = new UpdateUserPasswordCommandValidator();
        }

        // Using FluentValidation.TestHelper

        [Fact]
        public void Should_Fail_When_Password_Is_Empty()
        {
            var command = new UpdateUserPasswordCommand(
                Id: 1,
                Password: ""
            );

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Should_Fail_When_Password_Exceeds_MaxLength()
        {
            var longHash = new string('H', 256);

            var command = new UpdateUserPasswordCommand(
                1,
                longHash
            );

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Password);
        }
    }
}
