using System.Collections.Immutable;
using MediatR;
using AuthLocationApp.Application.DTOs.Countries;

namespace AuthLocationApp.Application.CQRS.Countries.Queries
{
   public record GetAllCountriesQuery() : IRequest<IImmutableList<CountryDto>>;
}
