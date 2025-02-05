using System.Collections.Immutable;
using MediatR;
using AuthLocationApp.Application.DTOs.Provinces;
using AuthLocationApp.Application.Interfaces.Repositories;

namespace AuthLocationApp.Application.CQRS.Provinces.Queries.Handlers
{
   public class GetAllProvincesByCountryIdQueryHandler : IRequestHandler<GetAllProvincesByCountryIdQuery, IImmutableList<ProvinceDto>>
   {
      private readonly IProvinceRepository _provinceRepository;

      public GetAllProvincesByCountryIdQueryHandler(IProvinceRepository provinceRepository)
      {
         _provinceRepository = provinceRepository;
      }

      public async Task<IImmutableList<ProvinceDto>> Handle(GetAllProvincesByCountryIdQuery request, CancellationToken cancellationToken)
      {
         var provinces = await _provinceRepository.GetAllByCountryIdAsync(request.CountryId, cancellationToken);

         return provinces
             .Select(c => new ProvinceDto(c.Id, c.Name))
             .ToImmutableList();
      }
   }
}
