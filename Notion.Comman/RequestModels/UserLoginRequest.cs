using FluentValidation;
using FluentValidation.Attributes;

namespace Notion.Comman.RequestModels
{
    [Validator(typeof(UserLoginRequestValidator))]
    public class UserLoginRequest
    {

        public string Email { get; set; }
        public string Password { get; set; }
        
    }

    public class UserLoginRequestValidator : AbstractValidator<UserLoginRequest>
    {
        public UserLoginRequestValidator()
        {
            RuleFor(user => user.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Please enter valid email address");
            RuleFor(user => user.Password).NotEmpty().WithMessage("Password is required").MinimumLength(6).WithMessage("Password required minimum 6 length");
        }
    }
}