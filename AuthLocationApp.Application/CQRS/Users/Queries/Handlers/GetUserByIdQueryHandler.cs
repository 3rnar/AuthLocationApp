using MediatR;
using AuthLocationApp.Application.DTOs.Users;
using AuthLocationApp.Application.Interfaces.Repositories;

namespace AuthLocationApp.Application.CQRS.Users.Queries.Handlers
{
   public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
   {
      private readonly IUserRepository _userRepository;

      public GetUserByIdQueryHandler(IUserRepository userRepository)
      {
         _userRepository = userRepository;
      }

      public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
      {
         var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
         return user == null ? null : new UserDto(user.Id, user.Email.ToString(), user.CountryId, user.ProvinceId);
      }
   }
}
