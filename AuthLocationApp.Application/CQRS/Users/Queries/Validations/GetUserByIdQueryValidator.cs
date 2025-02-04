using FluentValidation;

namespace AuthLocationApp.Application.CQRS.Users.Queries.Validations
{
   public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
   {
      public GetUserByIdQueryValidator()
      {
         RuleFor(x => x.Id)
             .GreaterThan(0).WithMessage("UserId must be greater than 0.");
      }
   }
}
