using FluentValidation;
using FluentValidation.Attributes;

namespace Notion.Common.RequestModels
{
    [Validator(typeof(UserRegisterRequestValidator))]
    public class UserRegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }

    public class UserRegisterRequestValidator : AbstractValidator<UserRegisterRequest>
    {
        public UserRegisterRequestValidator()
        {
            RuleFor(user => user.FirstName).NotEmpty().NotNull().WithMessage("First name should not be empty or null");
            RuleFor(user => user.LastName).NotEmpty().NotNull().WithMessage("First name should not be empty or null");
            RuleFor(user => user.Email).NotEmpty().WithMessage("Email address is required").EmailAddress().WithMessage("A valid email is required");
            RuleFor(user => user.Password).NotEmpty().WithMessage("Password is required").MinimumLength(6).WithMessage("Password should be minimum 6 length");
            RuleFor(user => user.ConfirmPassword).Equal(user => user.Password).WithMessage("Passwords are not equal").NotEmpty().WithMessage("Confirm Pasword can not be null");
        }

       
    }
}