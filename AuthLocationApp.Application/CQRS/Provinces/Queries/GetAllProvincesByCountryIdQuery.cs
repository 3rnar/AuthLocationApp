using System.Collections.Immutable;
using MediatR;
using AuthLocationApp.Application.DTOs.Provinces;

namespace AuthLocationApp.Application.CQRS.Provinces.Queries
{
   public record GetAllProvincesByCountryIdQuery(int CountryId) : IRequest<IImmutableList<ProvinceDto>>;
}
