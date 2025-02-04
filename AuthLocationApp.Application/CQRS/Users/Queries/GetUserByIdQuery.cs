using MediatR;
using AuthLocationApp.Application.DTOs.Users;

namespace AuthLocationApp.Application.CQRS.Users.Queries
{
   public record GetUserByIdQuery(int Id) : IRequest<UserDto?>;
}
