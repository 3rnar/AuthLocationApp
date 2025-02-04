using System.Collections.Immutable;
using MediatR;
using AuthLocationApp.Application.DTOs.Countries;
using AuthLocationApp.Application.Interfaces.Repositories;

namespace AuthLocationApp.Application.CQRS.Countries.Queries.Handlers
{
   internal class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, IImmutableList<CountryDto>>
   {
      private readonly ICountryRepository _countryRepository;

      public GetAllCountriesQueryHandler(ICountryRepository countryRepository)
      {
         _countryRepository = countryRepository;
      }

      public async Task<IImmutableList<CountryDto>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
      {
         var countries = await _countryRepository.GetAllAsync(true, cancellationToken);

         return countries
             .Select(c => new CountryDto(c.Id, c.Name))
             .ToImmutableList();
      }
   }
}
