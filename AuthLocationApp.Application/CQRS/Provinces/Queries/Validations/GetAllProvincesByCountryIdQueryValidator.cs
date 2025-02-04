using FluentValidation;

namespace AuthLocationApp.Application.CQRS.Provinces.Queries.Validations
{
   public class GetAllProvincesByCountryIdQueryValidator : AbstractValidator<GetAllProvincesByCountryIdQuery>
   {
      public GetAllProvincesByCountryIdQueryValidator()
      {
         RuleFor(x => x.CountryId)
             .GreaterThan(0).WithMessage("CountryId must be greater than 0.");
      }
   }
}
