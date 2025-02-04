using FluentValidation;
using AuthLocationApp.Application.CQRS.Users.Commands;

namespace AuthLocationApp.Application.CQRS.Users.Commands.Validations
{
   public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
   {
      public CreateUserCommandValidator()
      {
         RuleFor(x => x.User.Email)
             .NotEmpty().WithMessage("Email is required.")
             .EmailAddress().WithMessage("Invalid email format.");

         RuleFor(x => x.User.Password)
             .NotEmpty().WithMessage("Password is required.")
             .Matches(@"^(?=.*[A-Za-z])(?=.*\d).+$")
             .WithMessage("Password must contain at least one letter and one digit.");

         RuleFor(x => x.User.CountryId)
             .NotNull().WithMessage("CountryId is required.")
             .GreaterThan(0).WithMessage("CountryId must be greater than 0.");

         RuleFor(x => x.User.ProvinceId)
             .NotNull().WithMessage("ProvinceId is required.")
             .GreaterThan(0).WithMessage("ProvinceId must be greater than 0.");
      }
   }
}
